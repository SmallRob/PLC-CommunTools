using Com_CSSkin;
using Commun.NetWork.ModbusTCP;
using Commun.NetWork.MQTT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CommunTools
{
    public partial class Frm_SCHNEIDER : CSSkinMain
    {
        public Frm_SCHNEIDER()
        {
            InitializeComponent();
        }

        private void btnConnect_BtnClick(object sender, EventArgs e)
        {
            ModbusTcpHelper modbusTcp = new ModbusTcpHelper();
            //测试施耐德PLC通讯
            modbusTcp.ConnectPLC("192.168.1.1", 502);

            // 接收及发送数据
            byte[] sendData = new byte[12];
            byte[] receiveData = new byte[12];
            sendData[0] = 0x01;
            sendData[1] = 0x03;
            sendData[2] = 0x00;
            sendData[3] = 0x00;
            sendData[4] = 0x00;
            sendData[5] = 0x01;
            sendData[6] = 0x02;
            sendData[7] = 0xC4;
            sendData[8] = 0x0B;
            receiveData = modbusTcp.SendData(sendData);
            modbusTcp.Close();


        }
    }
}
