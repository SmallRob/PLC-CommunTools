using Com_CSSkin;
using Commun.NetWork;
using System;
using System.Text;
using System.Windows.Forms;
using ZCS_Common;

namespace CommunTools
{
    public partial class Frm_ComWebSocket : CSSkinMain
    {
        public Frm_ComWebSocket()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        WSocketClient wsClient;

        private void Frm_SerialServer_Load(object sender, EventArgs e)
        {
            JsonFileConfig jsonFile = new JsonFileConfig();
            jsonFile.ConfigSection = "TCPServer";

            var jc = jsonFile.LoadJsonConfig(jsonFile.ConfigFile, "TCPServer");

            txtTCPIP.InputText = jc[0];
        }

        private void btnStart_BtnClick(object sender, EventArgs e)
        {
            OpenWSServer();
        }

        private void OpenWSServer()
        {
            if (!"断开链接".Equals(btnStart.BtnText))
            {
                //新建客户端类 
                //服务端IP地址 ws://192.168.1.13 如果服务端开启了ssl或者tsl 这里前缀应该改成 wss:/
                //服务端监听端口 1234
                //自定义的地址参数 可以根据地址参数来区分客户端 /lcj控制台
                //开始链接

                try
                {
                    wsClient = new WSocketClient((ckbTLS.Checked ? "wss://" : "ws://") + txtTCPIP.InputText);
                    wsClient.Start();
                    ShowMsg("链接成功！");
                    MessageReceived();
                }
                catch (Exception ex)
                {
                    ShowMsg($"发生异常链接失败{ex.ToString()}");
                    throw;
                }

                btnStart.BtnText = "断开链接";
            }
            else
            {
                // 记得释放资源否则会造成堆栈
                wsClient.Dispose();
                ShowMsg("已成功释放资源!");
                MessageReceived();

                btnStart.BtnText = "链接服务";
            }
        }

        /// <summary>
        /// 服务端返回的消息
        /// </summary>
        private void MessageReceived()
        {
            //注册消息接收事件，接收服务端发送的数据
            wsClient.MessageReceived += (data) =>
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("[{0}]# \n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                sb.AppendFormat(data + "\n");

                txtShowMsg.Text += sb.ToString();
            };
        }

        void ShowMsg(string str)
        {
            txtShowMsg.AppendText(str + "\r\n");
        }

        private void btnSend_BtnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(richTextBox_Send.Text.Trim()))
            {
                string inputMsg = richTextBox_Send.Text.ToString();

                wsClient.SendMessage(inputMsg);
                MessageReceived();
            }
        }

        private void Frm_ComWebSocket_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (wsClient != null)
                wsClient.Dispose();
        }
    }
}
