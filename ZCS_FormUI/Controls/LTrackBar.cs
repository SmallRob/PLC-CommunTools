using ZCS_FormUI.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZCS_FormUI.Controls
{
    [DefaultEvent("LValueChanged")]
    public class LTrackBar : Control
    {
        public LTrackBar()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            CreateControl();
        }

        #region 属性

        private Color _BarColor = Color.FromArgb(128, 255, 128);//浅绿
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
                Invalidate();
            }
        }

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

        private int _Minimum = 0;
        /// <summary>
        /// 最小值<para>范围：大于等于0</para>
        /// </summary>
        [Category("DemoUI"), Description("最小值<para>范围：大于等于0</para>")]
        public int L_Minimum
        {
            get { return _Minimum; }
            set
            {
                _Minimum = value;
                if (_Minimum >= _Maximum) _Minimum = _Maximum - 1;
                if (_Minimum < 0) _Minimum = 0;
                Invalidate();
            }
        }

        private int _Maximum = 100;
        /// <summary>
        /// 最大值
        /// </summary>
        [Category("DemoUI"), Description("最大值")]
        public int L_Maximum
        {
            get { return _Maximum; }
            set
            {
                _Maximum = value;
                if (_Maximum <= _Minimum) _Maximum = _Minimum + 1;
                Invalidate();
            }
        }

        private int _Value = 0;
        /// <summary>
        /// 当前值
        /// </summary>
        [Category("DemoUI"), Description("当前值")]
        public int L_Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                if (_Value < _Minimum) _Value = _Minimum;
                if (_Value > _Maximum) _Value = _Maximum;
                Invalidate();
                LValueChanged?.Invoke(this, new LEventArgs(_Value));
            }
        }

        private Render.ScrollOrientation _ScrollOrientation = Render.ScrollOrientation.Horizontal_LR;
        /// <summary>
        /// 方向<para>默认：水平，从左到右</para>
        /// </summary>
        [Category("DemoUI"), Description("方向\r\n默认：水平，从左到右")]
        public Render.ScrollOrientation L_ScrollOrientation
        {
            get { return _ScrollOrientation; }
            set
            {
                Render.ScrollOrientation old = _ScrollOrientation;
                _ScrollOrientation = value;
                if ((old == Render.ScrollOrientation.Horizontal_LR || old == Render.ScrollOrientation.Horizontal_RL)
                 && (_ScrollOrientation == Render.ScrollOrientation.Vertical_BT || _ScrollOrientation == Render.ScrollOrientation.Vertical_TB))
                {
                    Size = new Size(Size.Height, Size.Width);
                }

                if ((_ScrollOrientation == Render.ScrollOrientation.Horizontal_LR || _ScrollOrientation == Render.ScrollOrientation.Horizontal_RL) && (old == Render.ScrollOrientation.Vertical_BT || old == Render.ScrollOrientation.Vertical_TB))
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
        [Category("DemoUI"), Description(" 滑条高度（水平）/宽度（垂直）")]
        public int L_BarSize
        {
            get { return _BarSize; }
            set
            {
                _BarSize = value;
                if (_BarSize < 1) _BarSize = 1;
                if (_ScrollOrientation == Render.ScrollOrientation.Horizontal_LR || _ScrollOrientation == Render.ScrollOrientation.Horizontal_RL)
                {
                    Size = new Size(Width, _BarSize);
                }
                else
                {
                    Size = new Size(_BarSize, Height);
                }
            }
        }

        #endregion

        #region 
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void LValueChangedEventHandler(object sender, LEventArgs e);
        /// <summary>
        /// 值发生改变时引发的事件
        /// </summary>
        public event LValueChangedEventHandler LValueChanged;
        #endregion

        #region 
        private MouseStatus mouseStatus = MouseStatus.Leave;
        private PointF mousePoint = Point.Empty;
        #endregion

        #region
        private void pValueToPoint()
        {
            float fCapHalfWidth = 0;
            float fCapWidth = 0;
            if (_IsRound)
            {
                fCapWidth = _BarSize;
                fCapHalfWidth = _BarSize / 2.0f;
            }

            float fRatio = Convert.ToSingle(_Value - _Minimum) / (_Maximum - _Minimum);
            if (_ScrollOrientation == Render.ScrollOrientation.Horizontal_LR)
            {
                float fPointValue = fRatio * (Width - fCapWidth) + fCapHalfWidth;
                mousePoint = new PointF(fPointValue, fCapHalfWidth);
            }
            else if (_ScrollOrientation == Render.ScrollOrientation.Horizontal_RL)
            {
                float fPointValue = Width - fCapHalfWidth - fRatio * (Width - fCapWidth);
                mousePoint = new PointF(fPointValue, fCapHalfWidth);
            }
            else if (_ScrollOrientation == Render.ScrollOrientation.Vertical_TB)
            {
                float fPointValue = fRatio * (Height - fCapWidth) + fCapHalfWidth;
                mousePoint = new PointF(fCapHalfWidth, fPointValue);
            }
            else
            {
                float fPointValue = Height - fCapHalfWidth - fRatio * (Height - fCapWidth);
                mousePoint = new PointF(fCapHalfWidth, fPointValue);
            }

        }

        private void pPointToValue()
        {
            float fCapHalfWidth = 0;
            float fCapWidth = 0;
            if (_IsRound)
            {
                fCapWidth = _BarSize;
                fCapHalfWidth = _BarSize / 2.0f;
            }

            if (_ScrollOrientation == Render.ScrollOrientation.Horizontal_LR)
            {
                float fRatio = Convert.ToSingle(mousePoint.X - fCapHalfWidth) / (Width - fCapWidth);
                _Value = Convert.ToInt32(fRatio * (_Maximum - _Minimum) + _Minimum);
            }
            else if (_ScrollOrientation == Render.ScrollOrientation.Horizontal_RL)
            {
                float fRatio = Convert.ToSingle(Width - mousePoint.X - fCapHalfWidth) / (Width - fCapWidth);
                _Value = Convert.ToInt32(fRatio * (_Maximum - _Minimum) + _Minimum);
            }
            else if (_ScrollOrientation == Render.ScrollOrientation.Vertical_TB)
            {
                float fRatio = Convert.ToSingle(mousePoint.Y - fCapHalfWidth) / (Height - fCapWidth);
                _Value = Convert.ToInt32(fRatio * (_Maximum - _Minimum) + _Minimum);
            }
            else
            {
                float fRatio = Convert.ToSingle(Height - mousePoint.Y - fCapHalfWidth) / (Height - fCapWidth);
                _Value = Convert.ToInt32(fRatio * (_Maximum - _Minimum) + _Minimum);
            }
            if (_Value < _Minimum) _Value = _Minimum;
            if (_Value > _Maximum) _Value = _Maximum;
            LValueChanged?.Invoke(this, new LEventArgs(_Value));

        }

        #endregion

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            int iHeight = _BarSize;
            if (_ScrollOrientation == Render.ScrollOrientation.Horizontal_LR || _ScrollOrientation == Render.ScrollOrientation.Horizontal_RL)
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
            pValueToPoint();
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            Pen penBarBack = new Pen(_BarColor, _BarSize);
            Pen penBarFore = new Pen(_SliderColor, _BarSize);

            float fCapHalfWidth = 0;
            float fCapWidth = 0;
            if (_IsRound)
            {
                fCapWidth = _BarSize;
                fCapHalfWidth = _BarSize / 2.0f;
                penBarBack.StartCap = LineCap.Round;
                penBarBack.EndCap = LineCap.Round;

                penBarFore.StartCap = LineCap.Round;
                penBarFore.EndCap = LineCap.Round;
            }

            float fPointValue = 0;
            if (_ScrollOrientation == Render.ScrollOrientation.Horizontal_LR || _ScrollOrientation == Render.ScrollOrientation.Horizontal_RL)
            {
                e.Graphics.DrawLine(penBarBack, fCapHalfWidth, Height / 2f, Width - fCapHalfWidth, Height / 2f);

                fPointValue = mousePoint.X;
                if (fPointValue < fCapHalfWidth) fPointValue = fCapHalfWidth;
                if (fPointValue > Width - fCapHalfWidth) fPointValue = Width - fCapHalfWidth;
            }
            else
            {
                e.Graphics.DrawLine(penBarBack, Width / 2f, fCapHalfWidth, Width / 2f, Height - fCapHalfWidth);

                fPointValue = mousePoint.Y;
                if (fPointValue < fCapHalfWidth) fPointValue = fCapHalfWidth;
                if (fPointValue > Height - fCapHalfWidth) fPointValue = Height - fCapHalfWidth;
            }


            if (_ScrollOrientation == Render.ScrollOrientation.Horizontal_LR)
            {
                e.Graphics.DrawLine(penBarFore, fCapHalfWidth, Height / 2f, fPointValue, Height / 2f);
            }
            else if (_ScrollOrientation == Render.ScrollOrientation.Horizontal_RL)
            {
                e.Graphics.DrawLine(penBarFore, fPointValue, Height / 2f, Width - fCapHalfWidth, Height / 2f);
            }
            else if (_ScrollOrientation == Render.ScrollOrientation.Vertical_TB)
            {
                e.Graphics.DrawLine(penBarFore, Width / 2f, fCapHalfWidth, Width / 2f, fPointValue);
            }
            else
            {
                e.Graphics.DrawLine(penBarFore, Width / 2f, fPointValue, Width / 2f, Height - fCapHalfWidth);
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mouseStatus = MouseStatus.Down;
            mousePoint = e.Location;
            pPointToValue();
            Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mouseStatus == MouseStatus.Down)
            {
                mousePoint = e.Location;
                pPointToValue();
                Invalidate();
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
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            mouseStatus = MouseStatus.Leave;
        }
    }
}
