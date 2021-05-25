using Commun.NetWork.Enum;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using ZCS_Common;

namespace Commun.NetWork
{
    /// <summary>
    /// 异步读状态对象
    /// </summary>
    internal class AsyncReadStateObject
    {
        /// <summary>
        /// The event done
        /// </summary>
        public ManualResetEvent eventDone;
        /// <summary>
        /// The stream
        /// </summary>
        public NetworkStream stream;
        /// <summary>
        /// The exception
        /// </summary>
        public Exception exception;
        /// <summary>
        /// The number of bytes read
        /// </summary>
        public Int32 numberOfBytesRead;
    }

    /// <summary>
    /// 异步写状态对象
    /// </summary>
    internal class AsyncWriteStateObject
    {
        /// <summary>
        /// The event done
        /// </summary>
        public ManualResetEvent eventDone;
        /// <summary>
        /// The stream
        /// </summary>
        public NetworkStream stream;
        /// <summary>
        /// The exception
        /// </summary>
        public Exception exception;
    }

    public partial class NetworkStreamPlus : IDisposable
    {
        /// <summary>
        /// 网络数据流，只读字段
        /// </summary>
        public readonly NetworkStream Stream;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="netStream">The net stream.</param>
        public NetworkStreamPlus(NetworkStream netStream)
        {   // 只读字段只能在构造函数中初始化
            Stream = netStream;
            //Stream.ReadTimeout = 1000 * 5;
        }

