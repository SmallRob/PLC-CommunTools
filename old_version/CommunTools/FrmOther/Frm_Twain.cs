using Com_CSSkin;
using Commun.NetWork.MQTT;
using CommunTools.Common;
using CommunTools.Common.Twain;
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
    public partial class Frm_Twain : CSSkinMain
    {
        public Frm_Twain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 开始扫描
        /// </summary>
        private void btnStartScan_BtnClick(object sender, EventArgs e)
        {
            TwainScanCommon twCom;

            twCom = new TwainScanCommon("NewScanImg", this.Handle);
            twCom.PassDataBetweenForm += new TwainScanCommon.PassDataBetweenFormHandler(twCom_PassDataBetweenForm);

            //是否连续扫描
            twCom.bContinuousScan = ckbSeries.Checked;

            this.Enabled = false;
            twCom.StartScan();
        }

        private void twCom_PassDataBetweenForm(object sender, PassDataEventArgs e)
        {
            if (e.EventStr == "exit")
            {
                this.Cursor = Cursors.WaitCursor;

                //向界面展示图片的一些代码
                //.......

                this.Cursor = Cursors.Default;
                this.Enabled = true;
                GC.Collect();
            }
        }
    }
}
