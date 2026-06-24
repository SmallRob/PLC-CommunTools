using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ZCS_FormUI.Controls
{
    public partial class ButtonEx : Button
    {
        public ButtonEx()
        {
            //this.UseVisualStyleBackColor = false;
            InitializeComponent();
        }

        public ButtonEx(IContainer container)
        {
            container.Add(this);
            //this.ForeColor = System.Drawing.Color.FromArgb(149,77,28);

            InitializeComponent();


        }



        protected override void OnPaint(PaintEventArgs e)
        {
            // base.OnPaint(e);
            // GraphicsPath path = new GraphicsPath();
            //e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // int x = this.ClientRectangle.X, y = this.ClientRectangle.Y, w = this.ClientRectangle.Width, h = this.ClientRectangle.Height, m = 2;   
            //path.AddArc(x, y, m, m, 180, 90);
            //path.AddArc(x + w - m, y, m, m, 270, 90);
            //path.AddArc(x + w - m, y + h - m, m, m, 0, 90);    
            //path.AddArc(x, y + h - m, m, m, 90, 90);    
            //path.CloseFigure();
            //this.Region = new Region(path);

            //e.Graphics.DrawPath(new Pen(Color.FromArgb(237, 144, 52), 2), path);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = this.ClientRectangle;

            bool mouseOver = e.ClipRectangle.Contains(this.PointToClient(Cursor.Position)) || this.Focused;
            //重绘背景
            ZCS_FormUI.Render.RenderHelper.RenderBackgroundInternal(
                         g,
                         rect,
                         Enabled ? (mouseOver ? Color.FromArgb(65, 105, 225 /*237, 77, 28*/) : Color.FromArgb(100, 149, 237/*237, 144, 52*/)) : Color.FromArgb(183, 179, 183),
                         Enabled ? Color.FromArgb(30, 144, 255 /*237, 144, 52*/) : Color.FromArgb(183, 179, 183),
                         Enabled ? Color.FromArgb(30, 144, 255 /*237, 144, 52*/) : Color.FromArgb(183, 179, 183),
                        ZCS_FormUI.Render.RoundStyle.All,
                         true,
                         true,
                         LinearGradientMode.Vertical);
            //重绘文本
            using (SolidBrush brush = new SolidBrush(Enabled ? Color.FromArgb(149, 77, 28) : Color.FromArgb(109, 109, 109)))
            {
                StringFormat strformat = new StringFormat();
                strformat.Alignment = StringAlignment.Center;
                strformat.LineAlignment = StringAlignment.Center;

                g.DrawString(this.Text, this.Font, brush, rect, strformat);
            }
        }


    }
}