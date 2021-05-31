using Com_CSSkin;
using Com_CSSkin.SkinControl;
using Commun.NetWork.MQTT;
using CommunTools.Common;
using CommunTools.Entity;
using CommunTools.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ZCS_Common;
using ZCS_FormUI.Forms;
using sp = CommunTools.Enums.SerialPortEnum;

namespace CommunTools
{
    public partial class Frm_TCPClient : CSSkinMain
    {
        public Frm_TCPClient()
        {
            InitializeComponent();
        }

        public byte[] recBy;        //传递串口接收到的数据

        public int recByLenth = 0;  //串口接收到的数据长度

        public bool receiveDtatFlag = false;

        //防止窗口关闭时线程没有关闭占用资源
        public bool ClientCloseForm = false;

        public bool ClientThreadStop = false;
        public int send_count = 0, recv_count = 0;
        public int recv_DataCnt = 0, send_DataCnt = 0;

        TCPClient client = new TCPClient();

        public delegate void ClientInvoke(string str, string str2);

        private void Frm_SerialServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void Frm_SerialServer_Load(object sender, EventArgs e)
        {
            JsonFileConfig jsonFile = new JsonFileConfig();
            jsonFile.ConfigSection = "TCPServer";

            var jc = jsonFile.LoadJsonConfig(jsonFile.ConfigFile, "TCPServer");

            txtTCPIP.InputText = jc[0];
            txtPort.InputText = jc[1];
        }

        private void btnStart_BtnClick(object sender, EventArgs e)
        {
            ConnectTCP();
        }

        private void ConnectTCP()
        {
            if (client.Connected == false)//没有连接
            {
                client.Newclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Ip = IPAddress.Parse(txtTCPIP.InputText.Trim());
                client.Port = Convert.ToInt32(txtPort.InputText.Trim());
                client.EndPoint = new IPEndPoint(client.Ip, client.Port);

                try
                {
                    client.Newclient.Connect(client.EndPoint);

                    client.Connected = true;
                    btnStart.BtnText = "断开";
                    ClientThreadStop = true;

                    txtTCPIP.Enabled = false;
                    txtPort.Enabled = false;
                }
                catch (SocketException e)
                {
                    client.Connected = false;
                    btnStart.BtnText = "连接";
                    ClientThreadStop = false;

                    txtTCPIP.Enabled = true;
                    txtPort.Enabled = true;

                    FrmDialog.ShowDialog(this, "连接服务器失败！\n" + e.Message);
                    return;
                }

                //多线程处理

                //ThreadStart myThreaddelegate = new ThreadStart(ReceiveMsg);
                //myThread = new Thread(myThreaddelegate);

                Thread myThread = new Thread(ReceiveMsg);//创建新线程
                myThread.IsBackground = true;//线程后台运行
                myThread.Start();//启动线程
            }
            else
            {
                client.Connected = false;
                btnStart.BtnText = "连接";
                if (client.ClientThread != null)
                {
                    client.ClientThread.Abort();
                    //Application.ExitThread();
                }
                ClientThreadStop = false;
                client.Newclient.Disconnect(false);
                txtTCPIP.Enabled = true;
                txtPort.Enabled = true;
            }
        }

        public void ReceiveMsg()//接收处理线程部分
        {
            while (ClientThreadStop)
            {
                string myRecvStrTemp = "";
                string bulidStr = "";

                #region 接收处理部分

                if (ClientCloseForm == false)
                {
                    try
                    {
                        //Thread.Sleep(Convert.ToInt32(textBox5.Text));//延时100ms 等待接收完数据
                        byte[] data = new byte[1024];

                        //先记录下来，避免某种原因，人为的原因，操作几次之间时间长，缓存不一致
                        int n = client.Newclient.Receive(data);
                        if (n > 0)//有数据时才处理
                        {
                            StringBuilder builder = new StringBuilder();//避免在事件处理方法中反复的创建，定义到外面。
                            long received_count = 0;//接收计数
                            byte[] buf = new byte[n];//声明一个临时数组存储当前来的串口数据
                            received_count += n;//增加接收计数
                            Array.Copy(data, 0, buf, 0, n);
                            //serialPort1.Read(buf, 0, n);//读取缓冲数据
                            builder.Clear();//清除字符串构造器的内容

                            recBy = buf;
                            recByLenth = buf.Length;

                            //因为要访问ui资源，所以需要使用invoke方式同步ui。
                            //this.Invoke((EventHandler)(delegate

                            myRecvStrTemp += String.Format("[{0}]# ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")) + "\n";

                            //判断是否是显示为16进制
                            if (ckbRevHEX.Checked)
                            {
                                //依次的拼接出16进制字符串
                                foreach (byte b in buf)
                                {
                                    builder.Append(b.ToString("X2") + " ");//在此实例的结尾追加指定字符串的副本
                                }
                            }
                            else
                            {
                                //直接按ASCII规则转换成字符串
                                builder.Append(Encoding.ASCII.GetString(buf));
                            }

                            bulidStr += builder.ToString() + "\n";

                            recv_count += n;//增加接收计数
                        }
                    }
                    catch (System.Exception ex)
                    {
                        client.Connected = false;
                        btnStart.Text = "连接";
                        ClientThreadStop = false;

                        txtTCPIP.Enabled = true;
                        txtPort.Enabled = true;

                        FrmDialog.ShowDialog(this, "与服务器断开！\n" + ex.Message);
                    }
                }
                #endregion

                showMsg(myRecvStrTemp, bulidStr);
            }
        }

