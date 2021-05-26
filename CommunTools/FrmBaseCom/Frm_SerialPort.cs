using Com_CSSkin;
using Com_CSSkin.SkinControl;
using Commun.NetWork.MQTT;
using CommunTools.Common;
using CommunTools.Entity;
using CommunTools.Enums;
using System;
using System.Collections.Generic;
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
    // 解决线程访问问题
    public delegate void SerialPortEventHandler(Object sender, SerialPortEventArgs e);    // 定义委托


    public partial class Frm_SerialPort : CSSkinMain
    {
        public Frm_SerialPort()
        {
            InitializeComponent();
            InitializeSerialSet();
        }

        private string FilePath = null;    // 打开文件路径

        private object thisLock = new object();    // 锁住线程

        public event SerialPortEventHandler comReceiveDataEvent = null;  // 定义串口接收数据响应事件
        // 数据状态
        private static int sendCount = 0;    // 发送数据量
        private static int receCount = 0;    // 接收数据量

        private void Frm_SerialPort_Load(object sender, EventArgs e)
        {
            comReceiveDataEvent += new SerialPortEventHandler(ComReceiveDataEvent);  // 订阅事件
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
            if (serialPort1 == null)
            {
                return;
            }

            if (serialPort1.IsOpen == false)
            {
                serialPort1.PortName = cmbComLst.Text;
                serialPort1.BaudRate = Convert.ToInt32(cmbBandRate.Text);
                serialPort1.Parity = (Parity)System.Enum.Parse(typeof(Parity), cmbPortParity.Text);
                serialPort1.DataBits = Convert.ToInt32(cmbDataBits.Text);
                serialPort1.StopBits = (StopBits)System.Enum.Parse(typeof(StopBits), cmbStopBits.Text);
                try
                {
                    serialPort1.Open();
                    SetBtnEnable(false);

                    // 打开属性变为关闭属性
                    btnOpenPort.BtnText = "关闭串口";
                    labComInfo.Text = "连接串口:" + cmbComLst.Text;
                }
                catch (Exception)
                {
                    labComInfo.Text = "连接失败:" + cmbComLst.Text;
                    FrmDialog.ShowDialog(this, "串口连接失败！\r\n可能原因：串口被占用");
                }
            }
            else
            {
                serialPort1.Close();
                SetBtnEnable(true);

                // 打开属性变为关闭属性
                btnOpenPort.BtnText = "打开串口";
                labComInfo.Text = "断开连接:" + cmbComLst.Text;
            }
        }

        private void SetBtnEnable(bool isEnable)
        {
            // 设置按键的使用权限
            cmbComLst.Enabled = isEnable;
            cmbBandRate.Enabled = isEnable;
            cmbPortParity.Enabled = isEnable;
            cmbDataBits.Enabled = isEnable;
            cmbStopBits.Enabled = isEnable;
            btnRefresh.Enabled = isEnable;

            btnSend.Enabled = !isEnable;
        }

        private void Frm_SerialServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1 != null && serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }

        /// <summary>
        /// 发送消息按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSend_BtnClick(object sender, EventArgs e)
        {
            string senddata = richTextBox_Send.Text;
            byte[] data = System.Text.Encoding.Default.GetBytes(senddata);   // 将发送的数据转化为字节数组            
            SendData(data);    // 发送数据
            sendCount += senddata.Length;
            lblSendStatus.Text = "已发送数据：" + sendCount.ToString();
        }

        private void btnRefresh_BtnClick(object sender, EventArgs e)
        {
            InitializeSerialSet(); // 刷新串口设置
        }

        #region 发送和接收
        /// <summary>
        /// 向串口中发送数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool SendData(byte[] data)
        {
            if (serialPort1 == null)
            {
                return false;
            }
            if (serialPort1.IsOpen == false)
            {
                return false;
            }

            try
            {
                serialPort1.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                FrmDialog.ShowDialog(this, "数据发送失败！");
                LogHelper.WriteException(ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 串口接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool ReceiveData()
        {
            if (serialPort1 == null)
            {
                return false;
            }
            if (serialPort1.IsOpen == false)
            {
                return false;
            }
            if (serialPort1.BytesToRead <= 0)   // 串口中没有数据
            {
                return false;
            }
            lock (thisLock)   // 锁住串口
            {
                int len = serialPort1.BytesToRead;
                byte[] data = new Byte[len];
                try
                {
                    serialPort1.Read(data, 0, len);   // 向串口中读取数据
                }
                catch (Exception ex)
                {
                    FrmDialog.ShowDialog(this, "数据接收失败！");
                    LogHelper.WriteException(ex);
                    return false;
                }

                SerialPortEventArgs args = new SerialPortEventArgs();
                args.receivedBytes = data;
                if (comReceiveDataEvent != null)
                {
                    comReceiveDataEvent.Invoke(this, args);
                }

            }
            return true;
        }

        /// <summary>
        /// 接收数据委托事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComReceiveDataEvent(object sender, SerialPortEventArgs e)
        {
            if (this.InvokeRequired)
            {
                try
                {
                    Invoke(new Action<Object, SerialPortEventArgs>(ComReceiveDataEvent), sender, e);
                }
                catch (Exception)
                {

                }
                return;
            }


            if (richTextBox_Receive.Text.Length > 0)
            {
                richTextBox_Receive.AppendText(" ");  // 中间使用 隔开，也可以使用-隔开
            }
            richTextBox_Receive.AppendText(System.Text.Encoding.Default.GetString(e.receivedBytes));

            // 更新状态显示框
            receCount += e.receivedBytes.Length;
            lblrecestatus.Text = "已接收数据：" + receCount.ToString();
        }
        #endregion

        private void btnClean_BtnClick(object sender, EventArgs e)
        {
            richTextBox_Send.Text = "";  // 清空显示框
            sendCount = 0;
            lblSendStatus.Text = "已发送数据：" + sendCount.ToString();

            richTextBox_Receive.Text = "";   // 清空接收数据
            receCount = 0;
            lblrecestatus.Text = "已接收数据：" + receCount.ToString();
        }

        /// <summary>
        /// 发送数据事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            ReceiveData();
        }

        /// <summary>
        /// 定时发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerSend_Tick(object sender, EventArgs e)
        {
            if (ckbTimeSend.Checked)
            {
                string datastr = richTextBox_Send.Text;
                if (datastr == "")
                {
                    return;
                }
                timerSend.Interval = int.Parse(txtSendTime.InputText);  // 将字符串转化为整型数字
                byte[] data = System.Text.Encoding.Default.GetBytes(datastr);   // 字符串转化为字节数组
                SendData(data);
                sendCount += datastr.Length;
                lblSendStatus.Text = "已发送数据：" + sendCount.ToString();

                btnSend.Enabled = false;
            }
            else
            {
                btnSend.Enabled = true;
            }
        }
    }
}
