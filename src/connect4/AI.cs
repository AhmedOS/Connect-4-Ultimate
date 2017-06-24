using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace connect4
{
    public class AI
    {
        int id, op, searchDepth;
        int[,] board = new int[6, 7];
        int spaceID = 2;
        int[] colCounter = new int[7];
        public void Init(int id, int searchDepth)
        {
            this.id = id;
            this.searchDepth = searchDepth;
            op = id == 0 ? 1 : 0;
            NewGame();
        }
        public void NewGame()
        {
            expression = Expression.Type.think_earlytojudge;
            for (int i = 0; i < 6; i++)
                for (int e = 0; e < 7; e++)
                    board[i, e] = spaceID;
            for (int i = 0; i < 7; i++)
                colCounter[i] = 0;
        }
        Expression.Type expression = Expression.Type.think_earlytojudge;
        void UpdateExpression(List<int> list)
        {
            int winChances = 0, loseChances = 0, tieChances = 0;
            int goodValue = 0, badValue = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] >= 300)
                    winChances++;
                else if (list[i] <= -300)
                    loseChances++;
                else if (list[i] == 0)
                    tieChances++;
                if (list[i] > 0)
                    goodValue++;
                else if (list[i] < 0)
                    badValue++;
            }
            if (winChances > 0 && winChances <= 2) //easy win
                expression = Expression.Type.think_closetowin;
            if (winChances > 2) //so easy, let's fool him
                expression = Expression.Type.think_bigwin;
            if (winChances > 0 && (loseChances > 0 || tieChances > 0) && Math.Abs(winChances - loseChances) <= 1) //close match
                expression = Expression.Type.think_closegame;
            if (winChances == 0 && loseChances > 0) //lose
                expression = Expression.Type.think_closetolose;
            if (winChances == 0 && loseChances == 0) //early to judge
                expression = (goodValue + badValue) % 2 == 0 ? Expression.Type.think_earlytojudge : Expression.Type.think_earlytojudge2;
        }
        public Pair GetNextMove()
        {
            int time = DateTime.Now.Millisecond;
            List<int> ret = BestRank(id, 0, time);
            UpdateBoard(id, ret[0]);
            return new Pair(ret[0], (int)expression);
        }
        public void UpdateBoard(int id, int col)
        {
            board[5 - colCounter[col], col] = id;
            colCounter[col]++;
        }
        Pair DFS(int ro, int co, int Rmv, int Cmv, int nm, bool spAct, int spCon)
        {
            Pair ret = new Pair(0, 0);
            if (ro == 6 || ro < 0 || co == 7 || co < 0)
            {
                return ret;
            }
            if ((board[ro, co] != nm && board[ro, co] != spaceID) ||
                (spAct && board[ro, co] != spaceID) ||
                (board[ro, co] == spaceID && spCon <= 0))
                return ret;
            if (board[ro, co] == spaceID)
            {
                ret = DFS(ro + Rmv, co + Cmv, Rmv, Cmv, nm, true, spCon - 1);
                ret.second++;
                return ret;
            }
            ret = DFS(ro + Rmv, co + Cmv, Rmv, Cmv, nm, spAct, spCon);
            ret.first++;
            return ret;
        }

        int[] dirR = { 1, 0, 1, -1 };
        int[] dirC = { 0, 1, 1, 1 };

        bool CheckForWin()
        {
            int count = 0;
            for (int i = 0; i < 6 && count < 4; i++)
            {
                for (int e = 0; e < 7 && count < 4; e++)
                {
                    for (int d = 0; board[i, e] != spaceID && d < 4 && count < 4; d++)
                    {
                        count = DFS(i, e, dirR[d], dirC[d], board[i, e], false, 0).first;
                    }
                }
            }
            return count >= 4;
        }

        List<int> BestRank(int turn, int curDepth, int time)
        {
            int bestCol = -1;
            List<int> curBest = new List<int>();
            if (CheckForWin())
            {
                int val = 300; //TODO: Depth-shifting
                if (turn == id)
                {
                    val *= -1; //Negamax
                }
                curBest.Add(val);
                return curBest;
            }
            bool tie = true;
            for (int i = 0; i < 7 && tie; i++)
            {
                tie = (colCounter[i] > 5);
            }
            if (tie)
            {
                int val = 0; //TODO: Depth-shifting
                if (turn != id)
                {
                    val *= -1; //Negamax
                }
                curBest.Add(val);
                return curBest;
            }
            curBest.Add(turn == id ? -1000000 : 1000000);
            int nw = DateTime.Now.Millisecond;
            int diff = nw - time;
            for (int i = 0; i < 7; i++)
            {
                if (colCounter[i] <= 5)
                { //if there is a space to play
                    int r = 5 - colCounter[i];
                    List<int> tmp = new List<int>();
                    if (curDepth >= searchDepth) //&& diff >= 1000
                    { //Run the heuristic function
                        for (int e = 0; e < 4; e++)
                        {
                            int id = this.id, op = this.op;
                            if (turn != id)
                            {
                                id = op;
                                op = id;
                            }
                            int rank, count, spaces;
                            Pair ret = DFS(r + dirR[e], i + dirC[e], dirR[e], dirC[e], id, false, 2);
                            count = ret.first;
                            spaces = ret.second;
                            ret = DFS(r + dirR[e] * -1, i + dirC[e] * -1, dirR[e] * -1, dirC[e] * -1, id, false, 2);
                            count += ret.first;
                            spaces += ret.second;
                            if (spaces + count < 3)////
                                count = 0;////
                            if (count >= 3)
                            {
                                count *= 100;//////////
                            }
                            else if (count == 2)
                            {
                                count *= 2;///////////
                            }
                            rank = count;
                            ret = DFS(r + dirR[e], i + dirC[e], dirR[e], dirC[e], op, false, 2);
                            count = ret.first;
                            spaces = ret.second;
                            ret = DFS(r + dirR[e] * -1, i + dirC[e] * -1, dirR[e] * -1, dirC[e] * -1, op, false, 2);
                            count += ret.first;
                            spaces += ret.second;
                            if (spaces + count < 3)////
                                count = 0;////
                            if (count >= 3)
                            {
                                count *= 100;//////////
                            }
                            else if (count == 2)
                            {
                                count *= 2;///////////
                            }
                            rank += count; //TODO: Depth-shifting
                            tmp.Add(rank);
                        }
                        if (turn != id)
                        {
                            for (int it = 0; it < tmp.Count; it++)
                            {
                                int val = tmp[it];
                                val *= -1;
                                tmp[it] = val;
                            }
                            tmp.Sort();
                        }
                        else
                        {
                            tmp.Sort();
                            tmp.Reverse();
                        }
                    }
                    else
                    {
                        board[r, i] = turn;
                        colCounter[i]++;
                        tmp = BestRank((turn == id ? op : id), curDepth + 1, time); //Backtracking
                        board[r, i] = spaceID;
                        colCounter[i]--;
                    }
                    for (int e = 0; ;)
                    {
                        if (tmp[e] > curBest[e])
                        {
                            if (turn == id)
                            {
                                curBest = tmp;
                                bestCol = i;
                            }
                            break;
                        }
                        else if (tmp[e] < curBest[e])
                        {
                            if (turn != id)
                            {
                                curBest = tmp;
                                bestCol = i;
                            }
                            break;
                        }
                        e++;
                        if (e == curBest.Count)
                        {
                            curBest = tmp;
                            bestCol = i;
                            break;
                        }
                        else if (e == tmp.Count)
                        {
                            break;
                        }
                    }
                }
            }
            if (curDepth == 0)
            {
                UpdateExpression(curBest);
                curBest.Clear();
                curBest.Add(bestCol);
            }
            return curBest;
        }
    }
}
