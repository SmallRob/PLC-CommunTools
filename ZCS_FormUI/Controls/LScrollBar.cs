using ZCS_FormUI.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZCS_FormUI.Controls
{
    /// <summary>
    /// 滚动条控件
    /// </summary>
    [DefaultEvent("LScrolled")]
    public class LScrollBar : Control
    {
        /// <summary>
        /// LScrollBar控件类
        /// </summary>
        public LScrollBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            CreateControl();
        }

        #region 

        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void LScrolledEventHandler(object sender, LScrollEventArgs e);
        /// <summary>
        /// 发生滚动时引发的事件
        /// </summary>
        public event LScrolledEventHandler LScrolled;

        #endregion

        #region Property

        private Color _BarColor = Color.FromArgb(224, 224, 224);//浅灰
        /// <summary>
        /// 背景条颜色
        /// </summary>
        [Category("DemoUI"), Description("背景条颜色")]
        public Color L_BarColor
        {
            get { return _BarColor; }
            set
            {
                _BarColor = value;
                Invalidate();
            }
        }

        private Color _SliderColor = Color.FromArgb(0, 192, 0);//绿
        /// <summary>
        /// 滑块颜色
        /// </summary>
        [Category("DemoUI"), Description("滑块颜色")]
        public Color L_SliderColor
        {
            get { return _SliderColor; }
            set
            {
                _SliderColor = value;
                colorDrawSlider = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 鼠标进入时滑块颜色<para>默认：绿</para>
        /// </summary>
        [Category("DemoUI"), Description("鼠标进入时滑块颜色\r\n默认：绿")]
        public Color L_SliderMouseInColor { get; set; } = Color.Green;//绿

        private bool _IsRound = true;
        /// <summary>
        /// 是否是圆角<para>默认：是</para>
        /// </summary>
        [Category("DemoUI"), Description("是否是圆角\r\n默认：是")]
        public bool L_IsRound
        {
            get { return _IsRound; }
            set
            {
                _IsRound = value;
                Invalidate();
            }
        }

        private OrientationScrollBar _Orientation = OrientationScrollBar.Vertical;
        /// <summary>
        /// 方向<para>默认：水平，从左到右</para>
        /// </summary>
        [Category("DemoUI"), Description("方向\r\n默认：水平，从左到右")]
        public OrientationScrollBar L_Orientation
        {
            get { return _Orientation; }
            set
            {
                OrientationScrollBar old = _Orientation;
                _Orientation = value;
                if (old == OrientationScrollBar.Horizontal && _Orientation == OrientationScrollBar.Vertical)
                {
                    Size = new Size(Size.Height, Size.Width);
                }

                if (_Orientation == OrientationScrollBar.Horizontal && old == OrientationScrollBar.Vertical)
                {
                    Size = new Size(Size.Height, Size.Width);
                }
                Invalidate();
            }
        }

        private int _BarSize = 10;
        /// <summary>
        /// 滑条高度（水平）/宽度（垂直）
        /// </summary>
        [Category("DemoUI"), Description("滑条高度（水平）/宽度（垂直")]
        public int L_BarSize
        {
            get { return _BarSize; }
            set
            {
                _BarSize = value;
                if (_BarSize < 1) _BarSize = 1;
                if (_Orientation == OrientationScrollBar.Horizontal)
                {
                    Size = new Size(Width, _BarSize);
                }
                else
                {
                    Size = new Size(_BarSize, Height);
                }
            }
        }

        /// <summary>
        /// 滑块位置
        /// </summary>
        [Category("DemoUI"), Description("滑块位置"), Browsable(false)]
        public float L_Position { get; private set; } = 0f;

        private float _PageSize = 1f;
        /// <summary>
        /// 显示长度/每页长度
        /// </summary>
        [Category("DemoUI"), Description("显示长度/每页长度")]
        public float L_PageSize
        {
            get { return _PageSize; }
            set
            {
                _PageSize = value;
                pInit();
                pChangeSliderLocation();
                Invalidate();
            }
        }

        private float _DocSize = 1f;
        /// <summary>
        /// 文档长度
        /// </summary>
        [Category("DemoUI"), Description("文档长度")]
        public float L_DocSize
        {
            get { return _DocSize; }
            set
            {
                _DocSize = value;
                pInit();
                pChangeSliderLocation();
                Invalidate();
            }
        }

        /// <summary>
        /// 滑块可移动距离
        /// </summary>
        [Category("DemoUI"), Description("滑块可移动距离"), Browsable(false)]
        public float L_SliderMoveRange { get; private set; }
        /// <summary>
        /// 滑块长度
        /// </summary>
        [Category("DemoUI"), Description("滑块长度"), Browsable(false)]
        public float L_SliderLength { get; private set; }
        private float _ShowPosition = 0f;
        /// <summary>
        /// 显示位置（正数）
        /// </summary>
        [Category("DemoUI"), Description("显示位置（正数）"), Browsable(false)]
        public float L_ShowPosition
        {
            get { return _ShowPosition; }
            set
            {
                _ShowPosition = value;
                pShowPositionToPosition();
                Invalidate();
            }
        }

        /// <summary>
        /// 鼠标滚轮和上下左右键每次移动距离（对文档视图言）<para>默认：10</para>
        /// </summary>
        [Category("LESLIE_UI"), Description("鼠标滚轮和上下左右键每次移动距离（对文档视图言）\r\n默认：10")]
        public float L_ScrollInterval { get; set; } = 10f;

        /// <summary>
        /// 滑块最小长度<para>默认：20</para>
        /// </summary>
        [Category("DemoUI"), Description("滑块最小长度\r\n默认：20")]
        public float L_SliderMiniSize { get; set; } = 20f;
        #endregion

        #region 

        private float fPageDocRatio, fShowScrollRatio, fAbove, fBelow, fCapWidth, fCapHalfWidth;

        private MouseStatus mouseStatus = MouseStatus.Leave;
        private Color colorDrawSlider = Color.FromArgb(0, 192, 0);//中绿
        #endregion

        #region 

        void pInit()
        {
            fPageDocRatio = _PageSize / _DocSize;
            fCapWidth = _IsRound ? _BarSize : 0f;
            fCapHalfWidth = fCapWidth / 2f;
            if (_Orientation == OrientationScrollBar.Vertical)
            {
                L_SliderLength = fPageDocRatio * (Height - fCapWidth);
                if (L_SliderLength < L_SliderMiniSize) L_SliderLength = L_SliderMiniSize;
                L_SliderMoveRange = Height - fCapWidth - L_SliderLength;
            }
            else
            {
                L_SliderLength = fPageDocRatio * (Width - fCapWidth);
                if (L_SliderLength < L_SliderMiniSize) L_SliderLength = L_SliderMiniSize;
                L_SliderMoveRange = Width - fCapWidth - L_SliderLength;
            }
            fShowScrollRatio = L_SliderMoveRange == 0 ? 0 : (_DocSize - _PageSize) / L_SliderMoveRange;
        }

        void pShowPositionToPosition()
        {
            pInit();
            L_Position = fShowScrollRatio == 0 ? 0 : _ShowPosition / fShowScrollRatio;
        }
        void pChangeSliderLocation()
        {
            float fa = Height - fCapWidth;
            float fb = L_Position + L_SliderLength;
            if (fb > fa)
            {
                L_Position -= fb - fa;
                _ShowPosition = L_Position * fShowScrollRatio;
            }
        }

        int pCheckPointInSlider(PointF point)
        {
            if (_DocSize > _PageSize)
            {
                float fMin = L_Position;
                float fMax = L_Position + L_SliderLength;
                float fPoint, fEnd;
                if (_Orientation == OrientationScrollBar.Vertical)
                {
                    fPoint = point.Y;
                    fEnd = Height;
                }
                else
                {
                    fPoint = point.X;
                    fEnd = Width;
                }
                if (fPoint > 0 && fPoint < fMin) return 0;
                else if (fPoint > fMin && fPoint < fMax) return 1;
                else if (fPoint > fMax && fPoint < fEnd) return 2;
                else return -1;
            }
            else
            {
                return -1;
            }
        }

        #endregion


        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            int iHeight = _BarSize;
            if (_Orientation == OrientationScrollBar.Horizontal)
            {
                base.SetBoundsCore(x, y, width, iHeight, specified);
            }
            else
            {
                base.SetBoundsCore(x, y, iHeight, height, specified);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            pInit();
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            Pen penBar = new Pen(L_BarColor, _BarSize);
            Pen penSlider = new Pen(colorDrawSlider, _BarSize);

            if (_IsRound)
            {
                penBar.StartCap = LineCap.Round;
                penBar.EndCap = LineCap.Round;
                penSlider.StartCap = LineCap.Round;
                penSlider.EndCap = LineCap.Round;
            }

            if (_Orientation == OrientationScrollBar.Horizontal)
            {
                e.Graphics.DrawLine(penBar, fCapHalfWidth, Height / 2f, Width - fCapHalfWidth, Height / 2f);
                e.Graphics.DrawLine(penSlider, L_Position + fCapHalfWidth, Height / 2f, L_Position + L_SliderLength + fCapHalfWidth, Height / 2f);
            }
            else
            {
                e.Graphics.DrawLine(penBar, Width / 2f, fCapHalfWidth, Width / 2f, Height - fCapHalfWidth);
                e.Graphics.DrawLine(penSlider, Width / 2f, L_Position + fCapHalfWidth, Width / 2f, L_Position + L_SliderLength + fCapHalfWidth);
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Select();
            int i = pCheckPointInSlider(e.Location);
            if (i != -1)
            {
                if (i == 0)
                {
                    //点在了滑块上方空白位置
                    //‘上一页’，其实是减去_Page的量
                    _ShowPosition -= _PageSize;
                    if (_ShowPosition < 0) _ShowPosition = 0;
                    pShowPositionToPosition();
                    Invalidate();
                    LScrolled?.Invoke(this, new LScrollEventArgs(L_Position, L_ShowPosition));
                }
                else if (i == 1)
                {
                    //点在了滑块上
                    //计算Above、Below
                    mouseStatus = MouseStatus.Down;
                    if (_Orientation == OrientationScrollBar.Vertical)
                    {
                        fAbove = e.Location.Y - L_Position;
                        fBelow = L_Position + L_SliderLength - e.Location.Y;
                    }
                    else
                    {
                        fAbove = e.Location.X - L_Position;
                        fBelow = L_Position + L_SliderLength - e.Location.X;
                    }
                }
                else
                {
                    //点在了滑块下方空白位置
                    //‘下一页’，加上_Page的量
                    _ShowPosition += _PageSize;
                    pShowPositionToPosition();
                    if (L_Position > L_SliderMoveRange)
                    {
                        L_Position = L_SliderMoveRange;
                        _ShowPosition = L_Position * fShowScrollRatio;
                    }
                    Invalidate();
                    LScrolled?.Invoke(this, new LScrollEventArgs(L_Position, L_ShowPosition));
                }

            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mouseStatus == MouseStatus.Down)
            {
                //证明鼠标点在了滑块上
                if (_Orientation == OrientationScrollBar.Vertical)
                {
                    L_Position = e.Location.Y - fAbove;

                }
                else
                {
                    L_Position = e.Location.X - fAbove;
                }

                if (L_Position < 0) L_Position = 0;
                if (L_Position > L_SliderMoveRange) L_Position = L_SliderMoveRange;

                _ShowPosition = L_Position * fShowScrollRatio;

                Invalidate();
                LScrolled?.Invoke(this, new LScrollEventArgs(L_Position, L_ShowPosition));
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            mouseStatus = MouseStatus.Up;
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            mouseStatus = MouseStatus.Enter;
            colorDrawSlider = L_SliderMouseInColor;
            Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mouseStatus = MouseStatus.Leave;
            colorDrawSlider = L_SliderColor;
            Invalidate();
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (_DocSize > _PageSize)
            {
                bool bToDown = e.Delta > 0 ? false : true;
                float fZ = Convert.ToSingle(Math.Abs(e.Delta)) / 120f;
                if (bToDown)
                {
                    _ShowPosition += L_ScrollInterval;
                    pShowPositionToPosition();
                    if (L_Position > L_SliderMoveRange)
                    {
                        L_Position = L_SliderMoveRange;
                        _ShowPosition = L_Position * fShowScrollRatio;
                    }
                    Invalidate();
                    LScrolled?.Invoke(this, new LScrollEventArgs(L_Position, L_ShowPosition));
                }
                else
                {
                    _ShowPosition -= L_ScrollInterval;
                    if (_ShowPosition < 0) _ShowPosition = 0;
                    pShowPositionToPosition();
                    Invalidate();
                    LScrolled?.Invoke(this, new LScrollEventArgs(L_Position, L_ShowPosition));

                }
            }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (_DocSize > _PageSize)
            {
                if (_Orientation == OrientationScrollBar.Vertical)
                {
                    if (keyData == Keys.Down || keyData == Keys.Up)
                    {
                        if (keyData == Keys.Down)
                        {
                            _ShowPosition += L_ScrollInterval;
                            pShowPositionToPosition();
                            if (L_Position > L_SliderMoveRange)
                            {
                                L_Position = L_SliderMoveRange;
                                _ShowPosition = L_Position * fShowScrollRatio;
                            }
                            Invalidate();
                            LScrolled?.Invoke(this, new LScrollEventArgs(L_Position, L_ShowPosition));
                        }
                        else
                        {
                            _ShowPosition -= L_ScrollInterval;
                            if (_ShowPosition < 0) _ShowPosition = 0;
                            pShowPositionToPosition();
                            Invalidate();
                            LScrolled?.Invoke(this, new LScrollEventArgs(L_Position, L_ShowPosition));

                        }
                    }
                }
                else
                {
                    if (keyData == Keys.Right || keyData == Keys.Left)
                    {
                        if (keyData == Keys.Right)
                        {
                            _ShowPosition += L_ScrollInterval;
                            pShowPositionToPosition();
                            if (L_Position > L_SliderMoveRange)
                            {
                                L_Position = L_SliderMoveRange;
                                _ShowPosition = L_Position * fShowScrollRatio;
                            }
                            Invalidate();
                            LScrolled?.Invoke(this, new LScrollEventArgs(L_Position, L_ShowPosition));
                        }
                        else
                        {
                            _ShowPosition -= L_ScrollInterval;
                            if (_ShowPosition < 0) _ShowPosition = 0;
                            pShowPositionToPosition();
                            Invalidate();
                            LScrolled?.Invoke(this, new LScrollEventArgs(L_Position, L_ShowPosition));

                        }
                    }
                }
            }

            return base.ProcessDialogKey(keyData);
        }

    }
}
