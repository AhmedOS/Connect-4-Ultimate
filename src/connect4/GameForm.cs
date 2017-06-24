using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace connect4
{
    public partial class GameForm : Form
    {
        Game game;
        Options options = new Options();
        Player[] players;
        static string[] playerType = { "Human", "Built-in AI", "Custom AI" };
        Label[,] labels;
        PictureBox[] picBox;
        //List or Bitmap?
        List<ShapeInfo> shapes = new List<ShapeInfo>();
        List<ShapeInfo> circles = new List<ShapeInfo>();
        Connection connection = new Connection();
        int onlinePlayer = -1;

        public GameForm()
        {
            InitializeComponent();
        }

        public GameForm(Player[] players, Options options, int firstPlayer, Connection connection)
                 : this(players, options, firstPlayer)
        {
            this.connection = connection;
            onlinePlayer = (players[0].online ? 0 : 1);
        }

        public GameForm(Player[] players, Options options, int firstPlayer)
        {
            InitializeComponent();
            LoadFonts();
            Icon = CommonProperties.GetIcon();
            this.options = options;
            this.players = players;
            Text = players[0].name + " vs " + players[1].name;
            labels = new Label[,] {
                {lblP1State, lblP1Name, lblP1Type, lblP1Time, lblP1Score, lblP1Log},
                {lblP2State, lblP2Name, lblP2Type, lblP2Time, lblP2Score, lblP2Log}
            };
            picBox = new PictureBox[] { pictureBox2, pictureBox3 };
            for (int i = 0; i < 2; i++)
                for (int e = 0; e < 5; e++)
                {
                    if (e == 1)
                        labels[i, e].Text = players[i].name;
                    else if (e == 2)
                        labels[i, e].Text = playerType[players[i].type];
                    labels[i, e].ForeColor = players[i].color;
                }
            BackColor = options.backColor;
            lblTime1.ForeColor = options.boardColor;
            lblTime2.ForeColor = options.boardColor;
            lblScore1.ForeColor = options.boardColor;
            lblScore2.ForeColor = options.boardColor;
            if (players[0].imageLocation != "")
                pictureBox2.ImageLocation = players[0].imageLocation;
            else
                pictureBox2.Image = Properties.Resources.electronic_ai;
            if (players[1].imageLocation != "")
                pictureBox3.ImageLocation = players[1].imageLocation;
            else
                pictureBox3.Image = Properties.Resources.electronic_ai;
            for (int i = 0; i < 2; i++)
                if (players[i].type == 1)
                    players[i].brain.Init(players[i].id, options.searchDepth);
            players[0].score = players[1].score = 0; //right place?
            game = new Game(players[0], players[1], firstPlayer); //0 => firstPlayer
        }

        public void NewGame()
        {
            if (connection.Connected())
                connection.ChangeMyState(Connection.State.Playing);
            game.NewGame();
            ResetPlayerImage(0);
            ResetPlayerImage(1);
            lblP1Log.Text = lblP2Log.Text = "";
            lblP1Time.Text = lblP2Time.Text = "0s";
            UpdateStateLabel((game.turn == 0 ? "Your Turn!" : ""),
                             (game.turn == 1 ? "Your Turn!" : ""));
            circles.Clear();
            shapes.Clear();
            shapes.Add(new ShapeInfo(1, new Pen(options.boardColor, 3), 171, 12, 171, 12 + 480));
            shapes.Add(new ShapeInfo(1, new Pen(options.boardColor, 3), 812, 12, 812, 12 + 480));
            shapes.Add(new ShapeInfo(2, new Pen(options.boardColor, 2), 184, 415, 73, 73));
            Invalidate();
            Update();
            Start();
        }

        public void Start()
        {
            CountdownForm cf = new CountdownForm("The game starts in..", 3);
            cf.ShowDialog(this);
            timer1.Start();
            if (OfflineMachine())
                machineBgw.RunWorkerAsync();
        }

        bool OfflineMachine()
        {
            return (!players[game.turn].online &&
                     (players[game.turn].type == 1 || players[game.turn].type == 2));
        }

        private void GameForm_MouseClick(object sender, MouseEventArgs e)
        {
            Point p = PointToClient(Cursor.Position);
            Console.WriteLine(p.X.ToString() + " " + p.Y.ToString());
            if (e.Button == MouseButtons.Right && game.state != 3)
            {
                if (connection.NoConnection())
                    NewGame();
                else if (connection.IsOnlinePlayerActive())
                {
                    connection.ChangeMyState(Connection.State.PlayAgain);
                    if (connection.otherPlayerState == Connection.State.PlayAgain)
                        NewGame();
                }
            }
            else
            {
                int col = game.ClickedOn(p.X, p.Y);
                UpdateAll(col, -1);
            }
        }

        public void UpdateAll(int col, int exp)
        {
            if (col == -2 || game.state != 3) //invalid click || game off
                return;
            timer1.Stop();
            int op = game.turn == 0 ? 1 : 0;
            if (!game.ValidMove(col) && players[game.turn].type == 2)
            {
                if (players[op].online)
                {
                    if (connection.IsOnlinePlayerActive())
                        try { connection.Send(Connection.Info.PlayAt, col.ToString()); }
                        catch { }
                }
                else if (players[game.turn].online)
                {
                    labels[game.turn, 5].Text = "Player's Custom AI Encountered a Problem While Operating";
                    labels[game.turn, 5].ForeColor = Color.Red;
                }
                game.ExternalAiCrashed();
                CheckGameState(game.state);
                return;
            }
            if (players[op].online)
            {
                if (connection.IsOnlinePlayerActive())
                    try { connection.Send(Connection.Info.PlayAt, col.ToString()); }
                    catch { }
            }
            else if (players[op].type == 1)
                players[op].brain.UpdateBoard(game.turn, col);
            if (exp != -1)
            {
                picBox[game.turn].Image = Expression.GetImage((Expression.Type)exp);
            }
            DrawCircleAt(col);
            game.PlayAt(col);
            game.UpdateGameState();
            CheckGameState(game.state);
        }

        void CheckGameState(int state)
        {
            if (state == 2 || state == 4) //tie || stop
            {
                if (state == 2)
                    UpdateStateLabel("Tie", "Tie");
            }
            else if (state == 3)
            {
                timer1.Start();
                game.SwitchTurn();
                UpdateStateLabel((game.turn == 0 ? "Your Turn!" : ""),
                                 (game.turn == 1 ? "Your Turn!" : ""));
                if (OfflineMachine())
                    machineBgw.RunWorkerAsync();
            }
            else
            {
                UpdateStateLabel((game.state == 0 ? "Winner" : ""),
                                 (game.state == 1 ? "Winner" : ""));
                labels[game.state, 4].Text = (++players[game.state].score).ToString();
                foreach (ShapeInfo si in game.winLines)
                {
                    si.id = 1;
                    si.pen = new Pen(Color.Green, 3);
                    shapes.Add(si);
                    Rectangle area = new Rectangle(Math.Min(si.x, si.a), Math.Min(si.y, si.b),
                                                   Math.Abs(si.x - si.a),Math.Abs(si.y - si.b));
                    area.Inflate(3, 3);
                    Invalidate(area);
                }
                Update();
            }
        }

        void UpdateStateLabel(int playerId, string state)
        {
            labels[playerId, 0].Text = state;
        }

        void UpdateStateLabel(string p1State, string p2State)
        {
            labels[0, 0].Text = p1State;
            labels[1, 0].Text = p2State;
        }

        ExternalAI.State state;
        int bgwCol = -1, bgwExp = -1, exitCode;
        private void machineBgw_DoWork(object sender, DoWorkEventArgs e)
        {
            if (players[game.turn].type == 1)
            {
                Pair ret = players[game.turn].brain.GetNextMove();
                bgwCol = ret.first;
                bgwExp = ret.second;
            }
            else if (players[game.turn].type == 2)
            {
                bgwCol = players[game.turn].externalBrain.GetNextMove(
                        players[game.turn].id, game.spaceID, game.board).first;
                state = players[game.turn].externalBrain.state;
                exitCode = players[game.turn].externalBrain.exitCode;
                if (state == ExternalAI.State.OK)
                    state = game.ValidMove(bgwCol) ? ExternalAI.State.OK : ExternalAI.State.Invalid_Output;
            }
        }

        private void machineBgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (players[game.turn].type == 2 && state != ExternalAI.State.OK)
            {
                labels[game.turn, 5].Text = "Custom AI Error: " + state;
                if (state == ExternalAI.State.Invalid_Output)
                    labels[game.turn, 5].Text += " (" + bgwCol + ")";
                labels[game.turn, 5].Text += '.';
                if (state != ExternalAI.State.Timeout && state != ExternalAI.State.Failed_To_Start)
                    labels[game.turn, 5].Text += " Exit Code = " + exitCode.ToString() + '.';
                labels[game.turn, 5].ForeColor = Color.Red;
            }
            UpdateAll(bgwCol, bgwExp);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            players[game.turn].time += 0.01;
            lblP1Time.Text = Math.Round(players[0].time, 2).ToString() + 's';
            lblP2Time.Text = Math.Round(players[1].time, 2).ToString() + 's';
        }

        void DrawCircleAt(int col)
        {
            circles.Add(new ShapeInfo(new SolidBrush(players[game.turn].color), 184 + col * 90,
                                                     415 - game.GetColCounter(col) * 80, 73, 73));
            Rectangle area = new Rectangle(184 + col * 90, 415 - game.GetColCounter(col) * 80, 73, 73);
            area.Inflate(3, 3);
            Invalidate(area);
            Update();
        }

        private void GameForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics formGraphics;
            formGraphics = e.Graphics;
            formGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            foreach (ShapeInfo si in circles.ToList())
            {   //Tolist() to avoid reading while it is being written to
                formGraphics.FillEllipse(si.sb, si.x, si.y, si.a, si.b);
            }
            foreach (ShapeInfo si in shapes.ToList())
            {
                if (si.id == 1)
                    formGraphics.DrawLine(si.pen, si.x, si.y, si.a, si.b);
                else if (si.id == 2)
                {
                    for (int i = 0; i < 6; i++)
                        for (int j = 0; j < 7; j++)
                            formGraphics.DrawEllipse(si.pen, si.x + j * 90, si.y - i * 80, si.a, si.b);
                }
            }
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
            timer1.Dispose();
            if (machineBgw.IsBusy)
            {
                ProgressForm pf = new ProgressForm();
                pf.DisplayedText = "Closing";
                pf.WaitBackgroundWorker(machineBgw, false, false);
                pf.ShowDialog(this);
            }
        }

        void Stop()
        {
            game.Stop();
            timer1.Enabled = false;
        }

        public void OnlinePlayerQuitted()
        {
            Stop();
            labels[onlinePlayer, 5].Text = "Player Left The Game.";
            labels[onlinePlayer, 5].ForeColor = Color.Red;
            onlinePlayer = -1;
            InfoForm infoForm = new InfoForm("Online Player Left The Game", 2);
            infoForm.ShowDialog(this);
        }

        private void GameForm_Shown(object sender, EventArgs e)
        {
            NewGame();
        }

        public void PlayAgainRequested()
        {
            labels[onlinePlayer, 5].Text = "The Player Likes To Play Again.";
            labels[onlinePlayer, 5].ForeColor = Color.Lime;
        }

        void ResetPlayerImage(int id)
        {
            if (players[id].imageLocation == "")
                picBox[id].Image = Properties.Resources.electronic_ai;
            else
                picBox[id].ImageLocation = players[id].imageLocation;
        }

        void LoadFonts()
        {
            this.lblP2Name.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 18F);
            this.lblP1Name.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1Score.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.AgencyFB), 18F);
            this.lblP1Time.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.AgencyFB), 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2Time.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.AgencyFB), 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2Score.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.AgencyFB), 18F);
            this.lblTime2.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScore2.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1Type.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2Type.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1State.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2State.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }
}
