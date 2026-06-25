using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commun.NetWork.MQTT
{
    public class MQTTBase
    {
        static Random random = new Random();

        private string _socketIP;
        private int _socketPort;

        /// <summary>
        /// Socket Host
        /// </summary>
        public string SocketIP
        {
            get => _socketIP; set => _socketIP = value;
        }

        /// <summary>
        /// Socket Port
        /// </summary>
        public int SocketPort
        {
            get => _socketPort; set => _socketPort = value;
        }

        public MQTTBase(string socketIP, int socketPort)
        {
            _socketIP = socketIP;
            _socketPort = socketPort;
        }

        /// <summary>
        /// 链接
        /// </summary>
        public void Connect()
        {
            TcpMsgManager.TcpClientConnect(SocketIP, SocketPort);
            TcpMsgManager.IslinkError = false;

            Console.WriteLine(">> 客户端连接已建立");

            // 发送MQTT的登录请求
            List<byte> byteList = new List<byte>();
            // 固定报头
            // 第一个字节8个位（消息类型4个、DUP1个位，QoS2个位、Retain1个位）
            // 0001 0000
            // 请求连接消息类型，
            byteList.Add(0x10);
            //byteList.Add(0x00);// 可变报头和载荷部分的总字节数

            List<byte> byteList2 = new List<byte>();  // 可变头+载荷
            // 可变报头
            // 协议名称的长度（名称：MQTT）
            string protocolName = "MQTT";
            byte[] proBytes = Encoding.UTF8.GetBytes(protocolName);// MQTT对应的Ascii码的16进制
            // 添加协议名称的长度字节
            byteList2.Add((byte)(proBytes.Length / 256 % 256));// Hi
            byteList2.Add((byte)(proBytes.Length % 256));// Lo
            byteList2.AddRange(proBytes);// 协议名称
            byteList2.Add(0x04);// 协议版本号

            // 连接标志　一个字节
            // 1100 0010
            byte flag = 0; // 0000 0000
            flag |= 128;   // 0000 0000   
                           // 1000 0000
                           // 1000 0000   // 把用户名这一位置1
            flag |= 64;    // 0100 0000
                           // 1100 0000
                           //flag |= 2;     // 0000 0010 // 在做连接请求的时候不能设置
                           // 1100 0010
            byteList2.Add(flag);
            // Keep Alive (时间秒值 )
            int second = 100; // 保持100秒   100*1.5   150秒
                              // 如果客户端没有再次向服务端发送报文的话，服务端可能会把客户端T掉
            byteList2.Add((byte)(second / 256 % 256)); // Hi
            byteList2.Add((byte)(second % 256)); // Lo

            // 有效载荷
            // ClientID  (长度+ID内容)
            string clientIdStr = "Jovan";
            byte[] idBytes = Encoding.UTF8.GetBytes(clientIdStr); // ID的字节数组
            int idLen = idBytes.Length;// ID字节数组的长度
            byteList2.Add((byte)(idLen / 256 % 256));// 长度
            byteList2.Add((byte)(idLen % 256));// 长度
            byteList2.AddRange(idBytes);// 添加ID数组

            // UserName
            string userName = "admin";
            byte[] unBytes = Encoding.UTF8.GetBytes(userName); // UserName的字节数组
            int unLen = unBytes.Length;// UserName字节数组的长度
            byteList2.Add((byte)(unLen / 256 % 256));// 长度
            byteList2.Add((byte)(unLen % 256));// 长度
            byteList2.AddRange(unBytes);// 添加UserName数组

            // Password
            string password = "123456";
            byte[] pwBytes = Encoding.UTF8.GetBytes(password); // UserName的字节数组
            int pwLen = pwBytes.Length;// UserName字节数组的长度
            byteList2.Add((byte)(pwLen / 256 % 256));// 长度
            byteList2.Add((byte)(pwLen % 256));// 长度
            byteList2.AddRange(pwBytes);// 添加UserName数组

            /// 添加固定报头的第二个字节 ：可变报头+载荷的字节数
            byteList.Add((byte)byteList2.Count);
            byteList.AddRange(byteList2);


            // 发送连接请求报文
            TcpMsgManager.SendMessage(byteList.ToArray());

            // 检查连接状态
            // 获取数据的超时处理
            byte[] result = new byte[1];
            result = TcpMsgManager.Receive(0, 1);

            // 0010 0000  16#20   10#32 
            int msgType = ((short)result[0]) >> 4;
            if (msgType == 2)// 属于一个连接确认数据报文
            {
                // 获取payload字节数
                byte[] lenResult = new byte[1];
                lenResult = TcpMsgManager.Receive(0, 1);
                int len = (short)lenResult[0];

                byte[] pyloadResult = new byte[len];
                pyloadResult = TcpMsgManager.Receive(0, 1);

                //socket.Receive(pyloadResult, 0, len, SocketFlags.None);// 把剩下所有字节全部拿到

                // 判断最后一个字节是不是0
                // 如果是0，说明连接请求被接受
                if (pyloadResult[len - 1] == 0)
                {
                    Console.WriteLine(">> MQTT连接成功");

                    // 发送一个心跳
                    // 开一个线程进行无限循环
                    // 发送消息类型为12（心跳请求）的报文
                    List<byte> pingBytes = new List<byte>();
                    // 固定报头
                    pingBytes.Add(0xC0);
                    pingBytes.Add(0x00);

                    _ = Task.Run(async () =>
                      {
                          while (true)
                          {
                              await Task.Delay(2000);

                              TcpMsgManager.SendMessage(pingBytes.ToArray());
                              Console.WriteLine(">> 发送心跳");
                          }
                      });
                }
            }
        }

        /// <summary>
        /// 订阅
        /// </summary>
        public void Subscription()
        {
            List<byte> ssList = new List<byte>();
            // 固定报头
            // 1000 0000
            ssList.Add(0x80);// 订阅请求消息类型
            // 长度先空着，后面再添加

            List<byte> dataBytes = new List<byte>();
            // 主题
            string topic = "test";
            int identifier = random.Next(1, 10_000);

            // 添加标识Identifier(两个字节)
            dataBytes.Add((byte)(identifier / 256 % 256));// 添加标识Identifier
            dataBytes.Add((byte)(identifier % 256));// 添加标识Identifier

            // 添加主题的字节数组
            byte[] topicBytes = Encoding.UTF8.GetBytes(topic); // Topic的字节数组
            int topicLen = topicBytes.Length;// UserName字节数组的长度
            dataBytes.Add((byte)(topicLen / 256 % 256));// 长度
            dataBytes.Add((byte)(topicLen % 256));// 长度
            dataBytes.AddRange(topicBytes);// 添加UserName数组

            // 一个字节   QoS  0/1/2
            dataBytes.Add(0x00);  // 发送的数据不保证一定送到，最多发送一次


            // 组装固定报头和可变载荷
            /// 添加固定报头的第二个字节 ：可变报头+载荷的字节数
            ssList.Add((byte)dataBytes.Count);
            ssList.AddRange(dataBytes);

            TcpMsgManager.SendMessage(ssList.ToArray());
        }

        /// <summary>
        /// 发布
        /// </summary>
        public void Publish()
        {
            List<byte> ssList = new List<byte>();
            // 固定报头
            // 0011 0000
            ssList.Add(0x30);// 订阅请求消息类型
            // 长度先空着，后面再添加

            List<byte> dataBytes = new List<byte>();
            // 主题
            string topic = "test";
            // 消息内容Payload
            string payload = "Hello MQTT!";

            // 添加主题的字节数组
            byte[] topicBytes = Encoding.UTF8.GetBytes(topic); // Topic的字节数组
            int topicLen = topicBytes.Length;// UserName字节数组的长度
            dataBytes.Add((byte)(topicLen / 256 % 256));// 长度
            dataBytes.Add((byte)(topicLen % 256));// 长度
            dataBytes.AddRange(topicBytes);// 添加UserName数组

            // 添加主题的字节数组
            byte[] payloadBytes = Encoding.UTF8.GetBytes(payload); // Topic的字节数组
            int payloadLen = payloadBytes.Length;// UserName字节数组的长度
            //dataBytes.Add((byte)(payloadLen / 256 % 256));// 长度
            //dataBytes.Add((byte)(payloadLen % 256));// 长度
            dataBytes.AddRange(payloadBytes);// 添加UserName数组

            // 组装固定报头和可变载荷
            /// 添加固定报头的第二个字节 ：可变报头+载荷的字节数
            ssList.Add((byte)dataBytes.Count);
            ssList.AddRange(dataBytes);

            TcpMsgManager.SendMessage(ssList.ToArray());

            // 获取数据的超时处理
            byte[] result = new byte[1];
            result = TcpMsgManager.Receive(0, 1);

            // 0011 0000  16#30   10#48 
            int msgType = ((short)result[0]) >> 4;
            if (msgType == 3)// 属于一个发布确认数据报文
            {
                // 获取payload字节数
                byte[] lenResult = new byte[1];
                lenResult = TcpMsgManager.Receive(0, 1);
                int len = (short)lenResult[0];

                byte[] pyloadResult = new byte[len];
                pyloadResult = TcpMsgManager.Receive(0, 1);

                //socket.Receive(pyloadResult, 0, len, SocketFlags.None);// 把剩下所有字节全部拿到

                // 判断最后一个字节是不是0
                // 如果是0，说明连接请求被接受
                if (pyloadResult[len - 1] == 0)
                {
                    Console.WriteLine(">> MQTT发布成功");
                }
            }
        }

    }
}
