using Com_CSSkin;
using Com_CSSkin.SkinControl;
using Commun.NetWork.MQTT;
using CommunTools.Common;
using CommunTools.Entity;
using CommunTools.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZCS_Common;
using ZCS_FormUI.Forms;
using sp = CommunTools.Enums.SerialPortEnum;

namespace CommunTools
{
    public partial class Frm_SerialServer : CSSkinMain
    {
        public Frm_SerialServer()
        {
            InitializeComponent();
            InitializeSerialSet();
        }

        /// <summary>
        /// 串口初始化加载
        /// </summary>
        private void InitializeSerialSet()
        {
            InitializePorts();   // 初始化串口号
            var ps = new List<(SkinComboBox, IList<EnumListModel>)>()
            {
                (cmbPortParity,EnumHelper.GetEnumList(typeof(sp.PortParity))),
                (cmbBandRate,EnumHelper.GetEnumList(typeof(sp.BandRate))),
                (cmbStopBits,EnumHelper.GetEnumList(typeof(sp.StopBits))),
                (cmbDataBits,EnumHelper.GetEnumList(typeof(sp.DataBit)))
            };

            foreach ((SkinComboBox, IList<EnumListModel>) item in ps)
            {
                foreach (EnumListModel enumLst in item.Item2)
                {
                    item.Item1.Items.Add(enumLst.EnumDescrip);
                }
                item.Item1.SelectedIndex = 0;
            }

            cmbBandRate.Text = cmbBandRate.Items[1].ToString();
        }

        /// <summary>
        /// 初始化串口号
        /// 扫描串口并显示
        /// </summary>
        private void InitializePorts()
        {
            cmbComLst.Items.Clear();   // 清空原来的信息
            // 返回可用串口号，形式：COM3
            string[] arraysPostsNames = SerialPort.GetPortNames();  // 获取所有可用的串口号

            // 检查串口号是否正确
            if (arraysPostsNames.Length > 0)
            {
                Array.Sort(arraysPostsNames);  // 使用默认进行排序，从小到大肾虚
                for (int i = 0; i < arraysPostsNames.Length; i++)
                {
                    cmbComLst.Items.Add(arraysPostsNames[i]);  // 将所有可用串口加载到串口显示框当中
                }
                cmbComLst.Text = arraysPostsNames[0];   // 默认选择第一个串口

                cmbComLst.Enabled = true;
            }
            else
            {
                labComInfo.Text = "没有可用串口";  // 设置状态栏的情况    
                cmbComLst.Enabled = false;
            }
        }

        /// <summary>
        /// TCPComServer对象
        /// </summary>
        private TCPComServer comServer;

        private void btnOpenServer_BtnClick(object sender, EventArgs e)
        {
            if (btnStart.BtnText == "开启服务")
            {
                string tipTxt = string.Empty;

                comServer = new TCPComServer();

                comServer.Context = SynchronizationContext.Current;
                comServer.ComPort = cmbComLst.Text;
                comServer.ip = txtTCPIP.InputText;
                comServer.port = txtPort.InputText;
                comServer.isTCPClient = false;

                //开启方法
                if (!InitSerialPort(comServer))
                {
                    tipTxt = "串口初始化失败!(" + cmbComLst.Text + ")";
                    FrmDialog.ShowDialog(this, tipTxt);
                    return;
                }
                else
                {
                    tipTxt = cmbComLst.Text + " 打开成功！";
                }
                if (!InitTCPListen(comServer))
                {
                    FrmDialog.ShowDialog(this, "监听服务端错误。(" + txtTCPIP.InputText + ")");
                    return;
                }
                InitComRecive(comServer);
                Thread.Sleep(100);
                btnStart.BtnText = "关闭";
                labComInfo.Text = "监听成功！" + tipTxt;
            }
            else
            {
                //关闭模式
                CloseDispose(comServer);
                Thread.Sleep(100);
                btnStart.BtnText = "开启服务";
                labLinkClient.Text = "";
                labComInfo.Text = "com打开信息";
            }
        }

