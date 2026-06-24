using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ZCS_Common;

namespace Commun.NetWork
{
    public class TcpMsgManager
    {
        /// <summary>
        /// The _TCP c
        /// </summary>
        public static TcpClient _tcpC = new TcpClient();

        /// <summary>
        /// The islink error
        /// </summary>
        public static bool IslinkError = false;

        /// <summary>
        /// 是否采用Json格式的收发
        /// </summary>
        public static bool isRevJson = false;


        /// <summary>
        /// 启动链接TCP
        /// </summary>
        /// <param name="IP">The ip.</param>
        /// <param name="Prot">The prot.</param>
        public static void TcpClientConnect(string IP, int Prot)
        {
            TcpClientConnect(_tcpC, IP, Prot);
        }


        /// <summary>
        /// 启动链接TCP
        /// </summary>
        /// <param name="_tcp">The _TCP.</param>
        /// <param name="IP">The ip.</param>
        /// <param name="Prot">The prot.</param>
        public static void TcpClientConnect(TcpClient _tcp, string IP, int Prot)
        {
            try
            {
                if (!_tcp.Connected)
                {
                    _tcp.Connect(IPAddress.Parse(IP), Prot);
                }
            }
            catch (Exception)
            {
                IslinkError = true;
                _tcp.Close();
                _tcpC = new TcpClient();
                throw;
            }

        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public static void TcpClientClose()
        {
            TcpClientClose(_tcpC);
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <param name="_tcp">The _TCP.</param>
        public static void TcpClientClose(TcpClient _tcp)
        {
            try
            {
                if ((_tcp != null) && (_tcp.Connected))
                {
                    _tcp.Close();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex, "TcpClientClose");
            }
        }

        /// <summary>
        /// 接受信息
        /// </summary>
        /// <param name="type">MessageInfo类型 typeof（MessageInfo）</param>
        /// <returns>System.Object.</returns>
        public static object GetMessage(Type type)
        {
            return GetMessage(_tcpC, type);
        }

        /// <summary>
        /// 接受信息
        /// </summary>
        /// <param name="_tcp">The _TCP.</param>
        /// <param name="type">MessageInfo类型 typeof（MessageInfo）</param>
        /// <returns>System.Object.</returns>
        public static object GetMessage(TcpClient _tcp, Type type)
        {
            try
            {
                if (!IslinkError)
                {
                    NetworkStream networkStream = _tcp.GetStream();
                    //获取回传信息
                    //byte[] messageByte = new byte[2048];
                    //int n = 0;
                    string readStr = string.Empty;
                    lock (networkStream)
                    {
                        NetworkStreamPlus networkStreamP = new NetworkStreamPlus(networkStream);
                        networkStreamP.Read(out readStr);
                    }

                    if (isRevJson)
                    {
                        //根据输入的type对象，把二进制信息转化为对应的对象
                        string jsonMessage = readStr;

                        try
                        {
                            object returnValue = JsonConvert.DeserializeObject(jsonMessage, type);

                            //if (JsonSplit.IsJson(jsonMessage))
                            //{
                            //    returnValue = JsonConvert.DeserializeObject(jsonMessage, type);
                            //}
                            //else
                            //{
                            //    returnValue = jsonMessage;
                            //}
                            return returnValue;
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteException(ex, "json:" + jsonMessage);
                            throw new Exception("infoErr");
                        }
                    }
                    else
                    {
                        return Encoding.Default.GetString(readStr.ToBytes());
                    }

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message == "infoErr")
                {
                    //throw new Exception("接收到的内容不正确！请与服务端联系");
                }
                else
                {
                    IslinkError = true;
                }
                return null;
            }
        }

        public static byte[] Receive(int startLen, int size)
        {
            byte[] data;
            try
            {
                if (!IslinkError)
                {
                    NetworkStream networkStream = _tcpC.GetStream();
                    lock (networkStream)
                    {
                        NetworkStreamPlus networkStreamP = new NetworkStreamPlus(networkStream);
                        networkStreamP.Read(out data, startLen, size);
                    }
                    return data;
                }
                return null;
            }
            catch (Exception ex)
            {
                IslinkError = true;
                LogHelper.WriteException(ex);
                return null;
            }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="message">发送内容</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool SendMessage(object message)
        {
            return SendMessage(_tcpC, message);
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="_tcp">The _TCP.</param>
        /// <param name="message">发送内容</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool SendMessage(TcpClient _tcp, object message)
        {
            try
            {
                if (!IslinkError)
                {
                    NetworkStream networkStream = _tcp.GetStream();
                    //利用Netonsoft.Json工具集将MessageEntity对象实现序列化，发送到服务器
                    //DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(message.GetType());

                    MemoryStream stream = new MemoryStream();
                    //jsonSerializer.WriteObject(stream, message);

                    stream.Write((byte[])message, 0, ((byte[])message).Length);
                    byte[] jsonbyte = stream.ToArray();
                    Int32 size = jsonbyte.Length;
                    Int32 Length = size /*+ 4*/;
                    Byte[] BytesArray = new Byte[Length];



                    Array.Copy(BitConverter.GetBytes(Length), BytesArray, 0 /*4*/);

                    //Array.Reverse(BytesArray, 0, 4);

                    Array.Copy(jsonbyte, 0, BytesArray, 0 /*4*/, size);

                    #region 测试用
                    //byte[] aa = new byte[Length + Length];
                    //Array.Copy(BytesArray, 0, aa, 0, Length);
                    //Array.Copy(BytesArray, 0, aa, Length, Length);
                    //networkStream.Write(aa, 0, aa.Length);
                    #endregion


                    NetworkStreamPlus networkStreamP = new NetworkStreamPlus(networkStream);


                    //如果文件超大 那么分拨发送
                    int send_size = 8 * 1024;
                    if (BytesArray.Length > send_size)
                    {
                        bool isSend = true;
                        int sended = 0;//已经发送的
                        while (isSend)
                        {
                            //如果未发送的超过8*1024
                            if (BytesArray.Length - sended > send_size)
                            {
                                networkStreamP.Write(BytesArray, sended, send_size);
                                sended += send_size;
                            }
                            else
                            {
                                networkStreamP.Write(BytesArray, sended, BytesArray.Length - sended);
                                isSend = false;
                            }
                        }
                    }
                    else
                    {
                        //直接发送
                        networkStreamP.Write(BytesArray, 0, BytesArray.Length);
                    }

                    Console.WriteLine();
                    //networkStream.Write(BytesArray, 0, BytesArray.Length);
                    //networkStream.Flush();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                IslinkError = true;

                LogHelper.WriteException(ex);
                return false;
                throw;
            }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="_tcp">The _TCP.</param>
        /// <param name="message">发送内容</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool SendMessage(TcpClient _tcp, byte[] messagebyte)
        {
            try
            {
                if (!IslinkError)
                {
                    NetworkStream networkStream = _tcp.GetStream();

                    NetworkStreamPlus networkStreamP = new NetworkStreamPlus(networkStream);

                    //如果文件超大 那么分拨发送
                    int send_size = 8 * 1024;
                    if (messagebyte.Length > send_size)
                    {
                        bool isSend = true;
                        int sended = 0;//已经发送的
                        while (isSend)
                        {
                            //如果未发送的超过8*1024
                            if (messagebyte.Length - sended > send_size)
                            {
                                networkStreamP.Write(messagebyte, sended, send_size);
                                sended += send_size;
                            }
                            else
                            {
                                networkStreamP.Write(messagebyte, sended, messagebyte.Length - sended);
                                isSend = false;
                            }
                        }
                    }
                    else
                    {
                        //直接发送
                        networkStreamP.Write(messagebyte, 0, messagebyte.Length);
                    }


                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                IslinkError = true;
                LogHelper.WriteException(ex);

                return false;
                throw;
            }
        }
    }
}
