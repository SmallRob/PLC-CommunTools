using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CommunTools.Entity
{
    public class TCPClient
    {
        private Thread clientThread;

        private IPEndPoint endPoint;

        private IPAddress ip;
        private int port;

        //新建socket连接口
        public Socket Newclient { get; internal set; }
        public bool Connected { get; set; } = false;

        public Thread ClientThread { get => clientThread; set => clientThread = value; }

        public IPEndPoint EndPoint { get => endPoint; set => endPoint = value; }

        public IPAddress Ip { get => ip; set => ip = value; }

        public int Port { get => port; set => port = value; }
    }
}