        /// <summary>
        /// 初始化串口方法
        /// </summary>
        private bool InitSerialPort(TCPComServer comServer)
        {
            Parity par = Parity.None;
            if (cmbPortParity.SelectedItem != null)
            {
                sp.PortParity portParity = (sp.PortParity)cmbPortParity.SelectedIndex;

                switch (portParity)
                {
                    case sp.PortParity.None:
                        par = Parity.None;
                        break;
                    case sp.PortParity.Odd:
                        par = Parity.Odd;
                        break;
                    case sp.PortParity.Even:
                        par = Parity.Even;
                        break;
                    case sp.PortParity.Mark:
                        par = Parity.Mark;
                        break;
                    default:
                        par = Parity.None;
                        break;
                }
            }

            //停止位
            StopBits stopBit = StopBits.None;

            sp.StopBits stopBits = (sp.StopBits)cmbStopBits.SelectedIndex;
            switch (stopBits)
            {
                case sp.StopBits.One:
                    stopBit = StopBits.One;
                    break;
                case sp.StopBits.OnePointFive:
                    stopBit = StopBits.OnePointFive;
                    break;
                case sp.StopBits.Two:
                    stopBit = StopBits.Two;
                    break;
                default:
                    stopBit = StopBits.None;
                    break;
            }
            try
            {
                comServer.EntitySerialPort = null;
                comServer.EntitySerialPort = new SerialPort();

                comServer.EntitySerialPort.PortName = comServer.ComPort;  //串口名称
                comServer.EntitySerialPort.BaudRate = int.Parse(cmbBandRate.Text);    //波特率
                comServer.EntitySerialPort.DataBits = int.Parse(cmbDataBits.Text);    //数据位
                comServer.EntitySerialPort.Parity = par;       //校验位
                comServer.EntitySerialPort.StopBits = stopBit; //停止位
                comServer.EntitySerialPort.ReadTimeout = 3000;   //读写超时控制在3秒内
                comServer.EntitySerialPort.WriteTimeout = 3000;

                //设置数据流控制；数据传输的握手协议
                comServer.EntitySerialPort.Handshake = Handshake.None;
                comServer.EntitySerialPort.ReceivedBytesThreshold = 1;
                comServer.EntitySerialPort.RtsEnable = true;

                if (comServer.EntityTCPListen != null)
                {
                    comServer.EntityTCPListen.Stop();
                }

                while (comServer.EntitySerialPort.IsOpen)
                {
                    comServer.EntitySerialPort.Close();
                }

                if (!comServer.EntitySerialPort.IsOpen)
                {
                    comServer.EntitySerialPort.Open();
                }
            }
            catch (Exception ex)
            {
                CloseDispose(comServer);
                return false;
            }

            comServer.IsLoopFlag = true;

            comServer.ThreadCOMSend = new Thread(new ParameterizedThreadStart(ComSendData));
            comServer.ThreadCOMSend.IsBackground = true;
            comServer.ThreadCOMSend.Start(comServer);
            return true;
        }

        /// <summary>
        /// 串口发送数据
        /// </summary>
        private void ComSendData(object comServer)
        {
            TCPComServer server = (TCPComServer)comServer;

            while (server.IsLoopFlag)
            {
                byte[] sendBytes = null;
                lock (server.TCPRecvQueue)
                {
                    if (server.TCPRecvQueue.Count > 0)
                    {
                        sendBytes = server.TCPRecvQueue.Dequeue();
                    }
                }
                if (sendBytes != null)
                {
                    try
                    {
                        server.EntitySerialPort.Write(sendBytes, 0, sendBytes.Length);

                        ShowRealInfoToFrm(CommunTCPEnum.COMEntitySendOK, sendBytes, server);
                    }
                    catch (Exception)
                    {
                        ShowRealInfoToFrm(CommunTCPEnum.COMEntitySendError, sendBytes, server);
                    }

                }
            }
        }

