#pragma warning (disable : 4996 4018)

#include <map>
#include <set>
#include <stack>
#include <queue>
#include <cmath>
#include <cstdio>
#include <vector>
#include <bitset>
#include <limits>
#include <iomanip>
#include <sstream>
#include <cstring>
#include <iostream>
#include <algorithm>
#include <functional>
using namespace std;

int id, op, searchDepth = 6;
int board[6][7];
int spaceID = 2;
int colCounter[7];

pair<int ,int> DFS(int ro, int co, int Rmv, int Cmv, int nm, bool spAct, int spCon)
{
	pair<int, int> ret = { 0,0 };
	if (ro == 6 || ro < 0 || co == 7 || co < 0)
	{
		return ret;
	}
	if ((board[ro][co] != nm && board[ro][co] != spaceID) ||
		(spAct && board[ro][co] != spaceID) ||
		(board[ro][co] == spaceID && spCon <= 0))
		return ret;
	if (board[ro][co] == spaceID)
	{
		ret = DFS(ro + Rmv, co + Cmv, Rmv, Cmv, nm, true, spCon - 1);
		ret.second++;
		return ret;
	}
	ret = DFS(ro + Rmv, co + Cmv, Rmv, Cmv, nm, spAct, spCon);
	ret.first++;
	return ret;
}

int dirR[] = { 1, 0, 1, -1 };
int dirC[] = { 0, 1, 1, 1 };

bool checkForWin()
{
	int count = 0;
	for (int i = 0; i < 6 && count < 4; i++)
	{
		for (int e = 0; e < 7 && count < 4; e++)
		{
			for (int d = 0; board[i][e] != spaceID && d < 4 && count < 4; d++)
			{
				count = DFS(i, e, dirR[d], dirC[d], board[i][e], false, 0).first;
			}
		}
	}
	return count >= 4;
}

vector<int> bestRank(int turn, int curDepth, int time)
{
	int bestCol = -1;
	vector<int> curBest;
	if (checkForWin())
	{
		int val = 300; //TODO: Depth-shifting
		if (turn == id)
		{
			val *= -1; //Negamax
		}
		curBest.push_back(val);
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
		curBest.push_back(val);
		return curBest;
	}
	curBest.push_back(turn == id ? -1000000 : 1000000);
	//int nw = DateTime.Now.Millisecond;
	//int diff = nw - time;
	for (int i = 0; i < 7; i++)
	{
		if (colCounter[i] <= 5)
		{ //if there is a space to play
			int r = 5 - colCounter[i];
			vector<int> tmp;
			if (curDepth >= searchDepth) //&& diff >= 1000
			{ //Run the heuristic function
				for (int e = 0; e < 4; e++)
				{
					int id = ::id, op = ::op;
					if (turn != id)
					{
						id = op;
						op = id;
					}
					int rank, count, spaces;
					pair<int, int> ret = DFS(r + dirR[e], i + dirC[e], dirR[e], dirC[e], id, false, 2);
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
					tmp.push_back(rank);
				}
				if (turn != id)
				{
					for (int it = 0; it < tmp.size(); it++)
					{
						int val = tmp[it];
						val *= -1;
						tmp[it] = val;
					}
					sort(tmp.begin(), tmp.end());
				}
				else
				{
					sort(tmp.rbegin(), tmp.rend());
				}
			}
			else
			{
				board[r][i] = turn;
				colCounter[i]++;
				tmp = bestRank((turn == id ? op : id), curDepth + 1, time); //Backtracking
				board[r][i] = spaceID;
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
				if (e == curBest.size())
				{
					curBest = tmp;
					bestCol = i;
					break;
				}
				else if (e == tmp.size())
				{
					break;
				}
			}
		}
	}
	if (curDepth == 0)
	{
		curBest.clear();
		curBest.push_back(bestCol);
	}
	return curBest;
}

int getNextMove()
{
	//int time = DateTime.Now.Millisecond;
	vector<int> ret = bestRank(id, 0, 0);
	return ret[0];
}

int main(int argc, char *argv[]) {
	id = argv[1][0] - '0'; //YOUR_ID
	op = argv[1][2] - '0'; //OPPONENT_ID
	spaceID = argv[1][4] - '0'; //EMPTY_SPACE_ID
  //Extract next 42 numbers and build the board
	int idx = 6;
	for (int i = 0; i < 6; i++)
		for (int e = 0; e < 7; e++) {
			if (argv[1][idx] > '9' || argv[1][idx] < '0') {
				e--;
				idx++;
				continue;
			}
			board[i][e] = argv[1][idx] - '0';
			idx++;
			if (board[i][e] != spaceID)
				colCounter[e]++;
		}
	cout << getNextMove() << endl; //output desired column index then terminate
	return 0;
}
