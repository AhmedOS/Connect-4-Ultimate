using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace connect4
{
    public class ExternalAI
    {
        public string filePath = "";
        public string executor = "";
        public int timeout = 10;
        public State state;
        public enum State { OK, Timeout, Failed_To_Start, Invalid_Output, Empty_Output, Crashed }
        public int exitCode;

        public void Init(string filePath, string executor, int timeout)
        {
            this.filePath = filePath;
            this.executor = executor;
            this.timeout = timeout;
        }

        Process process;
        string line;
        List<int> nums = new List<int>();
        public Pair GetNextMove(int playerID, int spaceID, int[,] board)
        {
            int oppoID = (playerID == 0 ? 1 : 0);
            Pair ret = new Pair(-1, -1);
            StringBuilder arguments = new StringBuilder();
            arguments.Append(playerID.ToString() + ' ' + oppoID.ToString()
                                                 + ' ' + spaceID.ToString());
            for (int i = 0; i < 6; i++)
                for (int e = 0; e < 7; e++)
                    arguments.Append(' ' + board[i, e].ToString());
            using (process = new Process())
            {
                try
                {
                    process.StartInfo.FileName = (executor == "" ? filePath : executor + " " + filePath);
                    process.StartInfo.Arguments = arguments.ToString();
                    process.StartInfo.RedirectStandardInput = true; //for security?
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                }
                catch
                {
                    state = State.Failed_To_Start;
                    return ret;
                }
                try
                {
                    BackgroundWorker bgw = new BackgroundWorker();
                    bgw.DoWork += Bgw_DoWork;
                    bgw.RunWorkerAsync();
                    process.WaitForExit(timeout * 1000);
                    if (!process.HasExited)
                    {
                        state = State.Timeout;
                        return ret;
                    }
                    exitCode = process.ExitCode;
                }
                catch
                {
                    state = State.Crashed;
                    return ret;
                }
                if (exitCode != 0)
                {
                    state = State.Crashed;
                    return ret;
                }
            }
            ExtractNums();
            if (nums.Count == 0)
                state = State.Empty_Output;
            else if (nums.Count == 1)
            {
                ret.first = nums[0];
                state = State.OK;
            }
            else if (nums.Count >= 2) //accept more than 2 numbers?
            {
                ret.Set(nums[0], nums[1]);
                state = State.OK;
            }
            return ret;
        }

        private void Bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            line = process.StandardOutput.ReadLine();
        }

        void ExtractNums()
        {
            nums.Clear();
            int nm = 0;
            bool flag = false;
            char lst = '+';
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] >= '0' && line[i] <= '9')
                {
                    nm *= 10;
                    nm += line[i] - '0';
                    flag = true;
                }
                else if (flag)
                {
                    nums.Add(nm * (lst == '-' ? -1 : 1));
                    nm = 0;
                    flag = false;
                }
                if (!flag)
                    lst = line[i];
            }
            if (flag)
                nums.Add(nm * (lst == '-' ? -1 : 1));
        }

    }
}