        /// <summary>
        /// TCP服务端监听初始化
        /// </summary>
        private bool InitTCPListen(TCPComServer comServer)
        {
            try
            {
                comServer.EntityTCPListen = new TcpListener(IPAddress.Parse(comServer.ip), int.Parse(comServer.port));
                comServer.EntityTCPListen.Start(1);
            }
            catch (Exception ex)
            {
                CloseDispose(comServer);
                LogHelper.WriteException(ex, "InitTCPListen");
                return false;
            }
            comServer.EntityTCPListen.Stop();
            comServer.IsLoopFlag = true;
            comServer.ThreadTcpSever = new Thread(new ParameterizedThreadStart(ListenClient));
            comServer.ThreadTcpSever.IsBackground = true;
            comServer.ThreadTcpSever.Start(comServer);
            return true;
        }

        /// <summary>
        /// TCP服务端监听线程
        /// </summary>
        private void ListenClient(object comServer)
        {
            TCPComServer server = (TCPComServer)comServer;

            while (server.IsLoopFlag)
            {
                try
                {
                    server.EntityTCPListen.Start(1);
                    server.TCPClient = server.EntityTCPListen.AcceptTcpClient();
                }
                catch
                { }
                if (server.TCPClient == null)
                {
                    continue;
                }
                byte[] tempRec = new byte[1024 * 2];
                server.Context.Post(ret =>
                {
                    labLinkClient.Text = "- 客户端[" + server.TCPClient.Client.RemoteEndPoint.ToString() + "]已连接";
                    //labLinkClient.ForeColor = Color.Green;
                }, null);
                server.EntityTCPListen.Stop();
                while (server.IsLoopFlag)
                {
                    int recNU = server.TCPClient.Client.Receive(tempRec);
                    if (recNU == 0)
                    {
                        server.Context.Post(ret =>
                        {
                            labLinkClient.Text = "- 客户端断开连接！";
                        }, null);
                        server.TCPClient.Close();
                        server.TCPClient = null;
                        break;
                    }
                    byte[] queueBytes = new byte[recNU];
                    Array.Copy(tempRec, queueBytes, queueBytes.Length);
                    server.RecivePacketNumber++;
                    server.ReciveBytesNumber += recNU;
                    server.Context.Post(ret =>
                    {
                        //labReciveInfo.Text = string.Format("{0}/{1}", RecivePacketNumber, ReciveBytesNumber);
                    }, null);
                    //EntitySerialPort.Write(queueBytes, 0, queueBytes.Length);  //读到数据直接发
                    ShowRealInfoToFrm(CommunTCPEnum.TCPEntityReciveOK, queueBytes, null);
                    lock (server.TCPRecvQueue)
                    {
                        server.TCPRecvQueue.Enqueue(queueBytes);
                    }
                }
            }
        }

        /// <summary>
        /// 串口接收数据初始化
        /// </summary>
        /// <param name="comServer"></param>
        private void InitComRecive(TCPComServer comServer)
        {
            // 订阅事件
            //comReceiveDataEvent = new SerialPortEventHandler(ComReceiveDataEvent);

            comServer.ThreadComRecive = new Thread(new ParameterizedThreadStart(ProcComRecive));
            comServer.ThreadComRecive.IsBackground = true;
            comServer.ThreadComRecive.Start(comServer);
        }

