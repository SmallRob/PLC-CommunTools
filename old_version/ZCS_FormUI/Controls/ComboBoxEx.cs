using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZCS_FormUI.Controls
{
    public partial class ComboBoxEx : ComboBox
    {
        public ComboBoxEx()
        {
            InitializeComponent();
            this.ForeColor = System.Drawing.Color.Black;
            this.DropDownStyle = ComboBoxStyle.DropDown;
            this.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyUp += new KeyEventHandler(ComboBoxEx_KeyUp);
            this.KeyDown += new KeyEventHandler(ComboBoxEx_KeyDown);

            GC.KeepAlive(this);
        }

        #region 属性
        private bool _isInput = false;
        /// <summary>
        /// 获取设置其他自定义内容
        /// </summary>
        [Category("Custom"), Browsable(true), Description("是否可以输入？默认false")]
        public bool IsInput
        {
            get { return _isInput; }
            set { _isInput = value; }

        }
        #endregion

        private int indexid = -1;

        public ComboBoxEx(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            this.ForeColor = System.Drawing.Color.Black;
            this.DropDownStyle = ComboBoxStyle.DropDown;
            this.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyUp += new KeyEventHandler(ComboBoxEx_KeyUp);
            this.KeyDown += new KeyEventHandler(ComboBoxEx_KeyDown);
        }

        void ComboBoxEx_KeyDown(object sender, KeyEventArgs e)
        {
            indexid = this.SelectedIndex;
        }

        void ComboBoxEx_KeyUp(object sender, KeyEventArgs e)
        {
            if (!IsInput)
            {
                //MessageBox.Show(e.KeyCode+"\r\n"+e.KeyValue);
                //MessageBox.Show(this.SelectedIndex.ToString());
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
                {
                }
                else
                {
                    if (SelectedIndex == -1)
                    {
                        this.SelectedIndex = indexid;
                        if (indexid == -1)
                            this.Text = "";
                    }
                }
            }
        }
    }
}
