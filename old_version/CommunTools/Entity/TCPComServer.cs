using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CommunTools.Entity
{
    public class TCPComServer
    {
        /// <summary>
        /// COM端口号
        /// </summary>
        public string ComPort { get; set; } = null;

        /// <summary>
        /// 网口IP
        /// </summary>
        public string ip { get; set; } = null;

        /// <summary>
        /// TCP端口
        /// </summary>
        public string port { get; set; } = null;

        /// <summary>
        /// 是否作为TCP客户端
        /// </summary>
        public bool isTCPClient { get; set; } = false;

        public bool IsLoopFlag { get; set; } = false;

        /// <summary>
        /// 已接收包大小
        /// </summary>
        public int RecivePacketNumber { get; set; } = 0;

        /// <summary>
        /// 发送字节大小
        /// </summary>
        public double ReciveBytesNumber { get; set; } = 0;

        /// <summary>
        /// COM端口发送字节大小
        /// </summary>
        public int COMRecivePacketNumber { get; set; } = 0;

        /// <summary>
        /// COM端口接收字节大小
        /// </summary>
        public double COMReciveBytesNumber { get; set; } = 0;

        public Thread ThreadTcpSever { get; set; } = null;
        public Thread ThreadCOMSend { get; set; } = null;

        public Thread ThreadComRecive { get; set; } = null;

        public SynchronizationContext Context { get; set; }

        public bool IsRealShowInfo { get; set; } = true;

        public TcpListener EntityTCPListen { get; set; } = null;

        public SerialPort EntitySerialPort { get; set; } = null;

        public TcpClient TCPClient { get; set; } = null;

        public Queue<byte[]> TCPRecvQueue { get; set; } = new Queue<byte[]>();
    }
}
