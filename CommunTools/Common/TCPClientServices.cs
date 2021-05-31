using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CommunTools.Common
{
    public class TCPClientServices
    {
        private Socket clientSocket;
        private Thread t;
        public string ip = "";
        private byte[] data = new byte[1024];

        public static List<TCPClientServices> clientList = new List<TCPClientServices>();

        public TCPClientServices(Socket socket)
        {
            clientSocket = socket;
            ip = (clientSocket.RemoteEndPoint as IPEndPoint).Address.ToString();//获取客户端的ip
            t = new Thread(ReceiveMessage);//开启线程执行循环接收消息
            t.Start();
        }

        private void ReceiveMessage()//接收消息
        {
            int length = 0;//初始化消息的长度
            while (true)//循环接收消息
            {
                length = clientSocket.Receive(data);//获取存放消息数据数组的长度
                if (clientSocket.Poll(10, SelectMode.SelectRead))//？？？？
                {
                    clientSocket.Close();
                    break;
                }
                if (length != 0)//判断是否有数组内是否存放消息数据
                {
                    string message = Encoding.UTF8.GetString(data, 0, length);//Byte转为string
                    BroadcastMessage(message, this);//广播给其他客户端
                    Console.WriteLine("来自" + (clientSocket.RemoteEndPoint as IPEndPoint).Address + ":" + message);//输出到服务器控制台
                    length = 0;//归0
                }
            }
        }

        private void BroadcastMessage(string message, TCPClientServices tCPClientServices)
        {
            var onConnectedList = new List<TCPClientServices>();//用来存储已断开服务器连接的客户端

            foreach (var client in clientList)//遍历已经存储的客户端
            {
                //如果该客户端是消息来源，则不重复发送消息给该客户端
                if (clientSocket.Equals(client))
                {
                    continue;
                }
                if (client.Connected)//判断该客户端的状态
                {
                    client.SendMessage(client.ip + ":" + message);
                }
                else//把已断开连接的客户端放到数组中
                {
                    onConnectedList.Add(client);
                }
            }

            //广播完之后从存储已连接客户端集合中移除掉已断开连接的客户端
            foreach (var temp in onConnectedList)
            {
                clientList.Remove(temp);
            }

            clientList = onConnectedList;
        }

        public void SendMessage(string message)//发送消息
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            clientSocket.Send(data);
        }

        public bool Connected//获取该客户端的状态
        {
            get { return clientSocket.Connected; }
        }
    }
}
