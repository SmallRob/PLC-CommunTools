using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace ZCS_FormUI.Controls
{
    public partial class CheckBoxEx : CheckBox
    {
        public CheckBoxEx()
        {
            this.ForeColor = System.Drawing.Color.Black;
            this.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            InitializeComponent();
        }

        public CheckBoxEx(IContainer container)
        {
            container.Add(this);
            this.ForeColor = System.Drawing.Color.Black;
            this.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            InitializeComponent();
        }
    }
}
