using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Commun.NetCore;

namespace CommunWPF.Views
{
    /// <summary>
    /// Interaction logic for ModbusWindow.xaml
    /// </summary>
    public partial class ModbusWindow : Window
    {
        SerialPort serialPortPoll = null;
        SerialPort serialPortSlave = null;

        public ModbusWindow()
        {
            InitializeComponent();

            // 设备列表设备   0
        }

        private List<byte> BaseCommands()
        {
            List<byte> bytes = new List<byte>();
            bytes.Add(0x01);// 从站地址
            //byte funcCode = 0x01;// 
            byte funcCode = 0x03; // 读保持型寄存器
            //byte funcCode = 0x05; // 写单个线圈状态
            //byte funcCode = 0x06; // 保持型寄存器  单个
            //byte funcCode = 0x0F; // 写多个线圈
            //byte funcCode = 0x10; // 写多个保持型寄存器
            //byte funcCode = 0x16; // 
            bytes.Add(funcCode);// 功能码
            bytes.Add(0x00);//  H
            bytes.Add(0x00);// 起始地址L
            return bytes;
        }

        private void OpenSerialPort()
        {
            serialPortPoll = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            serialPortPoll.Open();
        }

        private void ButtonRTU_Click(object sender, RoutedEventArgs e)
        {
            // 服务端-客户端     从站-主站
            // 主站

            //1、串口RTU   串口通信对象  
            //string portName, int baudRate, Parity parity, int dataBits, StopBits stopBit
            this.OpenSerialPort();
            if (!this.serialPortPoll.IsOpen) return;

            // 准备报文
            List<byte> bytes = this.BaseCommands();
            {
                // 读
                int len = 10;
                bytes.Add((byte)(len / 256));//  H    BitConverter     >>
                bytes.Add((byte)(len % 256));// 读取数量L
            }
            {
                // 掩码写多线圈
                //bytes.Add(0xFF);
                //bytes.Add(0xFF);// And_Mask
                //bytes.Add(0x00);
                //bytes.Add(0x25);// And_Mask


                // 写单线圈
                //bool state = false;
                //bytes.Add((byte)(state ? 0xFF : 0x00));//  H    BitConverter     >>
                //bytes.Add(0x00);

                // 写多线圈
                //List<bool> values = new List<bool> { true, true, true, false, false, false, true, true, true, false };
                //bytes.Add((byte)(values.Count / 256));
                //bytes.Add((byte)(values.Count % 256));// 写寄存器数量
                //// "111010101001011"; 
                //// Convert.ToInt32(strValue, 2);
                //{
                //    List<int> valueTemp = new List<int>();
                //    List<int> intValues = new List<int>();
                //    for (int i = 0; i < values.Count; i++)
                //    {
                //        valueTemp.Insert(0, values[i] ? 1 : 0);
                //        if (valueTemp.Count == 8)
                //        {
                //            string strValue = string.Join("", valueTemp.Select(vt => vt.ToString()));
                //            intValues.Insert(0, Convert.ToInt32(strValue, 2));
                //            valueTemp.Clear();
                //        }
                //    }
                //    if (valueTemp.Count > 0)
                //    {
                //        string strValue = string.Join("", valueTemp.Select(iv => iv.ToString()));
                //        intValues.Add(Convert.ToInt32(strValue, 2));
                //    }

                //    bytes.Add((byte)(intValues.Count));// 数据字节数
                //    bytes.AddRange(intValues.Select(iv => Convert.ToByte(iv)).ToList());// 
                //}


                // 利用数据公式的方式重新计算，不借助底层类来处理了
                // 验证有效
                {
                    // 第一种方式  
                    //List<int> intValues = new List<int>();

                    //int sum = 0;
                    //for (int i = 0; i < values.Count(); i++)
                    //{
                    //    if (i > 0 && i % 8 == 0)
                    //    {
                    //        intValues.Add(sum);
                    //        sum = 0;
                    //    }
                    //    if (values[i])
                    //    {
                    //        sum += (int)Math.Pow(2, i % 8);
                    //    }
                    //}
                    //if (sum > 0)
                    //    intValues.Add(sum);

                    //List<byte> commandBytes = new List<byte>();
                    //foreach (var item in intValues)
                    //{
                    //    commandBytes.Add(Convert.ToByte(item));
                    //}
                    //bytes.Add((byte)(commandBytes.Count));// 数据字节数
                    //bytes.AddRange(commandBytes);// 
                }
                {
                    // 第二种方式
                    //List<byte> boolBytes = new List<byte>();
                    //byte valueTemp = 0;
                    //byte start = 128;
                    //while (values.Count % 8 != 0)
                    //    values.Add(false);
                    //for (int i = values.Count - 1; i >= 0; i--)
                    //{   1111  1111
                    //    if (values[i]) valueTemp |= start;   // 或等于   C= A|C  =》  C|=A
                    //    start /= 2;
                    //    if (i < values.Count - 1 && i % 8 == 0)
                    //    {
                    //        boolBytes.Add(valueTemp);
                    //        valueTemp = 0; start = 128;
                    //    }
                    //}
                    //if (valueTemp > 0) boolBytes.Add(valueTemp);
                    //boolBytes.Reverse();
                    //bytes.Add((byte)(boolBytes.Count));// 数据字节数
                    //bytes.AddRange(boolBytes);// 
                }


                // 字节拆开   写哪一个位   设置哪个位

                // 写保持型寄存器 单精度
                //short value = 123;
                //bytes.Add(BitConverter.GetBytes(value)[1]);//  H
                //bytes.Add(BitConverter.GetBytes(value)[0]);  //1

                // 写保持型寄存器  float

                //bytes.Add(0x00);// 起始地址L
                //bytes.Add(0x02);// 起始地址L
                //bytes.Add(0x04);// 起始地址L

                //float value = 123.456f;  //2
                //bytes.Add(BitConverter.GetBytes(value)[3]);//  H
                //bytes.Add(BitConverter.GetBytes(value)[2]);//  H
                //bytes.Add(BitConverter.GetBytes(value)[1]);//  H
                //bytes.Add(BitConverter.GetBytes(value)[0]);
            }
            // CRC
            bytes.AddRange(ModbusValid.CRC16(bytes));

            // 轮询  while(ture){  serialPort.Write(bytes.ToArray(), 0, bytes.Count);  Read();}
            serialPortPoll.Write(bytes.ToArray(), 0, bytes.Count);


            // 报文传输时间的问题

            // 超时
            // 10ms
            int time = 1;
            while (time < 2000 && serialPortPoll.BytesToRead <= 0)
            {
                time += 1;
                //Debug.WriteLine("1");
                Thread.Sleep(1);
            }

            //while (true)
            //{
            //    serialPort.Write(bytes.ToArray(), 0, bytes.Count);
            //}

            //serialPort.DataReceived

            byte[] buffer = new byte[serialPortPoll.BytesToRead];
            serialPortPoll.Read(buffer, 0, buffer.Length);
            // 进行CRC校验
            // buffer 最后两位CRC
            byte[] valid = new byte[2];
            valid[0] = buffer[buffer.Length - 2];
            valid[1] = buffer[buffer.Length - 1];
            // 自行处理

            // 去头去尾
            List<byte> byteList = new List<byte>(buffer);
            byteList.RemoveRange(0, 3);
            byteList.RemoveRange(byteList.Count - 2, 2);


            //   解析布尔型数值
            //byteList.Reverse();
            //var values = string.Join("", byteList.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')).ToList()).ToList();
            //values.Reverse();
            //for (int i = 0; i < Math.Min(values.Count, len); i++)
            //{
            //    System.Diagnostics.Debug.WriteLine(Convert.ToBoolean(int.Parse(values[i].ToString())));
            //}

            // 解析有符号单精度
            List<byte> valueBytes = new List<byte>();
            for (int i = 0; i < 10; i++)
            {
                valueBytes.Clear();
                valueBytes.Add(byteList[i * 2 + 1]);
                valueBytes.Add(byteList[i * 2]);

                var value = BitConverter.ToUInt16(valueBytes.ToArray(), 0);

                Debug.WriteLine(value);
            }
            // 解析float
            //  指定ABCD 模式   可配置 
            //List<byte> valueBytes = new List<byte>();
            //for (int i = 0; i < len/4; i++)
            //{
            //    valueBytes.Clear();
            //    valueBytes.Add(byteList[i * 4]);
            //    valueBytes.Add(byteList[i * 4 + 1]);
            //    valueBytes.Add(byteList[i * 4 + 2]);
            //    valueBytes.Add(byteList[i * 4 + 3]);

            //    var value = BitConverter.ToSingle(valueBytes.ToArray(), 0);
            //}
        }