        /// <summary>
        /// 串口接收数据 开始转发
        /// </summary>
        private void ProcComRecive(Object comServer)
        {
            TCPComServer server = (TCPComServer)comServer;

            byte[] comButs = new byte[1024 * 10];
            byte[] heardBytes = { 0xa1, 0xa2, 0xa3, 0xa4 };
            byte[] footBytes = { 0xe1, 0xe2, 0xe3, 0xe4 };
            while (server.IsLoopFlag)
            {
                if (server.EntitySerialPort != null && server.EntitySerialPort.IsOpen)
                {
                    Thread.Sleep(1);
                    int reciveNU = server.EntitySerialPort.BytesToRead;
                    if (reciveNU > 0)
                    {
                        int nu = server.EntitySerialPort.Read(comButs, 0, reciveNU);

                        ShowRealInfoToFrm(CommunTCPEnum.COMEntityReciveOK, comButs, nu, server);
                        byte[] SendBytes = new byte[nu + 8];
                        Array.Copy(heardBytes, 0, SendBytes, 0, heardBytes.Length);
                        Array.Copy(comButs, 0, SendBytes, heardBytes.Length, nu);
                        Array.Copy(footBytes, 0, SendBytes, SendBytes.Length - footBytes.Length, footBytes.Length);
                        byte[] originalData = new byte[nu];
                        Array.Copy(comButs, 0, originalData, 0, nu);

                        //
                        //SerialPortEventArgs args = new SerialPortEventArgs();
                        //args.receivedBytes = originalData;
                        //if (comReceiveDataEvent != null)
                        //{
                        //    comReceiveDataEvent.Invoke(this, args);
                        //}

                        if (server.TCPClient != null)
                        {
                            server.COMRecivePacketNumber++;
                            server.COMReciveBytesNumber += reciveNU;
                            server.TCPClient.Client.Send(originalData);
                            server.Context.Post(ret =>
                            {
                                //已发送的数据量
                                lblCount.Text = server.COMRecivePacketNumber + "/" + server.COMReciveBytesNumber;
                            }, null);
                        }
                        else
                        {
                            server.Context.Post(ret =>
                            {
                                labComInfo.Text = "无客端连接，转发收到串口数据失败！";
                            }, null);
                        }
                    }

                }
            }
        }

        private void ShowRealInfoToFrm(CommunTCPEnum commEnum, byte[] buff, int buffLen, TCPComServer comServer)
        {
            if (buffLen == buff.Length)
            {
                ShowRealInfoToFrm(commEnum, buff, comServer);
            }
            else if (buffLen > buff.Length)
            {
                return;
            }
            else if (buffLen < buff.Length)
            {
                byte[] revRealBuff = new byte[buffLen];
                Array.Copy(buff, 0, revRealBuff, 0, buffLen);

                ShowRealInfoToFrm(commEnum, revRealBuff, comServer);
            }
        }

