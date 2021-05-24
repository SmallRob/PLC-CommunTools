using ZCS_FormUI.Function;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ZCS_FormUI.Controls
{
    public partial class ImageCroppingBox : Control
    {
        public ImageCroppingBox()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.Size = new Size(200, 150);
        }

        private Image _Image;
        /// <summary>
        /// 要裁剪的图像
        /// </summary>
        [Description("要裁剪的图像"), Category("Customs")]
        public Image Image
        {
            get { return _Image; }
            set
            {
                if (value == this._Image) return;
                _Image = value;
                this.Clear();
            }
        }

        private Color _MaskColor = Color.FromArgb(125, 0, 0, 0);
        /// <summary>
        /// 遮罩颜色
        /// </summary>
        [Description("遮罩颜色"), Category("Customs")]
        public Color MaskColor
        {
            get { return _MaskColor; }
            set
            {
                if (_MaskColor == value) return;
                _MaskColor = value;
                if (this._Image != null) this.Invalidate();
            }
        }

        private bool _IsLockSelected;
        /// <summary>
        /// 是否锁定当前选择 锁定后无法重新拖选区域
        /// </summary>
        [Description("是否锁定当前选择 锁定后无法重新拖选区域"), Category("Customs")]
        public bool IsLockSelected
        {
            get { return _IsLockSelected; }
            set { _IsLockSelected = value; }
        }

        private bool _IsSetClip;
        /// <summary>
        /// 是否限定在拖动区域时 限定鼠标范围
        /// </summary>
        [Description("是否限定在拖动区域时 限定鼠标范围"), Category("Customs")]
        public bool IsSetClip
        {
            get { return _IsSetClip; }
            set { _IsSetClip = value; }
        }

        private bool _IsDrawMagnifier;
        /// <summary>
        /// 是否显示放大镜
        /// </summary>
        [Description("是否显示放大镜"), Category("Customs")]
        public bool IsDrawMagnifier
        {
            get { return _IsDrawMagnifier; }
            set { _IsDrawMagnifier = value; }
        }

        private bool _IsDrawed;
        /// <summary>
        /// 获取当前是否已经选择区域
        /// </summary>
        [Description("获取当前是否已经选择区域"), Category("Customs")]
        public bool IsDrawed
        {
            get { return _IsDrawed; }
        }

        private Rectangle _SelectedRectangle;
        /// <summary>
        /// 获取或设置悬着区域
        /// </summary>
        [Description("获取或设置选择的区域"), Category("Customs")]
        public Rectangle SelectedRectangle
        {
            get { return _SelectedRectangle; }
            set
            {
                if (this._IsSetClip)
                    value.Intersect(this.DisplayRectangle);
                if (this._SelectedRectangle == value) return;
                this._SelectedRectangle = value;
                m_ptTempForMove = value.Location;
                this.Invalidate();
                this._IsDrawed = true;
            }
        }

        private Point m_ptStart;        //起始点位置
        private Point m_ptCurrent;      //当前鼠标位置
        private Point m_ptTempForMove;  //移动选框的时候 临时用
        private Rectangle m_rectClip;   //限定鼠标活动的区域
        private Rectangle[] m_rectDots = new Rectangle[8];  //八个控制点

        protected bool m_bMoving;
        protected bool m_bChangeWidth;
        protected bool m_bChangeHeight;
        protected bool m_bMouseHover;
        //初始化控件
        protected override void OnCreateControl()
        {
            for (int i = 0; i < 8; i++)
            {
                m_rectDots[i].Size = new Size(5, 5);
            }
            this.BackColor = Color.Black;
            this._IsSetClip = true;
            //this._IsDrawMagnifier = true;
            base.OnCreateControl();
            this.MouseEnter += (s, e) => m_bMouseHover = true;
            this.MouseLeave += (s, e) =>
            {
                m_bMouseHover = false;
                if (this._IsDrawMagnifier) this.Invalidate();
            };
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this._Image == null || this._IsLockSelected)
            {
                base.OnMouseDown(e);
                return;//Image属性null或者已经锁定选择 直接返回
            }
            //如果限定了鼠标范围 判断若不在限定范围内操作 返回
            if (this._IsSetClip)
            {
                if (e.X > this._Image.Width || e.Y > this._Image.Height)
                {
                    base.OnMouseDown(e);
                    return;
                }//否则计算需要限定鼠标的区域
                m_rectClip = this.DisplayRectangle;
                Size sz = this.Size;
                if (this._Image != null) sz = this._Image.Size;
                m_rectClip.Intersect(new Rectangle(Point.Empty, sz));
                m_rectClip.Width++; m_rectClip.Height++;
                Cursor.Clip = RectangleToScreen(m_rectClip);
            }

            m_ptStart = e.Location;
            m_bChangeHeight = true;
            m_bChangeWidth = true;
            //如果 已经选择区域 若鼠标点下 判断是否在控制顶点上
            if (this._IsDrawed)
            {
                this._IsDrawed = false; //默认表示 要更改选取设置 清楚IsDrawed属性
                if (m_rectDots[0].Contains(e.Location))
                {
                    m_ptStart.X = this._SelectedRectangle.Right;
                    m_ptStart.Y = this._SelectedRectangle.Bottom;
                }
                else if (m_rectDots[1].Contains(e.Location))
                {
                    m_ptStart.Y = this._SelectedRectangle.Bottom;
                    m_bChangeWidth = false;
                }
                else if (m_rectDots[2].Contains(e.Location))
                {
                    m_ptStart.X = this._SelectedRectangle.X;
                    m_ptStart.Y = this._SelectedRectangle.Bottom;
                }
                else if (m_rectDots[3].Contains(e.Location))
                {
                    m_ptStart.X = this._SelectedRectangle.Right;
                    m_bChangeHeight = false;
                }
                else if (m_rectDots[4].Contains(e.Location))
                {
                    m_ptStart.X = this._SelectedRectangle.X;
                    m_bChangeHeight = false;
                }
                else if (m_rectDots[5].Contains(e.Location))
                {
                    m_ptStart.X = this._SelectedRectangle.Right;
                    m_ptStart.Y = this._SelectedRectangle.Y;
                }
                else if (m_rectDots[6].Contains(e.Location))
                {
                    m_ptStart.Y = this._SelectedRectangle.Y;
                    m_bChangeWidth = false;
                }
                else if (m_rectDots[7].Contains(e.Location))
                {
                    m_ptStart = this._SelectedRectangle.Location;
                }
                else if (this._SelectedRectangle.Contains(e.Location))
                {
                    m_bMoving = true;
                    m_bChangeWidth = false;
                    m_bChangeHeight = false;
                }
                else { this._IsDrawed = true; }   //若以上条件不成立 表示不需要更改设置 
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            m_ptCurrent = e.Location;
            if (this._Image == null || this._IsLockSelected)
            {
                base.OnMouseMove(e);
                return;
            }
            if (this._IsDrawed)
            {//如果已经绘制 移动过程中判断是否需要设置鼠标样式
                this.SetCursorStyle(e.Location);
            }
            else if (e.Button == MouseButtons.Left)
            {//否则可能表示在选择区域或重置大小
                if (m_bChangeWidth)
                {//是否允许选区宽度改变 如重置大小时候 拉动上边和下边中点时候
                    this._SelectedRectangle.X = e.Location.X > m_ptStart.X ? m_ptStart.X : e.Location.X;
                    this._SelectedRectangle.Width = Math.Abs(e.Location.X - m_ptStart.X);
                }
                if (m_bChangeHeight)
                {
                    this._SelectedRectangle.Y = e.Location.Y > m_ptStart.Y ? m_ptStart.Y : e.Location.Y;
                    this._SelectedRectangle.Height = Math.Abs(e.Location.Y - m_ptStart.Y);
                }
                if (m_bMoving)
                {//如果是移动选区 判断选区移动范围
                    int tempX = m_ptTempForMove.X + e.X - m_ptStart.X;
                    int tempY = m_ptTempForMove.Y + e.Y - m_ptStart.Y;
                    if (this._IsSetClip)
                    {
                        if (tempX < 0) tempX = 0;
                        if (tempY < 0) tempY = 0;
                        if (this._SelectedRectangle.Width + tempX >= m_rectClip.Width) tempX = m_rectClip.Width - this._SelectedRectangle.Width - 1;
                        if (this._SelectedRectangle.Height + tempY >= m_rectClip.Height) tempY = m_rectClip.Height - this._SelectedRectangle.Height - 1;
                    }
                    this._SelectedRectangle.X = tempX;
                    this._SelectedRectangle.Y = tempY;
                }
                this.Invalidate();
            }
            else if (this._IsDrawMagnifier && !this._IsDrawed)
            {
                this.Invalidate();//否则 在需要绘制放大镜并且还没有选好区域同时 都重绘
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this._IsDrawed = !this._SelectedRectangle.IsEmpty;
            m_ptTempForMove = this._SelectedRectangle.Location;
            m_bMoving = false;
            m_bChangeWidth = false;
            m_bChangeHeight = false;
            if (this._IsSetClip) Cursor.Clip = Rectangle.Empty;
            this.Invalidate();
            base.OnMouseUp(e);
        }
        //绘制控件
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (this._Image != null)
            {
                g.DrawImage(this._Image, Point.Empty);//原图
                using (SolidBrush sb = new SolidBrush(this._MaskColor))
                {
                    g.FillRectangle(sb, this.ClientRectangle);//遮罩
                }
                if (!this._SelectedRectangle.IsEmpty)
                    this.DrawSelectedRectangle(g);//选框
                if (this._IsDrawMagnifier && m_bMouseHover && !_IsDrawed && !m_bMoving)
                    this.DrawMagnifier(g, m_ptCurrent);//放大镜
            }
            base.OnPaint(e);
        }
        /// <summary>
        /// 判断鼠标当前位置显示样式
        /// </summary>
        /// <param name="pt">鼠标坐标</param>
        protected virtual void SetCursorStyle(Point pt)
        {
            if (m_rectDots[0].Contains(pt) || m_rectDots[7].Contains(pt))
                this.Cursor = Cursors.SizeNWSE;
            else if (m_rectDots[1].Contains(pt) || m_rectDots[6].Contains(pt))
                this.Cursor = Cursors.SizeNS;
            else if (m_rectDots[2].Contains(pt) || m_rectDots[5].Contains(pt))
                this.Cursor = Cursors.SizeNESW;
            else if (m_rectDots[3].Contains(pt) || m_rectDots[4].Contains(pt))
                this.Cursor = Cursors.SizeWE;
            else if (this._SelectedRectangle.Contains(pt))
                this.Cursor = Cursors.SizeAll;
            else
                this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// 绘制选框
        /// </summary>
        /// <param name="g">绘图表面</param>
        protected virtual void DrawSelectedRectangle(Graphics g)
        {
            m_rectDots[0].Y = m_rectDots[1].Y = m_rectDots[2].Y = this._SelectedRectangle.Y - 2;
            m_rectDots[5].Y = m_rectDots[6].Y = m_rectDots[7].Y = this._SelectedRectangle.Bottom - 2;
            m_rectDots[0].X = m_rectDots[3].X = m_rectDots[5].X = this._SelectedRectangle.X - 2;
            m_rectDots[2].X = m_rectDots[4].X = m_rectDots[7].X = this._SelectedRectangle.Right - 2;
            m_rectDots[3].Y = m_rectDots[4].Y = this._SelectedRectangle.Y + this._SelectedRectangle.Height / 2 - 2;
            m_rectDots[1].X = m_rectDots[6].X = this._SelectedRectangle.X + this._SelectedRectangle.Width / 2 - 2;

            g.DrawImage(this._Image, this._SelectedRectangle, this._SelectedRectangle, GraphicsUnit.Pixel);
            g.DrawRectangle(Pens.Cyan, this._SelectedRectangle.Left, this._SelectedRectangle.Top, this._SelectedRectangle.Width - 1, this._SelectedRectangle.Height - 1);
            foreach (Rectangle rect in m_rectDots)
                g.FillRectangle(Brushes.Yellow, rect);
            string str = string.Format("X:{0} Y:{1} W:{2} H:{3}",
                this._SelectedRectangle.Left, this._SelectedRectangle.Top, this._SelectedRectangle.Width, this._SelectedRectangle.Height);
            Size szStr = g.MeasureString(str, this.Font).ToSize();
            Point ptStr = new Point(this._SelectedRectangle.Left, this._SelectedRectangle.Top - szStr.Height - 5);
            if (ptStr.Y < 0) ptStr.Y = this._SelectedRectangle.Top + 5;
            if (ptStr.X + szStr.Width > this.Width) ptStr.X = this.Width - szStr.Width;
            using (SolidBrush sb = new SolidBrush(Color.FromArgb(125, 0, 0, 0)))
            {
                g.FillRectangle(sb, new Rectangle(ptStr, szStr));
                g.DrawString(str, this.Font, Brushes.White, ptStr);
            }
        }
        /// <summary>
        /// 绘制放大镜
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="pt">待放大点的位置</param>
        protected virtual void DrawMagnifier(Graphics g, Point pt)
        {
            Color clr = Color.Transparent;
            if (this._Image != null && pt.X < this._Image.Width && pt.Y < this._Image.Height)
                clr = ((Bitmap)this._Image).GetPixel(pt.X, pt.Y);
            Bitmap bmpSrc = new Bitmap(15, 15);//创建放大点原始位图
            using (Graphics gb = Graphics.FromImage(bmpSrc))
            {
                gb.DrawImage(
                    this._Image,
                    new Rectangle(Point.Empty, bmpSrc.Size),
                    new Rectangle(pt.X - 7, pt.Y - 7, 15, 15),
                    GraphicsUnit.Pixel);
            }
            Image bmpNew = ImageFunc.ZoomImage(bmpSrc, 7F);//将图放大7倍
            //放大镜区域
            Rectangle rect = new Rectangle(pt.X + 20, pt.Y + 20, bmpNew.Width + 6, bmpNew.Height + 6 + this.Font.Height * 3);
            if (rect.Right > this.Width) rect.X = pt.X - rect.Width - 10;
            if (rect.Bottom > this.Height) rect.Y = pt.Y - rect.Height - 10;
            //区域背景和图像先刷上去
            SolidBrush sb = new SolidBrush(Color.FromArgb(125, 0, 0, 0));
            g.FillRectangle(sb, rect);
            g.DrawImage(bmpNew, rect.X + 3, rect.Y + 3);
            //反正下面就是各种巴拉巴拉的装饰、、、
            using (Pen pen = new Pen(Color.FromArgb(125, 0, 255, 255), 5))
            {
                g.DrawLine(pen, rect.X + 3, rect.Y + 3 + (bmpNew.Height >> 1), rect.Right - 3, rect.Y + 3 + (bmpNew.Height >> 1));
                g.DrawLine(pen, rect.X + 3 + (bmpNew.Width >> 1), rect.Y + 3, rect.X + 3 + (bmpNew.Width >> 1), rect.Y + 3 + bmpNew.Height);
                pen.Color = Color.White; pen.Width = 1;
                g.DrawRectangle(pen, rect.X + 1, rect.Y + 1, rect.Width - 3, bmpNew.Height + 3);

                pen.Color = Color.Cyan;
                g.DrawRectangle(pen, rect.Right - 12, rect.Bottom - 12, 10, 10);
                g.DrawRectangle(pen, rect.X + (bmpNew.Width >> 1) - 1, rect.Y + (bmpNew.Height >> 1) - 1, 8, 8);

            }
            string strDraw = "size:" + (this._SelectedRectangle.Width) + " * " + (this._SelectedRectangle.Height)
                + "\r\n" + clr.A + "," + clr.R + "," + clr.G + "," + clr.B
                + "\r\n0x" + clr.A.ToString("X").PadLeft(2, '0') + clr.R.ToString("X").PadLeft(2, '0') + clr.G.ToString("X").PadLeft(2, '0') + clr.B.ToString("X").PadLeft(2, '0');
            g.DrawString(strDraw, this.Font, Brushes.White, rect.X + 3, rect.Y + 6 + bmpNew.Height);
            sb.Color = clr;
            g.FillRectangle(sb, rect.Right - 11, rect.Bottom - 11, 9, 9);
            g.FillRectangle(sb, rect.X + (bmpNew.Width >> 1), rect.Y + (bmpNew.Height >> 1), 7, 7);

            sb.Dispose();
            bmpNew.Dispose();
            bmpSrc.Dispose();
        }
        /// <summary>
        /// 获取当前选择区域的图像
        /// </summary>
        /// <returns>当前选择区域图像</returns>
        public virtual Image GetSelectedImage()
        {
            if (this._Image == null) return null;
            if (this._SelectedRectangle.Size == Size.Empty) return null;
            Image img = new Bitmap(this._SelectedRectangle.Width, this._SelectedRectangle.Height);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.DrawImage(this._Image,
                    new Rectangle(Point.Empty, img.Size),
                    new Rectangle(this._SelectedRectangle.Location, img.Size),
                    GraphicsUnit.Pixel);
            }
            return img;
        }
        /// <summary>
        /// 清空选择
        /// </summary>
        public virtual void Clear()
        {
            m_bMoving = false;
            m_bChangeWidth = false;
            m_bChangeHeight = false;
            this._IsDrawed = false;
            this._IsLockSelected = false;
            this._SelectedRectangle = Rectangle.Empty;
            this.Cursor = Cursors.Default;
            this.Invalidate();
        }
    }
}
