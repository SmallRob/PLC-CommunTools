using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commun.NetWork.ModbusTCP
{
    public class ModbusTcpHelper
    {
        #region 变量定义
        /// <summary>
        /// 与PLC通信的socket客户端
        /// </summary>
        private Socket socket;
        /// <summary>
        /// 是否已连接上PLC，true：已连接上PLC false：未连接
        /// </summary>
        public bool isConnectPLC = false;
        #endregion

        #region 连接PLC
        /// <summary>
        /// 连接PLC,异步连接
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool ConnectPLC(string ip, int port)
        {
            isConnectPLC = false;
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IAsyncResult asyncResult = socket.BeginConnect(ip, port, CallbackConnect, socket);
                asyncResult.AsyncWaitHandle.WaitOne();
                socket.ReceiveTimeout = 2000;//2000ms无数据接收则超时
                Thread.Sleep(600);//异步连接，等待状态返回
                return isConnectPLC;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("连接Modbus_TCP失败：" + ex.Message);
                isConnectPLC = false;
            }
            return isConnectPLC;
        }
        #endregion

        #region 异步连接PLC
        /// <summary>
        /// 异步连接PLC
        /// </summary>
        /// <param name="ar"></param>
        private void CallbackConnect(IAsyncResult ar)
        {
            isConnectPLC = false;
            try
            {
                Socket skt = ar.AsyncState as Socket;
                skt.EndConnect(ar);
                isConnectPLC = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("连接Modbus_TCP失败：" + ex.Message);
                System.Diagnostics.Debug.WriteLine("连接Modbus_TCP失败：" + ex.Message);
            }
        }
        #endregion

        #region 关闭套接字连接
        /// <summary>
        /// 关闭套接字连接
        /// </summary>
        public void CloseConnect()
        {
            if (this.socket != null)
            {
                try
                {
                    this.socket.Close(1000);
                    isConnectPLC = false;
                }
                catch { }
            }
        }
        #endregion

        #region 保持寄存器操作   寄存器范围40001~49999
        #region 读取单个保持寄存器值【40001~49999注意高低位】
        /// <summary>
        /// 读取单个保持寄存器值【40001~49999注意高低位】
        /// </summary>
        /// <typeparam name="T">基本的数据类型，如short，int，double等</typeparam>
        /// <param name="startAddress">起始地址</param>
        /// <param name="value">返回的具体指</param>
        /// <returns>true：读取成功 false：读取失败</returns>
        public bool ReadSingleHoldingRegisterValue<T>(int startAddress, out T value)
        {
            value = default(T);
            if (socket == null || !socket.Connected)
            {
                System.Diagnostics.Debug.WriteLine("socket为空或者尚未建立与PLC_Modbus的连接...");
                return false;
            }
            if (startAddress < 0 || startAddress > 65535)
            {
                System.Diagnostics.Debug.WriteLine("Modbus的起始地址必须在0~65535之间");
                return false;
            }
            byte[] addrArray = BitConverter.GetBytes((ushort)startAddress);
            byte wordLength = 0;//读取的地址个数【多少个字Word】 int，float需要两个字 long,double需要四个字
            if (typeof(T) == typeof(sbyte) || typeof(T) == typeof(byte) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort))
            {
                wordLength = 1;
            }
            else if (typeof(T) == typeof(int) || typeof(T) == typeof(uint) || typeof(T) == typeof(float))
            {
                wordLength = 2;
            }
            else if (typeof(T) == typeof(long) || typeof(T) == typeof(ulong) || typeof(T) == typeof(double))
            {
                wordLength = 4;
            }
            else
            {
                //暂不考虑 char(就是ushort，两个字节)，decimal（十六个字节）等类型
                System.Diagnostics.Debug.WriteLine("读Modbus数据暂不支持其他类型：" + value.GetType());
                return false;
            }
            byte[] sendBuffer = new byte[12] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x03, addrArray[1], addrArray[0], 0x00, wordLength };
            socket.Send(sendBuffer);

            DisplayBuffer(sendBuffer, sendBuffer.Length, true);
            Thread.Sleep(50);//等待50ms

            byte[] receiveBuffer = new byte[1024];
            try
            {
                //协议错误时 Receive函数将发生异常
                int receiveCount = socket.Receive(receiveBuffer);
                DisplayBuffer(receiveBuffer, receiveCount, false);
                //receiveBuffer[8] : 真实数据的字节流总个数
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("接收Modbus的响应数据异常，请查看发送的报文格式是否有误：" + ex.Message);
                return false;
            }

            if (typeof(T) == typeof(sbyte))
            {
                byte b = receiveBuffer[10];
                sbyte sb = (sbyte)b;
                value = (T)(object)sb;
            }
            else if (typeof(T) == typeof(byte))
            {
                byte b = receiveBuffer[10];
                value = (T)(object)b;
            }
            else if (typeof(T) == typeof(short))
            {
                short s = BitConverter.ToInt16(new byte[] { receiveBuffer[10], receiveBuffer[9] }, 0);
                value = (T)(object)s;
            }
            else if (typeof(T) == typeof(ushort))
            {
                ushort us = BitConverter.ToUInt16(new byte[] { receiveBuffer[10], receiveBuffer[9] }, 0);
                value = (T)(object)us;
            }
            else if (typeof(T) == typeof(int))
            {
                int i = BitConverter.ToInt32(new byte[] { receiveBuffer[12], receiveBuffer[11], receiveBuffer[10], receiveBuffer[9] }, 0);
                value = (T)(object)i;
            }
            else if (typeof(T) == typeof(uint))
            {
                uint ui = BitConverter.ToUInt32(new byte[] { receiveBuffer[12], receiveBuffer[11], receiveBuffer[10], receiveBuffer[9] }, 0);
                value = (T)(object)ui;
            }
            else if (typeof(T) == typeof(long))
            {
                long l = BitConverter.ToInt64(new byte[] { receiveBuffer[16], receiveBuffer[15], receiveBuffer[14], receiveBuffer[13], receiveBuffer[12], receiveBuffer[11], receiveBuffer[10], receiveBuffer[9] }, 0);
                value = (T)(object)l;
            }
            else if (typeof(T) == typeof(ulong))
            {
                ulong ul = BitConverter.ToUInt64(new byte[] { receiveBuffer[16], receiveBuffer[15], receiveBuffer[14], receiveBuffer[13], receiveBuffer[12], receiveBuffer[11], receiveBuffer[10], receiveBuffer[9] }, 0);
                value = (T)(object)ul;
            }
            else if (typeof(T) == typeof(float))
            {
                float f = BitConverter.ToSingle(new byte[] { receiveBuffer[12], receiveBuffer[11], receiveBuffer[10], receiveBuffer[9] }, 0);
                value = (T)(object)f;
            }
            else if (typeof(T) == typeof(double))
            {
                double d = BitConverter.ToDouble(new byte[] { receiveBuffer[16], receiveBuffer[15], receiveBuffer[14], receiveBuffer[13], receiveBuffer[12], receiveBuffer[11], receiveBuffer[10], receiveBuffer[9] }, 0);
                value = (T)(object)d;
            }
            return true;
        }
        #endregion
        #region 读取多个保持寄存器值【40001~49999注意高低位】
        /// <summary>
        /// 读取多个保持寄存器值【40001~49999注意高低位】
        /// </summary>
        /// <param name="startAddress">起始寄存器地址%MW startAddr</param>
        /// <param name="length">读取的字节个数</param>
        /// <param name="value">返回的字节流数据</param>
        /// <returns>true：读取成功 false：读取失败</returns>
        public bool ReadManyHoldingRegisterValue(int startAddress, int length, out byte[] value)
        {
            value = new byte[length];
            if (socket == null || !socket.Connected)
            {
                System.Diagnostics.Debug.WriteLine("socket为空或者尚未建立与PLC_Modbus的连接...");
                return false;
            }
            //读保持寄存器0x03读取的寄存器数量的范围为 1~125。因一个寄存器【一个Word】存放两个字节，因此 字节数组的长度范围 为 1~250
            if (length < 1 || length > 250)
            {
                System.Diagnostics.Debug.WriteLine("返回的字节数组的长度范围为 1~250");
                return false;
            }
            if (startAddress < 0 || startAddress > 65535)
            {
                System.Diagnostics.Debug.WriteLine("Modbus的起始地址必须在0~65535之间");
                return false;
            }
            byte[] addrArray = BitConverter.GetBytes((ushort)startAddress);
            //读取的寄存器个数： 如果length为偶数 则为 length/2 如果length为奇数，则为(length+1)/2。因整数相除，结果不考虑余数，所以如下通用：
            byte registerCount = (byte)((length + 1) / 2);
            byte[] sendBuffer = new byte[12] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x03, addrArray[1], addrArray[0], 0x00, registerCount };
            socket.Send(sendBuffer);

            DisplayBuffer(sendBuffer, sendBuffer.Length, true);
            Thread.Sleep(50);//等待50ms

            byte[] receiveBuffer = new byte[1024];
            try
            {
                int receiveCount = socket.Receive(receiveBuffer);
                DisplayBuffer(receiveBuffer, receiveCount, false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("接收Modbus的响应数据异常，请查看发送的报文格式是否有误：" + ex.Message);
                return false;
            }
            //接收到的实际数据字节个数
            byte receiveLength = receiveBuffer[8];
            if (receiveLength != registerCount * 2)
            {
                System.Diagnostics.Debug.WriteLine("解析接收数据非法，接收的实际数据长度【不是】读取寄存器数量的2倍");
                return false;
            }
            value = new byte[receiveLength];
            for (int i = 0; i < receiveLength; i++)
            {
                value[i] = receiveBuffer[9 + i];
            }
            return true;
        }
        #endregion
        #region 写单个保持寄存器【40001~49999注意高低位】
        /// <summary>
        /// 写单个保持寄存器【40001~49999注意高低位】
        /// </summary>
        /// <typeparam name="T">基本的数据类型，如short，int，double等</typeparam>
        /// <param name="startAddress">寄存器起始地址，范围：【0x0000~0xFFFF】</param>
        /// <param name="value">写入的值</param>
        /// <returns>true：写入成功 false：写入失败</returns>
        public bool WriteSingleHoldingRegisterValue<T>(int startAddress, T value)
        {
            if (socket == null || !socket.Connected)
            {
                System.Diagnostics.Debug.WriteLine("socket为空或者尚未建立与PLC_Modbus的连接...");
                return false;
            }
            if (startAddress < 0 || startAddress > 65535)
            {
                System.Diagnostics.Debug.WriteLine("Modbus的起始地址必须在0~65535之间");
                return false;
            }
            byte[] addrArray = BitConverter.GetBytes((ushort)startAddress);
            //sbyte，byte，short，ushort 占用一个寄存器（Word）范围的可以使用功能码0x06：写单个寄存器
            //int,long,float,double 需要使用两个或两个以上寄存器，因此只能使用功能码0x10：写多个寄存器 其中int，uint，float占用两个寄存器 long，ulong，double占用四个寄存器
            byte[] buffer = new byte[12] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, addrArray[1], addrArray[0], 0x00, 0x00 };
            if (typeof(T) == typeof(sbyte))
            {
                sbyte sb = Convert.ToSByte(value);
                byte b = (byte)sb;
                buffer[11] = b;
            }
            else if (typeof(T) == typeof(byte))
            {
                byte b = Convert.ToByte(value);
                buffer[11] = b;
            }
            else if (typeof(T) == typeof(short))
            {
                short s = Convert.ToInt16(value);
                byte[] writeValueArray = BitConverter.GetBytes(s);
                buffer[10] = writeValueArray[1];
                buffer[11] = writeValueArray[0];
            }
            else if (typeof(T) == typeof(ushort))
            {
                ushort us = Convert.ToUInt16(value);
                byte[] writeValueArray = BitConverter.GetBytes(us);
                buffer[10] = writeValueArray[1];
                buffer[11] = writeValueArray[0];
            }
            else if (typeof(T) == typeof(int))
            {
                int i = Convert.ToInt32(value);
                byte[] writeValueArray = BitConverter.GetBytes(i);
                buffer = new byte[17] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x0B, 0x01, 0x10, addrArray[1], addrArray[0], 0x00, 0x02, 0x04, 0x00, 0x00, 0x00, 0x00 };
                buffer[13] = writeValueArray[3];
                buffer[14] = writeValueArray[2];
                buffer[15] = writeValueArray[1];
                buffer[16] = writeValueArray[0];
            }
            else if (typeof(T) == typeof(uint))
            {
                uint ui = Convert.ToUInt32(value);
                byte[] writeValueArray = BitConverter.GetBytes(ui);
                buffer = new byte[17] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x0B, 0x01, 0x10, addrArray[1], addrArray[0], 0x00, 0x02, 0x04, 0x00, 0x00, 0x00, 0x00 };
                buffer[13] = writeValueArray[3];
                buffer[14] = writeValueArray[2];
                buffer[15] = writeValueArray[1];
                buffer[16] = writeValueArray[0];
            }
            else if (typeof(T) == typeof(long))
            {
                long l = Convert.ToInt64(value);
                byte[] writeValueArray = BitConverter.GetBytes(l);
                buffer = new byte[21] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x0F, 0x01, 0x10, addrArray[1], addrArray[0], 0x00, 0x04, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                buffer[13] = writeValueArray[7];
                buffer[14] = writeValueArray[6];
                buffer[15] = writeValueArray[5];
                buffer[16] = writeValueArray[4];
                buffer[17] = writeValueArray[3];
                buffer[18] = writeValueArray[2];
                buffer[19] = writeValueArray[1];
                buffer[20] = writeValueArray[0];
            }
            else if (typeof(T) == typeof(ulong))
            {
                ulong ul = Convert.ToUInt64(value);
                byte[] writeValueArray = BitConverter.GetBytes(ul);
                buffer = new byte[21] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x0F, 0x01, 0x10, addrArray[1], addrArray[0], 0x00, 0x04, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                buffer[13] = writeValueArray[7];
                buffer[14] = writeValueArray[6];
                buffer[15] = writeValueArray[5];
                buffer[16] = writeValueArray[4];
                buffer[17] = writeValueArray[3];
                buffer[18] = writeValueArray[2];
                buffer[19] = writeValueArray[1];
                buffer[20] = writeValueArray[0];
            }
            else if (typeof(T) == typeof(float))
            {
                float f = Convert.ToSingle(value);
                byte[] writeValueArray = BitConverter.GetBytes(f);
                buffer = new byte[17] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x0B, 0x01, 0x10, addrArray[1], addrArray[0], 0x00, 0x02, 0x04, 0x00, 0x00, 0x00, 0x00 };
                buffer[13] = writeValueArray[3];
                buffer[14] = writeValueArray[2];
                buffer[15] = writeValueArray[1];
                buffer[16] = writeValueArray[0];
            }
            else if (typeof(T) == typeof(double))
            {
                double d = Convert.ToDouble(value);
                byte[] writeValueArray = BitConverter.GetBytes(d);
                buffer = new byte[21] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x0F, 0x01, 0x10, addrArray[1], addrArray[0], 0x00, 0x04, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                buffer[13] = writeValueArray[7];
                buffer[14] = writeValueArray[6];
                buffer[15] = writeValueArray[5];
                buffer[16] = writeValueArray[4];
                buffer[17] = writeValueArray[3];
                buffer[18] = writeValueArray[2];
                buffer[19] = writeValueArray[1];
                buffer[20] = writeValueArray[0];
            }
            else
            {
                //暂不考虑 char(就是ushort，两个字节)，decimal（十六个字节）等类型
                System.Diagnostics.Debug.WriteLine("写Modbus数据暂不支持其他类型：" + value.GetType());
                return false;
            }
            try
            {
                socket.Send(buffer);
                DisplayBuffer(buffer, buffer.Length, true);
                Thread.Sleep(50);//等待50ms
                byte[] receiveBuffer = new byte[1024];
                int receiveCount = socket.Receive(receiveBuffer);
                DisplayBuffer(receiveBuffer, receiveCount, false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("接收Modbus的响应数据异常，请查看发送的报文格式是否有误：" + ex.Message);
                return false;
            }
            return true;
        }
        #endregion
        #region  写多个保持寄存器的值【40001~49999需要注意高低位】
        /// <summary>
        /// 写多个保持寄存器的值【40001~49999需要注意高低位】
        /// </summary>
        /// <param name="startAddress">起始地址</param>
        /// <param name="buffer">要写入的字节数组，buffer数组长度范围：【1~240（0x01~0xF0）】</param>
        /// <returns>true：写入成功 false：写入失败</returns>
        public bool WriteManyHoldingRegisterValue(int startAddress, byte[] buffer)
        {
            //分奇数个字节、偶数个字节
            if (socket == null || !socket.Connected)
            {
                System.Diagnostics.Debug.WriteLine("socket为空或者尚未建立与PLC_Modbus的连接...");
                return false;
            }
            if (startAddress < 0 || startAddress > 65535)
            {
                System.Diagnostics.Debug.WriteLine("Modbus的起始地址必须在0~65535之间");
                return false;
            }
            if (buffer == null || buffer.Length < 1 || buffer.Length > 240)
            {
                System.Diagnostics.Debug.WriteLine("写连续寄存器块范围：(1 至120 个寄存器)");//每个寄存器将数据分成两字节
                return false;
            }
            byte[] addrArray = BitConverter.GetBytes((ushort)startAddress);
            //需要写入的寄存器个数
            byte registerCount = (byte)((buffer.Length + 1) / 2);
            //实际写入的字节个数：注意buffer数组的长度为奇数时 需要将最后一个寄存器的高位设置为0
            byte writeCount = (byte)(registerCount * 2);
            byte[] sendBuffer = new byte[13 + writeCount];
            sendBuffer[0] = 0x02;
            sendBuffer[1] = 0x01;
            sendBuffer[5] = (byte)(7 + writeCount);
            sendBuffer[6] = 0x01;
            sendBuffer[7] = 0x10;
            sendBuffer[8] = addrArray[1];
            sendBuffer[9] = addrArray[0];
            sendBuffer[11] = registerCount;
            sendBuffer[12] = writeCount;
            for (int i = 0; i < writeCount - 2; i++)
            {
                sendBuffer[13 + i] = buffer[i];
            }

            //最后两个元素[最后的一个寄存器]的处理
            if (buffer.Length % 2 == 1)
            {
                //如果是奇数个，需要将最后一个寄存器的高位设置为0
                sendBuffer[13 + writeCount - 2] = 0;
                sendBuffer[13 + writeCount - 1] = buffer[buffer.Length - 1];
            }
            else
            {
                //如果是偶数个，则一一对应
                sendBuffer[13 + writeCount - 2] = buffer[buffer.Length - 2];
                sendBuffer[13 + writeCount - 1] = buffer[buffer.Length - 1];
            }

            try
            {
                socket.Send(sendBuffer);
                DisplayBuffer(sendBuffer, sendBuffer.Length, true);
                Thread.Sleep(50);//等待50ms
                byte[] receiveBuffer = new byte[1024];
                int receiveCount = socket.Receive(receiveBuffer);
                DisplayBuffer(receiveBuffer, receiveCount, false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("接收Modbus的响应数据异常，请查看发送的报文格式是否有误：" + ex.Message);
                return false;
            }
            return true;
        }
        #endregion
        #endregion

        #region 线圈状态操作    寄存器范围00001~09999
        #region 读单个线圈状态(位)00001~09999
        /// <summary>
        /// 读单个线圈状态(位)00001~09999
        /// </summary>
        /// <param name="startAddress"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ReadSingleCoilStatus(int startAddress, out bool value)
        {
            bool[] boolArr = new bool[1] { false };
            bool result = ReadManyCoilStatus(startAddress, 1, out boolArr);
            if (boolArr != null && boolArr.Length == 1)
            {
                value = boolArr[0];
            }
            else
            {
                value = false;
            }
            return result;
        }
        #endregion
        #region 读多个线圈状态(位)00001~09999功能码0X01
        /// <summary>
        /// 读多个线圈状态(位)00001~09999功能码0X01
        /// </summary>
        /// <param name="startAddress">起始地址</param>
        /// <param name="length">长度</param>
        /// <param name="value">输出bool型数组</param>
        /// <returns></returns>
        public bool ReadManyCoilStatus(int startAddress, int length, out bool[] value)
        {
            value = new bool[length];
            if (socket == null || !socket.Connected)
            {
                System.Diagnostics.Debug.WriteLine("socket为空或者尚未建立与PLC_Modbus的连接...");
                return false;
            }
            //读线圈状态0x01读取的范围为 1~2000（0X7D0）。因一个线圈【一个位】
            if (length < 1 || length > 2000)
            {
                System.Diagnostics.Debug.WriteLine("返回的字节数组的长度范围为 1~2000");
                return false;
            }
            if (startAddress < 0 || startAddress > 65535)
            {
                System.Diagnostics.Debug.WriteLine("Modbus的起始地址必须在0~65535之间");
                return false;
            }
            byte[] addrArray = BitConverter.GetBytes((ushort)startAddress);
            //读取的线圈状态个数：
            byte[] registerCount = BitConverter.GetBytes(length);
            byte[] sendBuffer = new byte[12] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x01, addrArray[1], addrArray[0], registerCount[1], registerCount[0] };
            socket.Send(sendBuffer);

            DisplayBuffer(sendBuffer, sendBuffer.Length, true);
            Thread.Sleep(50);//等待50ms

            byte[] receiveBuffer = new byte[1024];
            try
            {
                int receiveCount = socket.Receive(receiveBuffer);
                DisplayBuffer(receiveBuffer, receiveCount, false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("接收Modbus的响应数据异常，请查看发送的报文格式是否有误：" + ex.Message);
                return false;
            }
            //接收到的实际数据字节个数
            byte receiveLength = receiveBuffer[8];
            if (receiveLength == 0)
            {
                System.Diagnostics.Debug.WriteLine("解析接收数据非法，接收的实际数据长度不是 读取线圈状态（位）数量");
                return false;
            }
            else
            {
                value = new bool[length];
                byte[] byteNew = new byte[receiveLength];
                for (int i = 0; i < byteNew.Length; i++)
                {
                    byteNew[i] = receiveBuffer[9 + i];
                }
                string strTemp = byteArrToBinaryString(byteNew);
                if (strTemp != "")
                {
                    for (int i = 0; i < length; i++)
                    {
                        value[i] = strTemp.Substring(i, 1) == "1";
                    }
                }
            }
            return true;
        }
        #endregion
        #region  写单个线圈状态（功能码0X05）
        /// <summary>
        /// 写单个线圈状态（功能码0X05）
        /// </summary>
        /// <param name="startAddress">起始地址</param>
        /// <param name="buffer">要写入的整形数组，buffer数组长度范围：【1~1968（0x0001~0x07B0）】</param>
        /// <returns>true：写入成功 false：写入失败</returns>
        public bool WriteSingleCoilStatus(int startAddress, int buffer)
        {
            if (socket == null || !socket.Connected)
            {
                System.Diagnostics.Debug.WriteLine("socket为空或者尚未建立与PLC_Modbus的连接...");
                return false;
            }
            if (startAddress < 0 || startAddress > 9999)
            {
                System.Diagnostics.Debug.WriteLine("Modbus的起始地址必须在0~9999之间");
                return false;
            }
            if (buffer > 1)
            {
                System.Diagnostics.Debug.WriteLine("写线圈状态值范围：(0或者1)");
                return false;
            }
            return WriteManyCoilStatus(startAddress, new int[] { buffer });
        }
        #endregion
        #region  写多个线圈状态（功能码0X15）
        /// <summary>
        /// 写多个线圈状态（功能码0X15）
        /// </summary>
        /// <param name="startAddress">起始地址</param>
        /// <param name="buffer">要写入的整形数组，buffer数组长度范围：【1~1968（0x0001~0x07B0）】</param>
        /// <returns>true：写入成功 false：写入失败</returns>
        public bool WriteManyCoilStatus(int startAddress, int[] buffer)
        {
            if (socket == null || !socket.Connected)
            {
                System.Diagnostics.Debug.WriteLine("socket为空或者尚未建立与PLC_Modbus的连接...");
                return false;
            }
            if (startAddress < 0 || startAddress > 9999)
            {
                System.Diagnostics.Debug.WriteLine("Modbus的起始地址必须在0~9999之间");
                return false;
            }
            if (buffer == null || buffer.Length < 1 || buffer.Length > 1968)
            {
                System.Diagnostics.Debug.WriteLine("写线圈状态范围：(1 至1968 个寄存器)");
                return false;
            }
            for (int i = 0; i < buffer.Length; i++)
            {
                if (buffer[i] > 1)
                {
                    System.Diagnostics.Debug.WriteLine("buffer所有数据值必须为0或者1...");
                    return false;
                }
            }
            byte[] addrArray = BitConverter.GetBytes((ushort)startAddress);
            byte[] lengthArray = BitConverter.GetBytes((ushort)(buffer.Length));
            int intTemp = buffer.Length % 8;
            int byteTemp = buffer.Length / 8;
            intTemp = intTemp > 0 ? 1 : 0;
            //实际写入的线圈状态个数：注意buffer数组的长度
            byte[] sendBuffer = new byte[13 + intTemp + byteTemp];
            sendBuffer[0] = 0x02;
            sendBuffer[1] = 0x01;
            sendBuffer[2] = 0x00;
            sendBuffer[3] = 0x00;
            sendBuffer[4] = 0x00;
            sendBuffer[5] = (byte)(7 + byteTemp + intTemp);
            sendBuffer[6] = 0x01;

            sendBuffer[7] = 0x0F;
            sendBuffer[8] = addrArray[1];
            sendBuffer[9] = addrArray[0];
            sendBuffer[10] = lengthArray[1];
            sendBuffer[11] = lengthArray[0];
            sendBuffer[12] = (byte)(byteTemp + intTemp);


            for (int i = 0; i < intTemp + byteTemp; i++)
            {
                string strTemp = "";
                if (intTemp == 0)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        strTemp += buffer[i * 8 + 7 - j];
                    }
                }
                else
                {
                    if (i < byteTemp)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            strTemp += buffer[i * 8 + 7 - j];
                        }
                    }
                    else
                    {
                        for (int j = 0; j < buffer.Length % 8; j++)
                        {
                            strTemp += buffer[buffer.Length - 1 - j];
                        }
                    }
                }
                sendBuffer[13 + i] = GetByteValueFromBinaryStr(strTemp);
            }
            try
            {
                socket.Send(sendBuffer);
                DisplayBuffer(sendBuffer, sendBuffer.Length, true);
                Thread.Sleep(50);//等待50ms
                byte[] receiveBuffer = new byte[1024];
                int receiveCount = socket.Receive(receiveBuffer);
                DisplayBuffer(receiveBuffer, receiveCount, false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("接收Modbus的响应数据异常，请查看发送的报文格式是否有误：" + ex.Message);
                return false;
            }
            return true;
        }
        #endregion
        #endregion

        #region 输入寄存器操作   寄存器范围30001~39999
        #region 读取多个输入寄存器值【30001~39999注意高低位】
        /// <summary>
        /// 读取多个输入寄存器值【30001~39999注意高低位】
        /// </summary>
        /// <param name="startAddress">起始寄存器地址</param>
        /// <param name="length">读取的字节个数</param>
        /// <param name="value">返回的字节流数据</param>
        /// <returns>true：读取成功 false：读取失败</returns>
        public bool ReadManyInputRegisterValue(int startAddress, int length, out byte[] value)
        {
            value = new byte[length];
            if (socket == null || !socket.Connected)
            {
                System.Diagnostics.Debug.WriteLine("socket为空或者尚未建立与PLC_Modbus的连接...");
                return false;
            }
            //读保持寄存器0x03读取的寄存器数量的范围为 1~125。因一个寄存器【一个Word】存放两个字节，因此 字节数组的长度范围 为 1~250
            if (length < 1 || length > 250)
            {
                System.Diagnostics.Debug.WriteLine("返回的字节数组的长度范围为 1~250");
                return false;
            }
            if (startAddress < 0 || startAddress > 65535)
            {
                System.Diagnostics.Debug.WriteLine("Modbus的起始地址必须在0~65535之间");
                return false;
            }
            byte[] addrArray = BitConverter.GetBytes((ushort)startAddress);
            //读取的寄存器个数： 如果length为偶数 则为 length/2 如果length为奇数，则为(length+1)/2。因整数相除，结果不考虑余数，所以如下通用：
            byte registerCount = (byte)((length + 1) / 2);
            byte[] sendBuffer = new byte[12] { 0x02, 0x01, 0x00, 0x00, 0x00, 0x06, 0x01, 0x04, addrArray[1], addrArray[0], 0x00, registerCount };
            socket.Send(sendBuffer);

            DisplayBuffer(sendBuffer, sendBuffer.Length, true);
            Thread.Sleep(50);//等待50ms

            byte[] receiveBuffer = new byte[1024];
            try
            {
                int receiveCount = socket.Receive(receiveBuffer);
                DisplayBuffer(receiveBuffer, receiveCount, false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("接收Modbus的响应数据异常，请查看发送的报文格式是否有误：" + ex.Message);
                return false;
            }
            //接收到的实际数据字节个数
            byte receiveLength = receiveBuffer[8];
            if (receiveLength != registerCount * 2)
            {
                System.Diagnostics.Debug.WriteLine("解析接收数据非法，接收的实际数据长度【不是】读取寄存器数量的2倍");
                return false;
            }
            value = new byte[receiveLength];
            for (int i = 0; i < receiveLength; i++)
            {
                value[i] = receiveBuffer[9 + i];
            }
            return true;
        }
        #endregion
        #endregion

        #region 读取起始地址开始存储的条码，默认读取最大长度为100的条码字符串
        /// <summary>
        /// 读取起始地址开始存储的条码，默认读取最大长度为100的条码字符串
        /// </summary>
        /// <param name="startAddress">起始地址</param>
        /// <param name="barcode">返回的条码字符串</param>
        /// <returns>true：读取成功 false：读取失败</returns>
        public bool ReadBarcode(int startAddress, out string barcode)
        {
            barcode = string.Empty;
            byte[] dataBuffer = new byte[100];
            bool result = ReadManyHoldingRegisterValue(startAddress, 100, out dataBuffer);
            if (!result)
            {
                return false;
            }
            List<byte> list = new List<byte>();
            for (int i = 0; i < dataBuffer.Length; i += 2)
            {
                //因一个寄存器存储的数据 是一个字Word，分成两个字节Byte【高位字节、低位字节】，存储的条码是低位在前，因此每隔两个需要交换顺序
                list.Add(dataBuffer[i + 1]);
                list.Add(dataBuffer[i]);
                //遇到'\0'后面的数据无效
                if (dataBuffer[i] == 0 || dataBuffer[i + 1] == 0)
                {
                    break;
                }
            }
            byte[] actualBuffer = list.ToArray();
            barcode = Encoding.ASCII.GetString(actualBuffer).Trim('\0').Trim();
            return result;
        }
        #endregion

        #region 打印Debug发送或接收字节数组信息
        /// <summary>
        /// 打印Debug发送或接收字节数组信息
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="count"></param>
        /// <param name="isSend"></param>
        public void DisplayBuffer(byte[] buffer, int count, bool isSend)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((isSend ? "发送" : "接收到") + "的字节流：\n");
            for (int i = 0; i < count; i++)
            {
                if (i > 0)
                {
                    sb.Append(" ");
                }
                sb.Append(buffer[i].ToString("X2"));
            }
            string content = sb.ToString();
            System.Diagnostics.Debug.WriteLine(content);
        }
        #endregion

        #region byte[]Arr转int
        /// <summary>
        /// byte[]Arr转int
        /// </summary>
        /// <param name="byteIn"></param>
        /// <returns></returns>
        private int byteArrToInt(byte[] byteIn)
        {
            int value = 0;
            for (int i = 0; i < 4; i++)
            {
                int shift = (4 - 1 - i) * 8;
                value += (byteIn[i] & 0X000000FF) << shift;
            }
            return value;
        }
        #endregion
        #region byteArr转二进制字符串
        /// <summary>
        /// byte转二进制字符串(高位在前)
        /// </summary>
        /// <param name="byteIn"></param>
        /// <returns></returns>
        private string byteArrToBinaryString(byte[] byteIn)
        {
            string result = "";
            for (int i = 0; i < byteIn.Length; i++)
            {
                string str = Convert.ToString(byteIn[i], 2).PadLeft(8, '0');
                for (int j = 0; j < str.Length; j++)
                {
                    result += str[8 - 1 - j];
                }
            }
            return result;
        }
        #endregion
        #region 用于写位状态线圈时进行字节写入
        private byte GetByteValueFromBinaryStr(string strIn)
        {
            int result = 0;
            if (strIn.Length != 8)
            {
                strIn = strIn.PadLeft(8, '0');
            }
            for (int i = 0; i < strIn.Length; i++)
            {
                int intTemp = int.Parse(strIn.Substring(i, 1));
                result += intTemp * ((int)Math.Pow(2, (7 - i)));
            }
            return (byte)result;
        }
        #endregion
    }

}