        /// <summary>
        /// 窗体展示接收到的数据
        /// </summary>
        /// <param name="cOMEntitySendOK"></param>
        /// <param name="sendBytes"></param>
        private void ShowRealInfoToFrm(CommunTCPEnum commEnum, byte[] buff, TCPComServer server)
        {
            if (server == null)
            {
                return;
            }

            if (!server.IsRealShowInfo)
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("[{0}]# ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            switch (commEnum)
            {
                case CommunTCPEnum.COMEntitySendOK:
                    sb.AppendFormat("TXD[OK]  {0} >\r\n", server.ComPort);
                    break;
                case CommunTCPEnum.COMEntitySendError:
                    sb.AppendFormat("TXD[Error] {0} >\r\n", server.ComPort);
                    break;
                case CommunTCPEnum.COMEntityReciveOK:
                    sb.AppendFormat("RXD[OK] {0} >\r\n", server.ComPort);
                    break;
                case CommunTCPEnum.COMEntityReciveError:
                    sb.AppendFormat("RXD[Error] {0} >\r\n", server.ComPort);
                    break;
                case CommunTCPEnum.TCPEntitySendOK:
                    sb.AppendFormat("Send[OK] {0} >\r\n", server.TCPClient.Client.RemoteEndPoint.ToString());
                    break;
                case CommunTCPEnum.TCPEntitySendError:
                    sb.AppendFormat("Send[Err] {0} >\r\n", server.TCPClient.Client.RemoteEndPoint.ToString());
                    break;
                case CommunTCPEnum.TCPEntityReciveOK:
                    sb.AppendFormat("Recive[OK] {0} >\r\n", server.TCPClient.Client.RemoteEndPoint.ToString());
                    break;
                case CommunTCPEnum.TCPEntityReciveError:
                    sb.AppendFormat("Recive {0}[Err] >\r\n", server.TCPClient.Client.RemoteEndPoint.ToString());
                    break;
                default:
                    break;
            }

            lock (buff)
            {
                string strHex = (HEXConverter.GetBufferFormatHex(buff) + "\r\n");
                server.Context.Post(ret =>
                {
                    txtShowMsg.SelectionColor = Color.Gray;
                    txtShowMsg.AppendText(sb.ToString());
                    txtShowMsg.SelectionColor = Color.Blue;
                    txtShowMsg.AppendText(strHex);
                    txtShowMsg.ScrollToCaret();

                    if (commEnum == CommunTCPEnum.COMEntityReciveOK || commEnum == CommunTCPEnum.COMEntitySendOK)
                    {
                        //txtReceive.AppendText("\r\n");
                    }

                    Thread.Sleep(1);
                    //string buffStr = System.Text.Encoding.Default.GetString(buff).Trim();
                    //txtReceive.AppendText("[" + server.serverIndex + "]:" + buffStr);

                    //发送给TCP客户端
                    //Thread tdSendToC = new Thread(new ParameterizedThreadStart(TcpClientSendMsg));
                    //tdSendToC.Start(txtReceive.Text);

                    //txtReceive.ScrollToCaret();
                }, null);
            }
        }

        /// <summary>
        /// 关闭释放资源
        /// </summary>
        public void CloseDispose(TCPComServer tcpCom)
        {
            if (tcpCom == null)
            {
                return;
            }

            tcpCom.IsLoopFlag = false;
            tcpCom.RecivePacketNumber = 0;
            tcpCom.ReciveBytesNumber = 0;
            tcpCom.COMReciveBytesNumber = 0;
            tcpCom.COMRecivePacketNumber = 0;

            if (tcpCom.EntitySerialPort != null)
            {
                tcpCom.EntitySerialPort.RtsEnable = false;
                tcpCom.EntitySerialPort.Close();
                tcpCom.EntitySerialPort = null;
            }

            if (tcpCom.EntityTCPListen != null)
            {
                tcpCom.EntityTCPListen.Stop();
                tcpCom.EntityTCPListen = null;
            }
            if (tcpCom.TCPClient != null)
            {
                tcpCom.TCPClient.Close();
            }

            tcpCom.TCPClient = null;
            tcpCom.EntityTCPListen = null;
            if (tcpCom.ThreadTcpSever != null && tcpCom.ThreadTcpSever.IsAlive)
            {
                tcpCom.ThreadTcpSever.Abort();
            }
            if (tcpCom.ThreadCOMSend != null && tcpCom.ThreadCOMSend.IsAlive)
            {
                tcpCom.ThreadCOMSend.Abort();
            }
            if (tcpCom.ThreadComRecive != null && tcpCom.ThreadComRecive.IsAlive)
            {
                tcpCom.ThreadComRecive.Abort();
            }
            Thread.Sleep(100);
            GC.Collect();
        }

        private void Frm_SerialServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                CloseDispose(comServer);
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void Frm_SerialServer_Load(object sender, EventArgs e)
        {
            JsonFileConfig jsonFile = new JsonFileConfig();
            jsonFile.ConfigSection = "TcpServer";

            var jc = jsonFile.LoadJsonConfig(jsonFile.ConfigFile, "TcpServer");

            txtTCPIP.InputText = jc[0];
            txtPort.InputText = jc[1];
        }
    }
}
