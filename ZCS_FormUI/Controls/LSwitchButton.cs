using ZCS_FormUI.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZCS_FormUI.Render;

namespace ZCS_FormUI.Controls
{
    /// <summary>
    /// 开关按钮
    /// </summary>
    [DefaultEvent("LSwitched")]
    public class LSwitchButton:Control
    {
        /// <summary>
        /// 开关按钮
        /// </summary>
        public LSwitchButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            CreateControl();

            tTimer = new System.Windows.Forms.Timer();
            tTimer.Interval = 10;
            tTimer.Tick += TTimer_Tick;

        }

        #region Event
        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void LSwitchedEventHandler(object sender, LEventArgs e);
        /// <summary>
        /// 开关状态发生改变时引发的事件
        /// </summary>
        public event LSwitchedEventHandler LSwitched;
        #endregion

        #region Property

        private Color _CloseColor = Color.FromArgb(224, 224, 224);//浅灰
        /// <summary>
        /// “关”时背景色<para>默认：浅灰</para>
        /// </summary>
        [Category("DemoUI"), Description("“关”时背景色\r\n默认：浅灰")]
        public Color L_CloseColor
        {
            get { return _CloseColor; }
            set
            {
                _CloseColor = value;
                Invalidate();
            }
        }

        private Color _OpenColor = Color.FromArgb(128, 255, 128);//浅绿
        /// <summary>
        /// “开”时背景色<para>默认：浅绿</para>
        /// </summary>
        [Category("DemoUI"), Description("“开”时背景色\r\n默认：浅绿")]
        public Color L_OpenColor
        {
            get { return _OpenColor; }
            set
            {
                _OpenColor = value;
                Invalidate();
            }
        }

        private Color _DotColor = Color.White;
        /// <summary>
        /// 圆点颜色<para>默认：白色</para>
        /// </summary>
        [Category("DemoUI"), Description("圆点颜色\r\n默认：白色")]
        public Color L_DotColor
        {
            get { return _DotColor; }
            set
            {
                _DotColor = value;
                Invalidate();
            }
        }

        private int _DotHight = 20;
        /// <summary>
        /// 圆点高度（直径）<para>默认：20</para>
        /// </summary>
        [Category("DemoUI"), Description("圆点高度（直径）\r\n默认：20")]
        public int L_DotHight
        {
            get { return _DotHight; }
            set
            {
                _DotHight = value < 0 ? 0 : value;
                Size =new Size(Width,Math.Max(_DotHight, _BarHight));
                Invalidate();
            }
        }

        private int _BarHight = 24;
        /// <summary>
        /// 背景条高度<para>默认：24</para>
        /// </summary>
        [Category("DemoUI"), Description("背景条高度\r\n默认：24")]
        public int L_BarHight
        {
            get { return _BarHight; }
            set
            {
                _BarHight = value < 0 ? 0 : value;
                Size = new Size(Width, Math.Max(_DotHight, _BarHight));
                Invalidate();
            }
        }

        private bool _IsOpen = false;
        /// <summary>
        /// 开关状态是否是“开”<para>默认：false</para>
        /// </summary>
        [Category("DemoUI"), Description("开关状态是否是“开”\r\n默认：false")]
        public bool L_IsOpen
        {
            get { return _IsOpen; }
            set
            {
                _IsOpen = value;
                Invalidate();
            }
        }

        private int _Margin = 2;
        /// <summary>
        /// 圆点距左右两侧距离<para>默认：2</para>
        /// </summary>
        [Category("DemoUI"), Description("圆点距左右两侧距离\r\n默认：2")]
        public int L_Margin
        {
            get { return _Margin; }
            set
            {
                _Margin = value < 0 ? 0 : value; ;
                Invalidate();
            }
        }

        /// <summary>
        /// 动画效果<para>默认：Quartic</para>
        /// </summary>
        [Category("DemoUI"), Description("动画效果\r\n默认：Quartic")]
        public ShowColor animateType { get; set; } = ShowColor.Quartic;

        #endregion

        #region Private
        Timer tTimer;
        float fNowLocationBar, fNowLocationDot;
        /// <summary>
        /// 动画效果时长，毫秒
        /// </summary>
        int iDuration = 200;
        int iNow;
        bool bInAnimate = false;
        float fRangeBar,fRangeDot;
        #endregion

