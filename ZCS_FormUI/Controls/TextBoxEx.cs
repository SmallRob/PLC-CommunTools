using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace ZCS_FormUI.Controls
{
    public partial class TextBoxEx : TextBox
    {
        TextBox textbox;
        //private bool _IsTimer = false;
        public TextBoxEx()
        {
            InitializeComponent();
        }

        public TextBoxEx(IContainer container)
        {
            container.Add(this);

            //this.BackColor = System.Drawing.Color.FromArgb(255, 235, 205);
            InitializeComponent();
            Init();
        }

        #region 属性
        private string _otherText;
        /// <summary>
        /// 获取设置其他自定义内容
        /// </summary>
        [Category("Custom"), Browsable(true), Description("获取设置其他自定义内容")]
        public string OtherText
        {
            get { return _otherText; }
            set { _otherText = value; }
        }

        #endregion

        private string strText = string.Empty;

        private string thistext = string.Empty;

        Timer timer1 = new Timer();
        private void Init()
        {

            textbox = new TextBox();
            textbox.Hide();
            textbox.BringToFront();

            this.EnabledChanged += new EventHandler(TextBoxEx_EnabledChanged);
            this.TextChanged += new EventHandler(TextBoxEx_TextChanged);
            timer1.Interval = 600;
            timer1.Tick += new EventHandler(timer1_Tick);

        }

        void TextBoxEx_TextChanged(object sender, EventArgs e)
        {

            if (strText != this.Text)
            {
                strText = this.Text + "  ";
                thistext = this.Text;
            }
            StartTimer();
        }

        private void StartTimer()
        {
            Graphics graphics = CreateGraphics();
            Size sif = TextRenderer.MeasureText(graphics, this.Text, this.Font, new Size(0, 0), TextFormatFlags.NoPadding);

            if (!this.Multiline && sif.Width > this.Width && (!this.Enabled || this.ReadOnly) && this.Visible)
            {
                if (!timer1.Enabled)
                {
                    textbox.Location = this.Location;

                    textbox.Size = this.Size;
                    textbox.Font = this.Font;
                    textbox.ReadOnly = this.ReadOnly;

                    textbox.Parent = this.Parent;
                    textbox.BringToFront();

                    textbox.Show();

                    timer1.Start();
                    //_IsTimer = true;
                }
            }
            else
            {
                timer1.Enabled = false;
                textbox.Hide();
            }
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Text.Length > 0)
            {

                textbox.Text = strText.Substring(1) + strText.Substring(0, 1);
                strText = textbox.Text;
                //this.Text = strText.Substring(1) + strText.Substring(0, 1);
                //strText = this.Text;
            }
        }

        internal void TextBoxEx_EnabledChanged(object sender, EventArgs e)
        {

            if (!this.Enabled)
            {
                //this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                //this.BackColor = System.Drawing.Color.White;
            }
            else
            {
                //this.Font = new System.Drawing.Font("宋体", 9F, FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                //this.BackColor = System.Drawing.Color.FromArgb(255, 235, 205);
                //this.BackColor = Color.White;

            }
        }

        protected override void InitLayout()
        {
            base.InitLayout();
            if (!Enabled)
            {
                //this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                //BackColor = Color.White;
            }
        }
    }
}
