using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace connect4
{
    public class Connection
    {
        public enum Mode { Disconnected = -1, Server, Client }
        public enum State { Idle, Waiting, Disconnected, Playing, PlayAgain }
        public enum Info { Idle, Waiting, Disconnected, Playing, PlayAgain,
                           PlayAt, Type, Color, Image, Name  }

        Socket socket;
        public IPEndPoint endPoint;
        public State myState, otherPlayerState;
        public Mode connectionMode;

        public Connection()
        {
            myState = State.Disconnected;
            otherPlayerState = State.Disconnected;
            connectionMode = Mode.Disconnected;
            endPoint = new IPEndPoint(0, 777);
        }

        public bool goodIp(string ip)
        {
            IPAddress parsedAddress;
            return (!String.IsNullOrWhiteSpace(ip) && IPAddress.TryParse(ip, out parsedAddress));
        }

        public void Init(string ip, Mode connectionMode)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.NoDelay = true;
            endPoint.Address = IPAddress.Parse(ip);
            this.connectionMode = connectionMode;
            myState = otherPlayerState = State.Disconnected;
        }

        public void Start()
        {
            socket.Bind(endPoint);
            socket.Listen(0);
            socket = socket.Accept();
            myState = otherPlayerState = State.Idle;
        }
        
        public void Connect()
        {
            //socket.Bind();
            socket.Connect(endPoint);
            myState = otherPlayerState = State.Idle;
        }

        public void Close()
        {
            if (Connected())
                Send(Info.Disconnected, "");
            myState = otherPlayerState = State.Disconnected;
            connectionMode = Mode.Disconnected;
            endPoint = new IPEndPoint(0, 777);
            socket.Close();
        }

        public bool Connected()
        {
            return otherPlayerState != State.Disconnected;
        }

        public bool NoConnection()
        {
            return otherPlayerState == State.Disconnected;
        }

        public bool IsOnlinePlayerActive()
        {
            return otherPlayerState == State.Playing || otherPlayerState == State.PlayAgain;
        }

        public void ChangeMyState(State state)
        {
            if (Connected())
            {
                myState = state;
                Send((Info)state, "");
            }
        }

        public void Send(Info info, string data)
        {
            data = ((int)info).ToString() + '=' + data;
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            byte[] bufferLength = BitConverter.GetBytes(dataBytes.Length);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bufferLength);
            socket.Send(bufferLength);
            socket.Send(dataBytes);
        }

        string message;
        public void Receive()
        {
            byte[] receivedBytes = ReceiveBytes(4);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(receivedBytes);
            int length = BitConverter.ToInt32(receivedBytes, 0);
            message = Encoding.UTF8.GetString(ReceiveBytes(length)); //default instead of utf8?
            processMessage();
        }

        byte[] ReceiveBytes(int length)
        {
            byte[] receivedBytes = new byte[length];
            socket.Receive(receivedBytes, length, SocketFlags.None);
            return receivedBytes;
        }

        public Info receivedInfo;
        public string receivedData = "";
        public List<int> receivedNums = new List<int>();
        void processMessage()
        {
            receivedInfo = (Info)(message[0] - '0');
            if ((int)receivedInfo > 4 )
            {
                receivedData = message.Substring(2);
            }
            if ((int)receivedInfo >= 5 && (int)receivedInfo <= 8)
            {
                receivedNums.Clear();
                int nm = 0;
                bool flag = false;
                char lst = '+';
                for (int i = 0; i < receivedData.Length; i++)
                {
                    if (receivedData[i] >= '0' && receivedData[i] <= '9')
                    {
                        nm *= 10;
                        nm += receivedData[i] - '0';
                        flag = true;
                    }
                    else if (flag)
                    {
                        receivedNums.Add(nm * (lst == '-' ? -1 : 1));
                        nm = 0;
                        flag = false;
                    }
                    if (!flag)
                        lst = receivedData[i];
                }
                if (flag)
                    receivedNums.Add(nm * (lst == '-' ? -1 : 1));
            }
        }

        public void SendFile(string filePath)
        {
            long length = new FileInfo(filePath).Length;
            Send(Info.Image, length.ToString());
            socket.SendFile(filePath);
        }

        public string ReceiveFile(int length)
        {
            int maxSize = 4000;
            string fileName = ((long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString();
            while (length != 0)
            {
                int bufferSize = Math.Min(length, maxSize);
                byte[] receivedBytes = ReceiveBytes(bufferSize);
                try
                {
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Append, FileAccess.Write))
                    using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                        binaryWriter.Write(receivedBytes, 0, bufferSize);
                }
                catch
                {
                    MessageBox.Show("Error While Writing Downloaded File.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                length -= bufferSize;
            }
            return fileName;
            //TODO: unsafe cancelling, what if writing failed? what about data in the stream?
        }

        public void SendImage(string filePath)
        {
            if (File.Exists(filePath))
                SendFile(filePath);
            else
                Send(Info.Image, "0");
        }
    }
}