        /// <summary>
        /// 释放所有托管资源和非托管资源
        /// </summary>
        public void Dispose()
        {
            // 关闭加密传输模块
            //SecurityClose();

            // 请求系统不要调用指定对象的终结器
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~NetworkStreamPlus()
        {
            Dispose();
        }

        /// <summary>
        /// 接收缓冲区大小
        /// </summary>
        public Int32 ReceiveBufferSize = 8 * 1024;


        /// <summary>
        /// 异步接收
        /// </summary>
        /// <param name="data">接收到的字节数组</param>
        /// <exception cref="SocketException"></exception>
        /// <exception cref="TimeoutException"></exception>
        public void Read(out Byte[] data, int startLen = 0, int size = -1)
        {
            // 用户定义对象
            AsyncReadStateObject State = new AsyncReadStateObject
            {
                // 将事件状态设置为非终止状态，导致线程阻止
                eventDone = new ManualResetEvent(false),
                stream = Stream,
                exception = null,
                numberOfBytesRead = -1
            };

            Byte[] Buffer = new Byte[ReceiveBufferSize];

            //取缓存中粘包数据
            Byte[] cacheBytes = DataCache.GetCache("cacheBytes") as Byte[];

            using (MemoryStream memStream = new MemoryStream(ReceiveBufferSize))
            {
                Int32 TotalBytes = 0;       // 总共需要接收的字节数
                Int32 ReceivedBytes = 0;    // 当前已接收的字节数
                Int32 cacheLen = 0;
                while (true)
                {
                    // 将事件状态设置为非终止状态，导致线程阻止
                    State.eventDone.Reset();

                    //判断是缓存中是否有粘包数据
                    if (cacheBytes != null)
                    {
                        //判断是否是包头，并包头信息是否完整 
                        if (TotalBytes == 0 && cacheBytes.Length > 4)
                        {
                            int touLen = BitConverter.ToInt32(cacheBytes, 0);

                            //判断是否是完整的一条内容，是的跳出返回结里
                            if (cacheBytes.Length >= touLen)
                            {
                                TotalBytes = touLen;
                                memStream.Write(cacheBytes, 4, cacheBytes.Length - 4);
                                break;
                            }
                        }
                    }

                    int readSize = size < 1 ? Buffer.Length : size;

                    // 异步读取网络数据流
                    Stream.BeginRead(Buffer, startLen, readSize, new AsyncCallback(AsyncReadCallback), State);

                    // 等待操作完成信号
                    if (State.eventDone.WaitOne(Stream.ReadTimeout, false))
                    {   // 接收到信号
                        if (State.exception != null)
                        {
                            throw State.exception;
                        }

                        if (State.numberOfBytesRead == 0)
                        {
                            // 连接已经断开
                            throw new SocketException();
                        }
                        else if (State.numberOfBytesRead > 0)
                        {
                            //包头信息
                            if (TotalBytes == 0)
                            {
                                Byte[] tempbytes;

                                //缓存的不完整信息加载进新流内容前边
                                if (cacheBytes != null)
                                {
                                    cacheLen = cacheBytes.Length;

                                    tempbytes = new Byte[cacheBytes.Length + Buffer.Length];

                                    Array.Copy(cacheBytes, tempbytes, cacheBytes.Length);
                                    Array.Copy(Buffer, 0, tempbytes, cacheBytes.Length, Buffer.Length);
                                }
                                else
                                {
                                    tempbytes = Buffer;
                                }

                                // 提取流头部字节长度信息
                                TotalBytes = BitConverter.ToInt32(tempbytes, 0);

                                // 保存剩余信息
                                memStream.Write(tempbytes, 0, State.numberOfBytesRead + cacheLen /*- 4*/);
                            }
                            else
                            {
                                memStream.Write(Buffer, 0, State.numberOfBytesRead);
                            }

                            ReceivedBytes += State.numberOfBytesRead + cacheLen;

                            break;
                            //if (ReceivedBytes >= /*TotalBytes*/ Buffer.Length)
                            //{
                            //    break;
                            //}
                        }
                    }
                    else
                    {   // 超时异常
                        throw new TimeoutException();
                    }
                }

                // 将流内容写入字节数组
                //if (String.IsNullOrEmpty(_secretKey))
                //{
                byte[] data1 = (memStream.Length > 0) ? memStream.ToArray() : null;

                if (cacheBytes != null)
                {
                    cacheBytes = null;
                }

                Int32 infoLen = TotalBytes - 4;
                Byte[] resultbytes = new Byte[infoLen];

                //取得的流信息超出，包头中标识的长度，处理信息并缓存多出内容
                if (data1.Length > infoLen)
                {
                    Array.Copy(data1, 0, resultbytes, 0, infoLen);
                    cacheBytes = new byte[data1.Length - infoLen];
                    Array.Copy(data1, TotalBytes - 4, cacheBytes, 0, data1.Length - infoLen);

                }
                else
                {
                    resultbytes = data1;
                }

                DataCache.SetCache("cacheBytes", cacheBytes);

                data = resultbytes;
                // LogUtil.WriteInfoLog(TotalBytes.ToString() + ":" + Encoding.GetEncoding(65001).GetString(data), "MessLog");

                //}
                //else
                //{   // 进行数据解密
                //    data = (memStream.Length > 0) ? Decrypt(memStream.ToArray(), 0, TotalBytes - 4) : null;
                //}
            }
        }

        /// <summary>
        /// 异步接收
        /// </summary>
        /// <param name="answer">接收到的字符串</param>
        /// <param name="codePage">编码格式</param>
        public void Read(out String answer, Int32 codePage = (int)EncodeType.UTF8)
        {
            Byte[] data;
            Read(out data);
            answer = (data != null) ? Encoding.GetEncoding(codePage).GetString(data) : null;
        }

        /// <summary>
        /// 异步读取回调函数
        /// </summary>
        /// <param name="ar">异步操作结果</param>
        private static void AsyncReadCallback(IAsyncResult ar)
        {
            AsyncReadStateObject State = ar.AsyncState as AsyncReadStateObject;
            try
            {   // 异步写入结束
                State.numberOfBytesRead = State.stream.EndRead(ar);
            }

            catch (Exception e)
            {   // 异步连接异常
                State.exception = e;
            }

            finally
            {   // 将事件状态设置为终止状态，线程继续
                State.eventDone.Set();
            }
        }



        /// <summary>
        /// 异步发送
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="offset">起始偏移量</param>
        /// <param name="size">字节数</param>
        /// <exception cref="TimeoutException"></exception>
        public void Write(Byte[] buffer, Int32 offset, Int32 size)
        {
            // 用户定义对象
            AsyncWriteStateObject State = new AsyncWriteStateObject
            {   // 将事件状态设置为非终止状态，导致线程阻止
                eventDone = new ManualResetEvent(false),
                stream = Stream,
                exception = null,
            };

            Byte[] BytesArray;
            //if (String.IsNullOrEmpty(_secretKey))
            //{   // 在数据前插入长度信息
            //Int32 Length = size + 4;    // 加入4字节长度信息后的总长度
            //BytesArray = new Byte[Length];
            //Array.Copy(BitConverter.GetBytes(Length), BytesArray, 4);
            //Array.Copy(buffer, offset, BytesArray, 4, size);


            BytesArray = new Byte[size];
            Array.Copy(buffer, offset, BytesArray, 0, size);
            //}
            //else
            //{   // 数据加密
            //    Byte[] Cipher = Encrypt(buffer, offset, size);

            //    // 在数据前插入长度信息
            //    Int32 Length = Cipher.Length + 4;
            //    BytesArray = new Byte[Length];
            //    Array.Copy(BitConverter.GetBytes(Length), BytesArray, 4);
            //    Array.Copy(Cipher, 0, BytesArray, 4, Cipher.Length);
            //}

            // 写入加长度信息头的数据
            Stream.BeginWrite(BytesArray, 0, BytesArray.Length, new AsyncCallback(AsyncWriteCallback), State);

            // 等待操作完成信号
            if (State.eventDone.WaitOne(Stream.WriteTimeout, false))
            {   // 接收到信号
                if (State.exception != null)
                {
                    throw State.exception;
                }
            }
            else
            {   // 超时异常
                throw new TimeoutException();
            }
        }

        /// <summary>
        /// 异步发送
        /// </summary>
        /// <param name="data">字节数组</param>
        public void Write(Byte[] data)
        {
            Write(data, 0, data.Length);
        }

        /// <summary>
        /// 异步发送
        /// </summary>
        /// <param name="command">字符串</param>
        /// <param name="codePage">编码格式</param>      
        public void Write(String command, Int32 codePage = (int)EncodeType.UTF8)
        {
            Write(Encoding.GetEncoding(codePage).GetBytes(command));
        }

        /// <summary>
        /// 异步写入回调函数
        /// </summary>
        /// <param name="ar">异步操作结果</param>
        private static void AsyncWriteCallback(IAsyncResult ar)
        {
            AsyncWriteStateObject State = ar.AsyncState as AsyncWriteStateObject;
            try
            {   // 异步写入结束
                State.stream.EndWrite(ar);
            }

            catch (Exception e)
            {   // 异步连接异常
                State.exception = e;
            }

            finally
            {   // 将事件状态设置为终止状态，线程继续
                State.eventDone.Set();
            }
        }
    }
}