        private void TTimer_Tick(object sender, EventArgs e)
        {
            bool b = (_IsOpen?(iNow>=0):(iNow<=iDuration));
            if (b)
            {
                double d = 0;
                switch (animateType)
                {
                    case ShowColor.Back:
                        d = AnimateBase.Back(Convert.ToDouble(iNow) / iDuration, _IsOpen ? LEaseMode.EaseIn : LEaseMode.EaseOut);
                        break;
                    case ShowColor.Bounce:
                        d = AnimateBase.Bounce(Convert.ToDouble(iNow) / iDuration, _IsOpen ? LEaseMode.EaseIn : LEaseMode.EaseOut);
                        break;
                    case ShowColor.Circle:
                        d = AnimateBase.Circle(Convert.ToDouble(iNow) / iDuration, _IsOpen ? LEaseMode.EaseIn : LEaseMode.EaseOut);
                        break;
                    case ShowColor.Cubic:
                        d = AnimateBase.Cubic(Convert.ToDouble(iNow) / iDuration, _IsOpen ? LEaseMode.EaseIn : LEaseMode.EaseOut);
                        break;
                    case ShowColor.Elastic:
                        d = AnimateBase.Elastic(Convert.ToDouble(iNow) / iDuration, _IsOpen ? LEaseMode.EaseIn : LEaseMode.EaseOut);
                        break;
                    case ShowColor.Exponential:
                        d = AnimateBase.Exponential(Convert.ToDouble(iNow) / iDuration, _IsOpen ? LEaseMode.EaseIn : LEaseMode.EaseOut);
                        break;
                    case ShowColor.Quadratic:
                        d = AnimateBase.Quadratic(Convert.ToDouble(iNow) / iDuration, _IsOpen ? LEaseMode.EaseIn : LEaseMode.EaseOut);
                        break;
                    case ShowColor.Quartic:
                        d = AnimateBase.Quartic(Convert.ToDouble(iNow) / iDuration, _IsOpen ? LEaseMode.EaseIn : LEaseMode.EaseOut);
                        break;
                    case ShowColor.Quintic:
                        d = AnimateBase.Quintic(Convert.ToDouble(iNow) / iDuration, _IsOpen ? LEaseMode.EaseIn : LEaseMode.EaseOut);
                        break;
                    case ShowColor.Sine:
                        d = AnimateBase.Sine(Convert.ToDouble(iNow) / iDuration, _IsOpen ? LEaseMode.EaseIn : LEaseMode.EaseOut);
                        break;
                }
                
                fNowLocationBar = Convert.ToSingle(d * fRangeBar);
                fNowLocationDot = Convert.ToSingle(d * fRangeDot)+_Margin;
                iNow = _IsOpen ? (iNow-tTimer.Interval) : (iNow+tTimer.Interval);
                Invalidate();
            }
            else
            {
                tTimer.Stop();
                bInAnimate = false;
                LSwitched?.Invoke(this, new LEventArgs(_IsOpen));
            }
        }
        
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            //为达到较好的视觉效果，宽度至少要是高度的两倍
            int iHeight = Math.Max(_DotHight,_BarHight);
            int iWidth = Math.Max(iHeight * 2, width);
            iHeight++;
            fRangeBar = iWidth - _BarHight;
            fRangeDot = iWidth - _DotHight- _Margin * 2;
            base.SetBoundsCore(x, y, iWidth, iHeight, specified);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            Pen penBarBack = new Pen(_CloseColor, _BarHight);
            Pen penBarFore = new Pen(_OpenColor, _BarHight);
            penBarBack.StartCap = LineCap.Round;
            penBarBack.EndCap = LineCap.Round;
            penBarFore.StartCap = LineCap.Round;
            penBarFore.EndCap = LineCap.Round;

            float fCapHalfWidth = _BarHight / 2.0f;
            float fLeftTop = Convert.ToSingle(Height - _DotHight) / 2.0f;

            if (!bInAnimate)
            {
                fNowLocationBar = _IsOpen ? 0: fRangeBar;
                fNowLocationDot = _IsOpen ? _Margin : (fRangeDot + _Margin);
            }

            //画背景
            e.Graphics.DrawLine(penBarBack, fCapHalfWidth, Height / 2f, Width - fCapHalfWidth, Height / 2f);
            //画前景
            e.Graphics.DrawLine(penBarFore, fCapHalfWidth, Height / 2f, Width - fCapHalfWidth - fNowLocationBar, Height / 2f);
            //画圆点
            e.Graphics.FillEllipse(new SolidBrush(_DotColor),Width-_DotHight- fNowLocationDot, fLeftTop, _DotHight, _DotHight);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!bInAnimate)
            {
                bInAnimate = true;
                _IsOpen = !_IsOpen;
                iNow = _IsOpen ? iDuration : 0;
                tTimer.Start();
            }
        }
    }
}
