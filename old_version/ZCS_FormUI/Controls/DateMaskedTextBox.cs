using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ZCS_FormUI.Controls
{
    public partial class DateMaskedTextBox : MaskedTextBox
    {
        private int thisX = 0;
        private int thisY = 0;

        private DateTimePicker _dtp = null;

        public DateTimePicker Dtp
        {
            get { return _dtp; }
        }

        private Button _btn = null;

        public Button Btn
        {
            get { return _btn; }
        }

        private DateTime _value;
        public DateTime Value
        {
            set 
            { 
                DateTime d;
                if (!DateTime.TryParse(value.ToString(),out d))
                {
                    this._value = d;
                    this.Text = d.ToString("yyyy-MM-dd");
                    switch (this.Mask)
                    {
                        case "0000-00":
                            this.Text = d.ToString("yyyy-MM");
                            break;
                        case "0000-00-00":
                            this.Text = d.ToString("yyyy-MM-dd");
                            break;
                        case "0000-00-00 00:00:00":
                            this.Text = d.ToString("yyyy-MM-dd HH:mm:ss");
                            break;
                    }
                }
                
            }
            get 
            { 
                //return Convert.ToDateTime(this.Text); 
                DateTime d;
                if (DateTime.TryParse(this.Text.ToString(), out d))
                {
                    return d;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }


        public DateMaskedTextBox()
        {
            InitializeComponent();
            this.Mask = "0000-00-00";

            Init();
        }

        public DateMaskedTextBox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            Init();
        }

        internal void Init()
        {
            if (this.Mask != "0000-00-00" && this.Mask != "0000-00" && this.Mask!="0000-00-00 00:00:00")
            {
                this.Mask = "0000-00-00";
            }

            this._btn = new Button();
            Btn.Hide();
            Btn.Image = ((System.Drawing.Image)(global::ZCS_FormUI.Properties.Resources.calendar1));
            Btn.ImageAlign = ContentAlignment.MiddleCenter;
            Btn.FlatStyle = FlatStyle.Flat;
            Btn.FlatAppearance.BorderSize = 0;
            Btn.TabStop = false;


            _dtp = new DateTimePicker();
            Dtp.Hide();
            Dtp.Format = DateTimePickerFormat.Custom;
            switch (this.Mask)
            {
                case "0000-00":
                    Dtp.CustomFormat = "yyyy-MM";
                    break;
                case "0000-00-00":
                    Dtp.CustomFormat = "yyyy-MM-dd";
                    break;
                case "0000-00-00 00:00:00":
                    Dtp.CustomFormat = "yyyy-MM-dd HH:mm:ss";
                    break;
            }

            Btn.Click += new EventHandler(Btn_Click);

            Dtp.CloseUp += new EventHandler(Dtp_CloseUp);

            this.GotFocus += new EventHandler(DateMaskedTextBox_GotFocus);
            this.Validating += new CancelEventHandler(DateMaskedTextBox_Validating);
            this.EnabledChanged += new EventHandler(DateMaskedTextBox_EnabledChanged);
        }

        /// <summary>
        /// 判断是否为空，为空返回true否则返回false
        /// </summary>
        /// <returns><c>true</c> if this instance is null; otherwise, <c>false</c>.</returns>
        public bool IsNull()
        {
            if (this.Text != "    -  -" && this.Text != "    -" && this.Text != "    -  -     :  :")
            {
                return false ;
            }
            else
            {
                return true;
            }
        }

        void DateMaskedTextBox_EnabledChanged(object sender, EventArgs e)
        {
            Btn.Enabled = this.Enabled;
        }

        void DateMaskedTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (this.Text != "    -  -" && this.Text != "    -" && this.Text != "    -  -     :  :")
            {
                DateTime d;
                if (!DateTime.TryParse(this.Text, out d))
                {
                    
                    if (!this._btn.ContainsFocus)
                    {
                        e.Cancel = true;
                        MessageBox.Show("输入的日期不合法！");
                    }
                }
                
            }
            else
            {
                e.Cancel = false;
            }
        }


        void Dtp_CloseUp(object sender, EventArgs e)
        {
            this.Text = Dtp.Text;
        }

        void DateMaskedTextBox_GotFocus(object sender, EventArgs e)
        {
            if (!Btn.Visible)
            {
                thisX = 0;
                thisY = 0;
                controlParent(this);

                Btn.Size = new System.Drawing.Size(18, this.Height - 2);
                Btn.FlatStyle = FlatStyle.Flat;
                Btn.Location = new Point(thisX + this.Width - (Btn.Width + 4), this.Location.Y+1);
                Btn.Parent = this.Parent;
                Btn.BringToFront();
                Btn.Show();
            }
            this.SelectAll();
        }

        void Btn_Click(object sender, EventArgs e)
        {
            switch (this.Mask)
            {
                case "0000-00":
                    Dtp.CustomFormat = "yyyy-MM";
                    break;
                case "0000-00-00":
                    Dtp.CustomFormat = "yyyy-MM-dd";
                    break;
                case "0000-00-00 00:00:00":
                    Dtp.CustomFormat = "yyyy-MM-dd HH:mm:ss";
                    break;
            }

            if (!Dtp.Visible)
            {
                thisX = 0;
                thisY = 0;
                controlParent(this);

                Dtp.Size = new System.Drawing.Size(0, 0);
                Dtp.Location = new Point(thisX - 1, this.Location.Y);
                Dtp.Parent = this.Parent;
                Dtp.Show();
            }
            Dtp.Focus();
            SendKeys.Send("%{DOWN}");
        }


        /// <summary>
        /// 判断控件的父容器，取Location值
        /// </summary>
        /// <param name="con"></param>
        private void controlParent(Control con)
        {
            if (con != null && con.Name != this.FindForm().Name)
            {
                thisX += con.Location.X;
                thisY += con.Location.Y;

                controlParent(con.Parent);
            }
        }
    }
}
