using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connect4
{
    class Game
    {
        Player[] players = new Player[2];
        int[] colCounter = new int[7];
        int playsCounter = 0, firstPlayer = -1;
        public int[,] board = new int[6, 7];
        public int spaceID = 2;
        public int turn = -1; // -1 -> game off, 0 -> player1, 1 -> player2
        public int state = 3; // 0 -> first, 1 -> second, 2 -> tie, 3 -> game on, 4 -> stop
        public int gameMode = 0; // 0 -> no ai, 1 -> one ai, 2 -> two ai
        //TODO: enums
        public List<ShapeInfo> winLines = new List<ShapeInfo>();

        public Game(Player p1, Player p2, int firstPlayer)
        {
            players[0] = p1;
            players[1] = p2;
            this.firstPlayer = firstPlayer;
            //NewGame(); extra call
        }

        public void NewGame()
        {
            gameMode = (players[0].type == 0 ? 0 : 1) + (players[0].type == 0 ? 0 : 1);
            for (int i = 0; i < 2; i++)
                players[i].NewGame();
            for (int i = 0; i < 7; i++)
                colCounter[i] = 0;      
            for (int i = 0; i < 6; i++)
                for (int e = 0; e < 7; e++)
                    board[i, e] = spaceID;
            playsCounter = 0;
            turn = firstPlayer;
            state = 3;
            winLines.Clear();
        }

        public void ExternalAiCrashed()
        {
            state = turn == 0 ? 1 : 0;
            turn = -1;
        }

        public void Stop()
        {
            state = 4;
        }

        public int ClickedOn(int x, int y)
        {
            int badValue = -2;
            if (turn == -1 || players[turn].type != 0 || players[turn].online)
                return badValue;
            int c = badValue;
            int lx = 183, rx = 258;
            for (int i = 0; c == badValue && i < 7; i++)
                c = (x > lx + i * 15 + i * 76 && x < rx + i * 15 + i * 76 ? i : badValue);
            if (c != badValue)
                c = y > 12 && y < 490 && colCounter[c] < 6 ? c : badValue;
            return c;
        }

        public void PlayAt(int c)
        {
            board[5 - colCounter[c], c] = turn;
            colCounter[c]++;
            playsCounter++;
        }

        public void SwitchTurn()
        {
            turn = turn == 0 ? 1 : 0;
        }

        public int GetColCounter(int c)
        {
            return colCounter[c];
        }

        public bool ValidMove(int col)
        {
            return col >= 0 && col <= 6 && colCounter[col] < 6;
        }

        public void UpdateGameState()
        {
            int[] dirR = { 1, 0, 1, -1 };
            int[] dirC = { 0, 1, 1, 1 };
            int pl, count;
            for (int i = 0; i < 6; i++)
                for (int e = 0; e < 7; e++)
                {
                    pl = board[i, e];
                    for (int d = 0; pl != spaceID && d < 4; d++)
                    {
                        count = DFS(i, e, dirR[d], dirC[d], pl);
                        if (count >= 4)
                        {
                            int er = i, ec = e;
                            while (true)
                            {
                                int ro = er + dirR[d], co = ec + dirC[d];
                                if (ro == 6 || ro < 0 || co == 7 || co < 0 || board[ro, co] != pl)
                                    break;
                                er = ro;
                                ec = co;
                            }
                            winLines.Add(new ShapeInfo((185 + e * 90) + 36, (14 + i * 80) + 36, (185 + ec * 90) + 36, (14 + er * 80) + 36));
                            state = pl;
                        }
                    }
                }
            if (state == 3 && playsCounter == 42)
                state = 2;
            if (state != 3)
                turn = -1;
        }

        int DFS(int ro, int co, int Rmv, int Cmv, int nm)
        {
            if (ro == 6 || ro < 0 || co == 7 || co < 0 || board[ro, co] != nm)
                return 0;
            return 1 + DFS(ro + Rmv, co + Cmv, Rmv, Cmv, nm);
        }

    }
}
