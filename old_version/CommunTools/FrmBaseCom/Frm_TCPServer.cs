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
using System.IO;
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
    public partial class Frm_TCPServer : CSSkinMain
    {
        public Frm_TCPServer()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;
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
            OpenTCPServer();
        }

        Thread threadWatch = null; // 负责监听客户端连接请求的 线程；
        Socket socketWatch = null;

        Dictionary<string, Socket> dict = new Dictionary<string, Socket>();
        Dictionary<string, Thread> dictThread = new Dictionary<string, Thread>();

        private void OpenTCPServer()
        {
            if (!"停止服务".Equals(btnStart.BtnText))
            {
                // 创建负责监听的套接字，注意其中的参数；
                socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // 获得文本框中的IP对象；
                IPAddress address = IPAddress.Parse(txtTCPIP.InputText.Trim());
                // 创建包含ip和端口号的网络节点对象；
                IPEndPoint endPoint = new IPEndPoint(address, int.Parse(txtPort.InputText.Trim()));
                try
                {
                    // 将负责监听的套接字绑定到唯一的ip和端口上；
                    socketWatch.Bind(endPoint);
                }
                catch (SocketException se)
                {
                    FrmDialog.ShowDialog(this, "程序异常：\n" + se.Message, MessageCommon.msgTitle);
                    return;
                }
                // 设置监听队列的长度；
                socketWatch.Listen(10);
                // 创建负责监听的线程；
                threadWatch = new Thread(WatchConnecting);
                threadWatch.IsBackground = true;
                threadWatch.Start();
                ShowMsg("服务器启动监听成功！");

                btnStart.BtnText = "停止服务";

            }
            else
            {
                //停止服务

            }
        }

        /// <summary>
        /// 监听客户端请求的方法；
        /// </summary>
        void WatchConnecting()
        {
            while (true)  // 持续不断的监听客户端的连接请求；
            {
                // 开始监听客户端连接请求，Accept方法会阻断当前的线程；
                Socket sokConnection = socketWatch.Accept(); // 一旦监听到一个客户端的请求，就返回一个与该客户端通信的 套接字；
                // 想列表控件中添加客户端的IP信息；
                lbOnline.Items.Add(sokConnection.RemoteEndPoint.ToString());
                // 将与客户端连接的 套接字 对象添加到集合中；
                dict.Add(sokConnection.RemoteEndPoint.ToString(), sokConnection);
                ShowMsg("客户端连接成功！");
                Thread thr = new Thread(RecMsg);
                thr.IsBackground = true;
                thr.Start(sokConnection);
                dictThread.Add(sokConnection.RemoteEndPoint.ToString(), thr);  //  将新建的线程 添加 到线程的集合中去。
            }
        }

        void RecMsg(object sokConnectionparn)
        {
            Socket sokClient = sokConnectionparn as Socket;
            while (true)
            {
                // 定义一个2M的缓存区；
                byte[] arrMsgRec = new byte[1024 * 1024 * 2];
                // 将接受到的数据存入到输入  arrMsgRec中；
                int length = -1;
                try
                {
                    length = sokClient.Receive(arrMsgRec); // 接收数据，并返回数据的长度；
                }
                catch (SocketException se)
                {
                    ShowMsg("异常：" + se.Message);
                    // 从 通信套接字 集合中删除被中断连接的通信套接字；
                    dict.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从通信线程集合中删除被中断连接的通信线程对象；
                    dictThread.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从列表中移除被中断的连接IP
                    lbOnline.Items.Remove(sokClient.RemoteEndPoint.ToString());
                    break;
                }
                catch (Exception e)
                {
                    ShowMsg("异常：" + e.Message);
                    // 从 通信套接字 集合中删除被中断连接的通信套接字；
                    dict.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从通信线程集合中删除被中断连接的通信线程对象；
                    dictThread.Remove(sokClient.RemoteEndPoint.ToString());
                    // 从列表中移除被中断的连接IP
                    lbOnline.Items.Remove(sokClient.RemoteEndPoint.ToString());
                    break;
                }

                if (length > 0)
                {
                    if (arrMsgRec[0] == 0)  // 表示接收到的是数据；
                    {
                        string strMsg = System.Text.Encoding.UTF8.GetString(arrMsgRec, 1, length - 1);// 将接受到的字节数据转化成字符串；
                        ShowMsg(strMsg);
                    }
                    if (arrMsgRec[0] == 1) // 表示接收到的是文件；
                    {
                        SaveFileDialog sfd = new SaveFileDialog();

                        if (sfd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {// 在上边的 sfd.ShowDialog（） 的括号里边一定要加上 this 否则就不会弹出 另存为 的对话框，而弹出的是本类的其他窗口，，这个一定要注意！！！【解释：加了this的sfd.ShowDialog(this)，“另存为”窗口的指针才能被SaveFileDialog的对象调用，若不加thisSaveFileDialog 的对象调用的是本类的其他窗口了，当然不弹出“另存为”窗口。】

                            string fileSavePath = sfd.FileName;// 获得文件保存的路径；
                                                               // 创建文件流，然后根据路径创建文件；
                            using (FileStream fs = new FileStream(fileSavePath, FileMode.Create))
                            {
                                fs.Write(arrMsgRec, 1, length - 1);
                                ShowMsg("文件保存成功：" + fileSavePath);
                            }
                        }
                    }
                }
            }
        }


        void ShowMsg(string str)
        {
            txtShowMsg.AppendText(str + "\r\n");
        }

        private void btnSend_BtnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(richTextBox_Send.Text.Trim()))
            {
                string strMsg = "服务器 --> " + richTextBox_Send.Text.Trim() + "\r\n";
                byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(strMsg); // 将要发送的字符串转换成Utf-8字节数组；
                byte[] arrSendMsg = new byte[arrMsg.Length + 1];
                arrSendMsg[0] = 0; // 表示发送的是消息数据
                Buffer.BlockCopy(arrMsg, 0, arrSendMsg, 1, arrMsg.Length);
                string strKey = "";
                strKey = lbOnline.Text.Trim();
                if (string.IsNullOrEmpty(strKey))   // 判断是不是选择了发送的对象；
                {
                    FrmDialog.ShowDialog(this, "请先选择您要发送的对象!", MessageCommon.msgTitle);
                }
                else
                {
                    dict[strKey].Send(arrSendMsg);// 解决了 sokConnection是局部变量，不能再本函数中引用的问题；
                    ShowMsg(strMsg);
                    //richTextBox_Send.Clear();
                }
            }
        }

        /// <summary>
        /// 群发消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendToAll_BtnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(richTextBox_Send.Text.Trim()))
            {
                string strMsg = "服务器 --> " + richTextBox_Send.Text.Trim() + "\r\n";
                byte[] arrMsg = System.Text.Encoding.UTF8.GetBytes(strMsg); // 将要发送的字符串转换成Utf-8字节数组；
                foreach (Socket s in dict.Values)
                {
                    s.Send(arrMsg);
                }
                ShowMsg(strMsg);
                ShowMsg("服务器：群发消息完毕～～～");
            }
        }
    }
}
