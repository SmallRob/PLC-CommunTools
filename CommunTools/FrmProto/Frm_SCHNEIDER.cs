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
            // TODO


        }
    }
}