        private void ButtonAscii_Click(object sender, RoutedEventArgs e)
        {
            this.OpenSerialPort();
            if (!this.serialPortPoll.IsOpen) return;

            // 准备报文
            List<byte> bytes = this.BaseCommands();
            // 读寄存器数量
            bytes.Add(0x00);
            bytes.Add(0x0A);

            // 开始有变化 基于ModbusAscii的处理方式
            // 添加Ascii的LRC校验
            bytes = ModbusValid.LRC(bytes);


            // 转换成Ascii码字符16进制
            var hex = bytes.Select(b => b.ToString("X2"));
            // 转换成一个完整的字符串
            string hexStr = string.Join("", hex.ToArray());
            // 转换成AsciiList
            List<byte> asciiList = new List<byte>(Encoding.ASCII.GetBytes(hexStr));

            // 添加报头和报尾
            asciiList.Insert(0, 0x3A);
            asciiList.Add(13);
            asciiList.Add(10);


            serialPortPoll.Write(asciiList.ToArray(), 0, asciiList.Count);

            byte[] buffer = new byte[serialPortPoll.BytesToRead];
            serialPortPoll.Read(buffer, 0, buffer.Length);

            // 去头去尾
            // 报文 有头有尾   判断冒号  
            List<byte> byteList = new List<byte>(buffer);
            byteList.RemoveAt(0);
            byteList.RemoveRange(byteList.Count - 2, 2);

            //int v = 48;
            //var vs= ((char)v).ToString();

            // Ascii转换成字节
            List<string> asciiStrList = new List<string>();
            // 
            byteList.ForEach(b => asciiStrList.Add(((char)b).ToString()));

            List<byte> resultBytes = new List<byte>();
            for (int i = 0; i < asciiStrList.Count; i++)
            {
                var stringHex = asciiStrList[i].ToString() + asciiStrList[++i].ToString();
                resultBytes.Add(Convert.ToByte(stringHex, 16));
            }
            // 如果做校验   把最后两个取出来进行比对
            // LRC 校验拿出来进行


            resultBytes.RemoveRange(0, 3);
            resultBytes.RemoveRange(resultBytes.Count - 1, 1);
            // 解析寄存器数值
            List<byte> valueBytes = new List<byte>();

            for (int i = 0; i < resultBytes.Count / 2; i++)
            {
                valueBytes.Clear();
                valueBytes.Add(resultBytes[i * 2 + 1]);
                valueBytes.Add(resultBytes[i * 2]);

                var value = BitConverter.ToUInt16(valueBytes.ToArray(), 0);
                System.Diagnostics.Debug.WriteLine(value);
            }

            // 基本的读保持型寄存器
        }