        public void showMsg(string msg, string buildStr)//接收显示处理部分
        {
            {
                //在线程里以安全方式调用控件
                if ((txtShowMsg.InvokeRequired) || (lblSendStatus.InvokeRequired))
                {
                    ClientInvoke _myinvoke = new ClientInvoke(showMsg);
                    txtShowMsg.Invoke(_myinvoke, new object[] { msg, buildStr });
                }
                else
                {
                    txtShowMsg.SelectionColor = Color.Gray;
                    txtShowMsg.AppendText(msg);
                    txtShowMsg.SelectionColor = Color.Blue;
                    txtShowMsg.AppendText(buildStr);
                    txtShowMsg.ScrollToCaret();

                    recv_count += 1;
                    recv_DataCnt += Encoding.Default.GetByteCount(msg);
                    lblSendStatus.Text = "已接收数据：" + recv_count.ToString() + "/" + recv_DataCnt.ToString();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = Convert.ToInt32(txtSendTime.InputText);
            try
            {
                if ((client.Connected) && (ckbTimeSend.Checked))
                {
                    SendMsg();
                }
                else
                {
                    //MessageBox.Show("串口未打开！", "错误提示");
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("发送数据时发生错误！", "错误提示");
            }
        }

        private void btnSend_BtnClick(object sender, EventArgs e)
        {
            try
            {
                if (client.Connected)
                {
                    if (ckbTimeSend.Checked == true)
                    {
                        timer1.Enabled = true;
                    }
                    else
                    {
                        timer1.Enabled = false;
                    }

                    SendMsg();
                }
                else
                {
                    FrmDialog.ShowDialog(this, "网络端口未打开！", "错误提示");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteException(ex);
                FrmDialog.ShowDialog(this, "发送数据时发生错误！", "错误提示");
            }
        }

        private void SendMsg()
        {
            //十六进制发送
            if (ckbHEX.Checked)
            {
                ArrayList al = StringUtilites.Str16ToArrayList(richTextBox_Send.Text);
                byte[] by = new byte[al.Count];

                send_DataCnt += by.Length;

                int i = 0;
                foreach (string stmp in al)
                {
                    //将指定基的数字的字符串表示形式转换为等效的 8 位无符号整数。
                    by[i] += Convert.ToByte(stmp, 16);
                    i++;
                }
                int mySendLenth = client.Newclient.Send(by);

                //serialPort1.Write(by, 0, i);//发送字节
                //s = Encoding.GetEncoding("Gb2312").GetString(by);
                //在派生类中重写时，将指定字节数组中的所有字节解码为一个字符串。。
            }
            else//ASCII发送
            {
                int m_length = richTextBox_Send.Text.Trim().Length;
                byte[] data = new byte[m_length];
                data = Encoding.UTF8.GetBytes(richTextBox_Send.Text);
                int mySendLenth = client.Newclient.Send(data);

                send_DataCnt += data.Length;
                //Marshal.SizeOf((ByteDataCommon.byLength(data)).GetType());
            }
            send_count++;

            lblSendStatus.Text = "已发送数据：" + send_count + "/" + send_DataCnt.ToString();
        }

        private void btnClean_BtnClick(object sender, EventArgs e)
        {
            send_count = recv_count = 0;
            recv_DataCnt = send_DataCnt = 0;

            lblrecestatus.Text = "已接收数据：0";
            lblSendStatus.Text = "已发送数据：0";
        }
    }
}
