using CommunTools.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using static CommunTools.Enum.FuncItemCom;

namespace CommunTools
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private readonly static List<Type> fcunArr = new List<Type>() { typeof(Com_BaseFuncItem), typeof(Com_ProtoFuncItem) };

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            foreach (Type item in fcunArr)
            {
                AttributesContext<Type> attr = new AttributesContext<Type>();
                attr.XGroup(item);
            }
        }
    }
}
