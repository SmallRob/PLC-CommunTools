using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZCS_FormUI.Controls
{
    public partial class FuncItemBox : UserControl
    {
        public FuncItemBox()
        {
            InitializeComponent();
        }

        public FuncItemBox(int size, Color color)
            : this()
        {
            this.Size = new Size(size, size);
            vatioNum = size / 68;
            this.color = color;
            this.Refresh();
        }

        private Color color;
        private double vatioNum = 1; //缩放比率

        private int getVatioSize(int orginSize)
        {
            return Convert.ToInt32(Math.Floor(orginSize * vatioNum));
        }

        public void UnEnableBox(bool isEnable)
        {
            if (isEnable)
            {
                ShowItems("", "");
            }
            else
            {
                pnlBox.BackColor = Color.FromArgb(224, 224, 224);
                this.lblFuncItem.Visible = false;
                this.lblFuncMark.Visible = false;
            }
        }

        public void InitBox()
        {
            ShowItems("", "");
        }

        public void NewBox(string txt, string desc)
        {
            ShowItems(txt, desc);
        }

        public void ResetColor()
        {
            pnlBox.BackColor = color == null ? Color.Gold : color;
        }

        public void ResetBox()
        {
            if (this.pnlBox.BackColor == Color.Chartreuse)
            {
                pnlBox.BackColor = Color.Gold;
                this.lblFuncItem.Visible = false;
                this.lblFuncMark.Visible = false;
            }
        }

        public void ShowItems(string itemTxt, string itemMark)
        {
            bool isMark = string.IsNullOrWhiteSpace(itemMark) ? false : true;

            this.lblFuncItem.Location = isMark ? new Point(6, 4) : new Point(6, 9);
            //new Point(getVatioSize(17), getVatioSize(4)) : new Point(getVatioSize(23), getVatioSize(4));

            this.lblFuncMark.Visible = isMark;
            this.lblFuncItem.BringToFront();

            this.lblFuncItem.Text = itemTxt;
            this.lblFuncMark.Text = itemMark;
        }
    }
}
