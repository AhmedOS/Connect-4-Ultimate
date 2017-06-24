using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace connect4
{
    public partial class MainForm : Form
    {
        GameForm gameForm;
        Options options;
        OptionsForm optionsForm;
        LanForm lanForm;
        ProgressForm progressForm;
        Connection connection;
        Player[] players;
        TextBox[] tbxName;
        Label[] lblType;
        Label[] lblColor;
        PictureBox[] picBox;
        int onlinePlayer = -1;
        static string[] playerType = { "Human", "Built-in AI", "Custom AI" };

        public MainForm()
        {
            InitializeComponent();
            LoadFonts();
            Icon = CommonProperties.GetIcon();
            players = new Player[] { new Player(0), new Player(1) };
            tbxName = new TextBox[] { tbxP1Name, tbxP2Name };
            lblType = new Label[] { lblP1Type, lblP2Type };
            lblColor = new Label[] { lblP1Color, lblP2Color };
            picBox = new PictureBox[] { picBoxP1, picBoxP2 };
            ChangePlayerColor(0, Color.Red);
            ChangePlayerColor(1, Color.Yellow);
            options = new Options();
            connection = new Connection();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            optionsForm = new OptionsForm(options);
            optionsForm.ShowDialog(this);
            options = optionsForm.options;
        }

        void startGame(int firstPlayer)
        {
            if (connection.Connected())
            {
                connection.ChangeMyState(Connection.State.Waiting);
                if (connection.otherPlayerState != Connection.State.Waiting)
                {
                    progressForm = new ProgressForm();
                    progressForm.DisplayedText = "Waiting Other Player";
                    try { progressForm.ShowDialog(this); }
                    catch { }
                }
                if (connection.myState == connection.otherPlayerState
                   && connection.myState == Connection.State.Waiting)
                {
                    gameForm = new GameForm(players, options, firstPlayer, connection);
                    gameForm.ShowDialog(this);
                }
                connection.ChangeMyState(Connection.State.Idle);
                return;
            }
            gameForm = new GameForm(players, options, firstPlayer);
            gameForm.ShowDialog(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lanForm = new LanForm(connection);
            lanForm.DisconnectRequested += OnDisconnectRequested;
            lanForm.ShowDialog(this);
            if (lanForm.myID != -1)
                PlayerConnected(lanForm.myID);
        }
        void OnDisconnectRequested(object sender, EventArgs e)
        {
            PlayerDisconnected();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (onlinePlayer == -1 || onlinePlayer != id)
            {
                Color color = selectColor(players[id].color);
                ChangePlayerColor(id, color);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            int id = 1;
            if (onlinePlayer == -1 || onlinePlayer != id)
            {
                Color color = selectColor(players[id].color);
                ChangePlayerColor(id, color);
            }
        }

        void ChangePlayerColor(int id, Color color)
        {
            players[id].color = color;
            tbxName[id].ForeColor = players[id].color;
            lblColor[id].ForeColor = players[id].color;
            lblType[id].ForeColor = players[id].color;
            if (onlinePlayer != -1 && id != onlinePlayer)
                connection.Send(Connection.Info.Color, players[id].color.ToArgb().ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tbxP1Name.Text == "" || tbxP2Name.Text == "")
                MessageBox.Show(this, "Both players should have a name.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                startGame(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "By: Ahmed Osama\n2017", "About",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (onlinePlayer == -1 || onlinePlayer != id)
            {
                PlayerTypeForm sai = new PlayerTypeForm();
                sai.setData(players[0].type, players[0].externalBrain.filePath,
                            players[0].externalBrain.executor, players[0].externalBrain.timeout);
                sai.ShowDialog(this);
                if (sai.saved)
                {
                    players[0].type = sai.type;
                    players[0].externalBrain.filePath = sai.filePath;
                    players[0].externalBrain.executor = sai.executor;
                    players[0].externalBrain.timeout = sai.timeout;
                    lblP1Type.Text = playerType[players[0].type];
                    if (onlinePlayer != -1)
                        connection.Send(Connection.Info.Type, players[0].type.ToString());
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            int id = 1;
            if (onlinePlayer == -1 || onlinePlayer != id)
            {
                PlayerTypeForm sai = new PlayerTypeForm();
                sai.setData(players[1].type, players[1].externalBrain.filePath,
                            players[1].externalBrain.executor, players[1].externalBrain.timeout);
                sai.ShowDialog(this);
                if (sai.saved)
                {
                    players[1].type = sai.type;
                    players[1].externalBrain.filePath = sai.filePath;
                    players[1].externalBrain.executor = sai.executor;
                    players[1].externalBrain.timeout = sai.timeout;
                    lblP2Type.Text = playerType[players[1].type];
                    if (onlinePlayer != -1)
                        connection.Send(Connection.Info.Type, players[1].type.ToString());
                }
            }
        }

        string selectPicture(string path)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files|*.jpg; *.jpeg; *.png; *.gif; *.tif";
            string str = "";
            if (open.ShowDialog(this) == DialogResult.OK
                && !String.IsNullOrEmpty(str = open.FileName)
                && File.Exists(str))
            {
                string ext = Path.GetExtension(str).ToLower();
                string[] extArr = { ".jpg", ".jpeg", ".png", ".gif", ".tif" };
                bool ok = false;
                for (int i = 0; i < 5 && !ok; i++)
                    ok = ext == extArr[i];
                if (ok)
                    return str;
            }
            return path;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (onlinePlayer == -1 || onlinePlayer != id)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                if (me.Button == MouseButtons.Left)
                {
                    ChangePlayerImage(id, selectPicture(players[id].imageLocation));
                }
                else if (me.Button == MouseButtons.Right)
                {
                    ChangePlayerImage(id, "");
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            int id = 1;
            if (onlinePlayer == -1 || onlinePlayer != id)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                if (me.Button == MouseButtons.Left)
                {
                    ChangePlayerImage(id, selectPicture(players[id].imageLocation));
                }
                else if (me.Button == MouseButtons.Right)
                {
                    ChangePlayerImage(id, "");
                }
            }
        }

        Color selectColor(Color clr)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = clr;
            if (dialog.ShowDialog(this) == DialogResult.OK)
                return dialog.Color;
            return clr;
        }

        void ModifyComponents()
        {
            Cursor cur = connection.Connected() ? Cursors.Default : Cursors.Hand;
            tbxName[onlinePlayer].ReadOnly = !tbxName[onlinePlayer].ReadOnly;
            picBox[onlinePlayer].Cursor = cur;
            lblType[onlinePlayer].Cursor = cur;
            lblColor[onlinePlayer].Cursor = cur;
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (connection.Connected() && !bgw.CancellationPending)
                {
                    connection.Receive();
                    MessageReceived();
                }
            }
            catch
            {
                Connection.State myState = connection.myState;
                if (myState == Connection.State.Playing || myState == Connection.State.PlayAgain)
                    Invoke((MethodInvoker)(() => gameForm.OnlinePlayerQuitted()));
                Invoke((MethodInvoker)(() => PlayerDisconnected()));
            }
        }

        void MessageReceived()
        {
            Connection.State myState = connection.myState;
            Connection.Info info = connection.receivedInfo;
            int nm = -1;
            if (connection.receivedNums.Count > 0)
                nm = connection.receivedNums[0];
            string data = connection.receivedData;
            if (info == Connection.Info.Idle)
            {
                if (myState == Connection.State.Playing || myState == Connection.State.PlayAgain)
                    Invoke((MethodInvoker)(() => gameForm.OnlinePlayerQuitted()));
                connection.otherPlayerState = Connection.State.Idle;
            }
            else if (info == Connection.Info.Waiting)
            {
                connection.otherPlayerState = Connection.State.Waiting;
                if (connection.myState == Connection.State.Waiting)
                    progressForm.InstantClose();
            }
            else if (info == Connection.Info.Disconnected)
            {
                if (myState == Connection.State.Playing || myState == Connection.State.PlayAgain)
                    Invoke((MethodInvoker)(() => gameForm.OnlinePlayerQuitted()));
                Invoke((MethodInvoker)(() => PlayerDisconnected()));
            }
            else if (info == Connection.Info.Playing)
            {
                connection.otherPlayerState = Connection.State.Playing;
            }
            else if (info == Connection.Info.PlayAgain)
            {
                connection.otherPlayerState = Connection.State.PlayAgain;
                if (connection.myState == Connection.State.PlayAgain)
                    Invoke((MethodInvoker)(() => gameForm.NewGame()));
                else
                    Invoke((MethodInvoker)(() => gameForm.PlayAgainRequested()));
            }
            else if (info == Connection.Info.PlayAt && myState == Connection.State.Playing)
            {
                Invoke((MethodInvoker)(() => gameForm.UpdateAll(nm, -1)));
            }
            else if (info == Connection.Info.Type)
            {
                Invoke((MethodInvoker)(() =>
                {
                    players[onlinePlayer].type = nm;
                    lblType[onlinePlayer].Text = playerType[nm];
                }));
            }
            else if (info == Connection.Info.Color)
                Invoke((MethodInvoker)(() => ChangePlayerColor(onlinePlayer, Color.FromArgb(nm))));
            else if (info == Connection.Info.Name)
                Invoke((MethodInvoker)(() => tbxName[onlinePlayer].Text = data));
            else if (info == Connection.Info.Image)
            {
                string fileName = "";
                if (nm != 0)
                    fileName = connection.ReceiveFile(nm);
                Invoke((MethodInvoker)(() => ChangePlayerImage(onlinePlayer, fileName)));
            }
        }

        void PlayerConnected(int myId)
        {
            btnLan.ForeColor = Color.Green;
            onlinePlayer = myId == 0 ? 1 : 0;
            players[onlinePlayer].online = true;
            ModifyComponents();
            bgw.RunWorkerAsync();
            /*
            Why another thread? p1 is receiving, p1 is sending on ui thread.. send buffer is full..
            ui thread blocks till p2 receive.. p2 send message.. p1 received and wants to update ui..
            ui thread is blocked till p2 receive.. p2 can't receive because his ui thread also blocked..
            deadlock.
            */
            new Task(() =>
            {
                connection.Send(Connection.Info.Name, players[myId].name);
                connection.Send(Connection.Info.Color, players[myId].color.ToArgb().ToString());
                connection.Send(Connection.Info.Type, players[myId].type.ToString());
                connection.SendImage(players[myId].imageLocation);
            }).Start();
            InfoForm infoForm = new InfoForm("Online Player Connected", 2);
            infoForm.ShowDialog(this);
        }

        void PlayerDisconnected()
        {
            connection.otherPlayerState = Connection.State.Disconnected;
            connection.Close();
            btnLan.ForeColor = Color.Black;
            players[onlinePlayer].online = false;
            tbxName[onlinePlayer].Text = "";
            ChangePlayerColor(onlinePlayer, (onlinePlayer == 0 ? Color.Red : Color.Yellow));
            players[onlinePlayer].type = 0;
            lblType[onlinePlayer].Text = playerType[0];
            picBox[onlinePlayer].Image = Properties.Resources.electronic_ai;
            ModifyComponents();
            onlinePlayer = -1;
            InfoForm infoForm = new InfoForm("Online Player Disconnected", 2);
            infoForm.ShowDialog(this);
        }

        private void tbxP1Name_TextChanged(object sender, EventArgs e)
        {
            ChangePlayerName(0);
        }

        private void tbxP2Name_TextChanged(object sender, EventArgs e)
        {
            ChangePlayerName(1);
        }

        void ChangePlayerName(int id)
        {
            players[id].name = tbxName[id].Text;
            if (onlinePlayer != -1 && onlinePlayer != id)
                connection.Send(Connection.Info.Name, players[id].name);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (connection.Connected())
                connection.Close();
        }

        void ChangePlayerImage(int id, string path)
        {
            if (id != onlinePlayer && connection.Connected())
                connection.SendImage(path);
            else if (id == onlinePlayer && connection.Connected())
                try { File.Delete(players[id].imageLocation); }
                catch { }
            if (path == "")
            {
                players[id].imageLocation = "";
                picBox[id].Image = Properties.Resources.electronic_ai;
            }
            else
            {
                players[id].imageLocation = path;
                picBox[id].ImageLocation = players[id].imageLocation;
            }
        }

        void LoadFonts()
        {
            this.btnOptions.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 21.75F);
            this.btnAbout.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 54.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLan.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 21.75F);
            this.btnStart.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 21.75F);
            this.tbxP1Name.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 36F);
            this.label2.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxP2Name.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 36F);
            this.lblP1Color.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2Color.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP1Type.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblP2Type.Font = new System.Drawing.Font(CommonProperties.GetFontFamily(CommonProperties.MyFont.ElectroShackle), 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }
}
