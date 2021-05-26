using Com_CSSkin;
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
    public partial class Frm_MQTT : CSSkinMain
    {
        public Frm_MQTT()
        {
            InitializeComponent();
        }

        private void btnConnect_BtnClick(object sender, EventArgs e)
        {
            MQTTBase mqBase = new MQTTBase("192.168.0.36", 3678);
            mqBase.Connect();
        }
    }
}