        private void ButtonTCP_Click(object sender, RoutedEventArgs e)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse("192.168.151.1"), 502));
            // 连接成功

            // 报文接收监听
            Task.Run(() =>
            {
                byte[] receiveBytes = new byte[10 * 2 + 9];
                int count = socket.Receive(receiveBytes);

                // 1、判断传输标识是不是一致
                // 2、分离PDU部分
                List<byte> byteList = new List<byte>(receiveBytes);
                byteList.RemoveRange(0, 9);

                // 解析数据
                List<byte> valueBytes = new List<byte>();

                for (int i = 0; i < byteList.Count / 2; i++)
                {
                    valueBytes.Clear();
                    valueBytes.Add(byteList[i * 2 + 1]);
                    valueBytes.Add(byteList[i * 2]);

                    var value = BitConverter.ToUInt16(valueBytes.ToArray(), 0);
                    System.Diagnostics.Debug.WriteLine(value);
                }
            });

            // 组装报文
            List<byte> byteList = BaseCommands();
            byteList.Add(0x00);
            byteList.Add((byte)150);

            // 没有校验[固定的]
            // 添加一些报文头
            List<byte> mbap = new List<byte>();
            mbap.Add(0x00);// 传输标识
            mbap.Add(0x00);
            mbap.Add(0x00);// 协议标识  Modbus
            mbap.Add(0x00);
            mbap.Add(BitConverter.GetBytes(byteList.Count)[1]);
            mbap.Add(BitConverter.GetBytes(byteList.Count)[0]);

            mbap.AddRange(byteList);

            // 发送报文
            socket.Send(mbap.ToArray());

        }

        private void ButtonSlave_Click(object sender, RoutedEventArgs e)
        {
            serialPortSlave = new SerialPort("COM2", 9600, Parity.None, 8, StopBits.One);
            // 被动接收数据
            serialPortSlave.ReceivedBytesThreshold = 1;
            serialPortSlave.DataReceived += SerialPort_DataReceived;
            serialPortSlave.Open();

            MessageBox.Show("从站已启动");
            //serialPort.Write();// 发送报文 
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            Debug.WriteLine(serialPortSlave.BytesToRead);
            // 进行接收的报文解析
            byte[] buffer = new byte[serialPortSlave.BytesToRead];
            serialPortSlave.Read(buffer, 0, buffer.Length);

            List<byte> byteList = new List<byte>(buffer);
            // 进行CRC的校验
            List<byte> checkCRC = byteList.GetRange(byteList.Count - 2, 2);
            byteList.RemoveRange(byteList.Count - 2, 2);
            List<byte> validCode = ModbusValid.CRC16(byteList);
            if (validCode.SequenceEqual(checkCRC))
            {
                //  进行解析
                // 1、判断从站ID是不是当前，如果不是当前，结束，不处理
                byte slaveId = byteList[0];// 从buffer中获取
                if (new int[] { 1, 2 }.Contains(slaveId))
                {
                    // 2、判断功能码
                    byte fc = byteList[1];
                    if (new int[] { 1, 2, 3, 4, 5, 6, 15, 16, 17, 23 }.Contains(fc))
                    {
                        // 3、判断数据地址
                        List<byte> startAddr = new List<byte> { byteList[2], byteList[3] };
                        startAddr.Reverse();
                        int start = BitConverter.ToUInt16(startAddr.ToArray());

                        List<byte> lenBytes = new List<byte> { byteList[4], byteList[5] };
                        lenBytes.Reverse();
                        int len = BitConverter.ToUInt16(lenBytes.ToArray());
                        // 如果起始地址或长度不在有效地址范围内，报02异常码


                        // 4、判断数据是否可接受
                        // 是否会将一般数据（模拟量）写入线圈
                        // 报03异常码

                        // 5、判断命令是否执行成功
                        // 进行最终执行
                        // 超时处理，
                        // 05：正在执行，但是需要花时间，执行超时
                        // 06：正在处理别的处理，暂时处理不了当前报文


                        // 根据解析的内容进行响应
                        // 
                        List<byte> respBytes = new List<byte>();
                        respBytes.Add(slaveId);
                        respBytes.Add(fc);
                        respBytes.Add((byte)(len * 2));
                        List<int> datas = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                        for (int i = start; i < start + len; i++)
                        {
                            respBytes.Add(BitConverter.GetBytes(datas[i])[1]);
                            respBytes.Add(BitConverter.GetBytes(datas[i])[0]);
                        }
                        respBytes.AddRange(ModbusValid.CRC16(respBytes));

                        serialPortSlave.Write(respBytes.ToArray(), 0, respBytes.Count);
                    }
                    else
                    {
                        // 将传过来功能码最高位置1  1000 0111   
                        // 跟异常码  01
                    }
                }
            }
        }
    }
}
