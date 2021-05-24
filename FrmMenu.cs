using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static CommunTools.Enum.FuncItemCom;

namespace CommunTools
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private readonly static object[] fcunArr = new Type[2] { typeof(Com_BaseFuncItem), typeof(Com_ProtoFuncItem) };

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            foreach (var item in fcunArr)
            {

            }
        }
    }
}
