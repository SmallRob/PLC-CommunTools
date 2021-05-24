/***
 * create by  hexiuqi
 * description：用于等待操作或者使用弹出提示时所调用的窗体
                用于对操作者的友好提示,通用提示消息窗口
 ***/
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ZCS_FormUI.Forms
{
    public partial class TipMessage : Form
    {
        #region 全局属性控制

        /// <summary>
        /// 提示文本信息
        /// </summary> 
        public string strInfo { get; set; }
        public string strMore { get; private set; }

        /// <summary>
        /// 等待一次的时间长度(单位：毫秒)
        /// </summary>
        public int TimeSpan { get; set; }

        /// <summary>
        /// 最大尝试等待次数
        /// </summary>
        public int MaxWaitCount { get; set; }

        /// <summary>
        /// 最小尝试等待次数
        /// </summary>
        public int MinWaitCount { get; set; }

        private bool isWait = false;         //是否继续等待的标识
        private int waitCount = 0;           //尝试等待次数
        private Point screenMouse;           //鼠标在屏幕中的位置
        private Size size;

        /// <summary>
        /// 消息类型
        /// </summary>
        public enum showType
        {
            /// <summary>
            /// 等待窗体
            /// </summary>
            Waiting,
            /// <summary>
            /// 成功提示
            /// </summary>
            Succeed,
            /// <summary>
            /// 普通提示
            /// </summary>
            InfoMsg,
            /// <summary>
            /// 重要提示
            /// </summary>
            Warning,
            /// <summary>
            /// 错误信息
            /// </summary>
            Error
        }

        /// <summary>
        /// 消息显示类型
        /// </summary>
        public enum tipType
        {
            /// <summary>
            /// 普通的消息提示，显示位置在屏幕中央
            /// </summary>
            Message,
            /// <summary>
            /// 下角提示，显示位置在屏幕右下角
            /// </summary>
            BottomTip
        }

        //等待状态下的窗口委托
        //public Action<Waiting> loadingAction;
        public bool connectedBreak = false;  //网络连接是否已经中断
        public bool exceptionFlag = false;   //退出标识（异常情况下或者在需要取消提示框的时候使用）

        /// <summary>
        /// 调用类型，默认为等待窗体
        /// </summary>
        private showType showtype = showType.Waiting;

        /// <summary>
        /// 消息显示类型，默认为普通消息显示
        /// </summary>
        private tipType infoType = tipType.Message;

        #region 消息提示框使用范例
        /********
         * 方式一：调用等待窗口：
         * Thread TD = new Thread(new ParameterizedThreadStart(showwaitfrm));
         * TD.Start("正在获取数据，请稍候"); //建议10个汉字以内（包含中文符号）
         * Application.DoEvents();
         * （一段逻辑处理之后...）
         * waitfrm.exceptionFlag = true;     //退出提示窗口
         * this.Activate();                  //激活当前窗体
         *
         * private void showwaitfrm(object txtStr)
         * {
         *   waitfrm = new Waiting(txtStr.ToString());           
         *   waitfrm.ShowDialog();
         * }
         *
         * 方式二：调用提示窗口：
         * Waiting waitfrm = new Waiting(showType.InfoMsg,txtStr.ToString());           
         * waitfrm.ShowDialog();
         * waitfrm.Dispose();
         ********/
        #endregion

        public TipMessage()
        {
            InitializeComponent();
            this.strMore = ".";
            this.lblTxt.Text = "";
            this.DoubleBuffered = true;                          //设置本窗体双缓冲，避免加载闪烁
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);  // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true);          // 双缓冲 
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 全局方法，获取调用窗体传过来的提示信息
        /// </summary>
        public TipMessage(string txtStr)
            : this()
        {
            if (!String.IsNullOrEmpty(txtStr))
            {
                this.strInfo = txtStr;
            }
            else this.strInfo = "操作正在进行，请等待";
        }

        /// <summary>
        /// 带类型的提示信息
        /// </summary>
        /// <param name="showtype">消息类型</param>
        /// <param name="txtStr">文本信息</param>
        public TipMessage(showType showtype, string txtStr)
            : this()
        {
            this.showtype = showtype;
            this.strInfo = txtStr;
        }

        /// <summary>
        /// 带消息显示类型和消息类型的提示信息
        /// </summary>
        /// <param name="showtype">消息类型</param>
        /// <param name="infoType">消息显示类型</param>
        /// <param name="txtStr">消息文本</param>
        public TipMessage(showType showtype, tipType infoType, string txtStr)
            : this()
        {
            this.showtype = showtype;
            this.infoType = infoType;
            this.strInfo = txtStr;
        }

        /// <summary>
        /// API接口 任务栏闪烁状态
        /// </summary>
        [DllImportAttribute("user32.dll")]
        public static extern bool FlashWindow(IntPtr handle, bool bInvert);

        /// <summary>
        /// API接口 设置窗体打开效果
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="dwTime"></param>
        /// <param name="dwFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        private const int AW_HOR_POSITIVE = 0x0001;//从左到右打开窗口
        private const int AW_HOR_NEGATIVE = 0x0002;//从右到左打开窗口
        private const int AW_VER_POSITIVE = 0x0004;//从上到下打开窗口
        private const int AW_VER_NEGATIVE = 0x0008;//从下到上打开窗口
        private const int AW_CENTER = 0x0010;   //从中央打开
        private const int AW_HIDE = 0x10000;    //隐藏窗体
        private const int AW_ACTIVATE = 0x20000;//显示窗体
        private const int AW_SLIDE = 0x40000;
        private const int AW_BLEND = 0x80000;   //淡入淡出效果

        /// <summary>
        /// API接口 获取网络状态
        /// </summary>
        [DllImport("wininet")]
        private extern static bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int HWMSg, int wParam, int IParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        #endregion

        /// <summary>
        /// 窗体加载控制
        /// </summary>
        private void Waiting_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = this.BackColor;
            int strLength = strInfo.Length;

            #region 根据枚举类型调整消息显示
            switch (showtype)
            {
                case showType.Waiting:
                    Application.UseWaitCursor = true;
                    this.lblTxt.Text = strInfo + strMore;
                    break;
                case showType.Succeed:
                    this.lblTxt.Text = "提示：" + strInfo;
                    strLength = strLength + 3;
                    this.picInfo.Image = this.imgListInfo.Images[0];
                    break;
                case showType.InfoMsg:
                    this.lblTxt.Text = strInfo;
                    this.picInfo.Image = this.imgListInfo.Images[1];
                    break;
                case showType.Warning:
                    this.lblTxt.Text = strInfo;
                    this.picInfo.Image = this.imgListInfo.Images[3];
                    break;
                case showType.Error:
                    this.lblTxt.Text = "出错提示：" + strInfo;
                    strLength = strLength + 5;
                    this.picInfo.Image = this.imgListInfo.Images[2];
                    break;
                default:
                    //Application.UseWaitCursor = false;
                    break;
            }

            switch (infoType)
            {
                case tipType.Message:
                    break;
                case tipType.BottomTip:
                    this.picInfo.Image = this.imgListInfo.Images[3];
                    break;
                default:
                    break;
            }

            //设置最大最小等待次数
            MaxWaitCount = 99999;
            MinWaitCount = 39;

            #endregion

            #region 根据字符长度计算窗体大小
            if (strLength >= 12)
            {
                if (strLength > 33)
                {
                    //字符长度不得超过33个字符
                    strInfo = lblTxt.Text.Substring(0, 30) + "...";
                    strLength = 33;
                }
                this.Size = new Size(263 + (strLength - 11) * 16, 56); //225
                this.groupBoxEx1.Size = new Size(Size.Width - 2, 60);
            }
            #endregion

            #region 根据显示类型调整窗体位置
            Rectangle rect = new Rectangle();
            Screen CurrentScreen = Screen.FromControl(this);
            //Screen.FromPoint(new Point(Cursor.Position.X, Cursor.Position.Y));           

            rect = CurrentScreen.WorkingArea;
            //Screen.GetWorkingArea(this);                              //获得当前屏幕的大小

            if (infoType == tipType.Message)
            {
                this.Location = new Point((rect.Width - this.Size.Width) / 2, (rect.Height - this.Size.Height) / 2);
                AnimateWindow(this.Handle, 40, AW_ACTIVATE | AW_BLEND);     //设置窗体淡入效果
            }
            else
            {
                Point p = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, Screen.PrimaryScreen.WorkingArea.Height - this.Height);
                this.PointToScreen(p);
                this.Location = p;
                AnimateWindow(this.Handle, 20, AW_ACTIVATE | AW_VER_NEGATIVE);
            }

            #endregion

            screenMouse = PointToScreen(Cursor.Position);               //获取鼠标屏幕坐标
            size = Cursor.Size;
            if (showtype == showType.Waiting)
            {
                //Cursor.Clip = new Rectangle(this.Location, this.Size);     //设置鼠标活动范围为当前窗体
                Cursor.Hide();                                               //隐藏鼠标光标(不允许用户再操作，避免出现并发)
                //loadingAction(this);
            }
            this.timer.Start();
            Application.DoEvents();
        }

        /// <summary>
        /// 启用计时器，更改提示文本显示
        /// exceptionFlag等于true 则关闭窗口
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            //如果异常标识为true，表示存在异常或者调用结束 则关闭当前窗口
            if (exceptionFlag || connectedBreak)
            {
                //if (showtype == showType.Waiting)
                //{
                //  //Cursor.Position = screenMouse;
                //    Cursor.Clip = Rectangle.Empty;   //new Rectangle(screenMouse, size);
                //}
                Cursor.Show();
                Application.UseWaitCursor = false;

                if (!IsDisposed)
                {
                    if (infoType == tipType.Message)
                        AnimateWindow(this.Handle, 40, AW_HIDE | AW_BLEND);
                    else
                        AnimateWindow(this.Handle, 20, AW_HIDE | AW_VER_POSITIVE);

                    Cursor = Cursors.Default;
                    this.Close();
                    //GC.Collect();
                    //GC.WaitForPendingFinalizers();
                }
            }
            else
            {
                TimeSpan = 1200;
                this.tmrWait.Interval = TimeSpan;

                #region 控制等待类型的文本切换显示
                if (showtype == showType.Waiting)
                {
                    int i = 0;
                    //如果网络状态异常
                    if (!(InternetGetConnectedState(out i, 0)))
                    {
                        strInfo = "网络状况异常，正重试";
                        connectedBreak = true;
                    }
                    else
                    {
                        if (waitCount == MaxWaitCount - 1)
                        {
                            strInfo = "等待后台操作处理完成";
                            waitCount++;
                        }
                    }

                    //FlashWindow(this.Handle, true);      //任务栏闪烁
                    if (strMore.Length < 3)
                    {
                        strMore = strMore + ".";
                    }
                    else strMore = ".";
                    this.lblTxt.Text = strInfo + strMore;
                }
                #endregion

                this.tmrWait.Start();
            }
        }

        /// <summary>
        /// 依据等待次数设置窗口状态
        /// </summary>
        private void tmrWait_Tick(object sender, EventArgs e)
        {
            if (showtype != showType.Waiting)
            {
                if (infoType == tipType.Message)
                    exceptionFlag = true;
            }
            else
            {
                if (connectedBreak)
                {
                    waitCount = MaxWaitCount;
                }
                if (waitCount > 2)
                {
                    if (waitCount > MaxWaitCount)
                        exceptionFlag = true;
                    //else isWait = false;
                }
                //strInfo = isWait == true ? "正在处理数据，请稍等" : strInfo;
                //isWait = !isWait;
                waitCount++;
            }
        }

        /// <summary>
        /// 等待窗口关闭
        /// </summary>
        public void WaitClose()
        {
            Cursor.Show();
            Application.UseWaitCursor = false;
            Cursor = Cursors.Default;
            AnimateWindow(this.Handle, 40, AW_HIDE | AW_BLEND);
            this.Close();
        }

        /// <summary>
        /// 窗体键盘按下
        /// </summary>
        private void Waiting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                WaitClose();
            }
        }

        /// <summary>
        /// 窗体鼠标按下
        /// </summary>
        private void Waiting_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        /// <summary>
        /// 窗体失去焦点
        /// </summary>
        private void Waiting_Deactivate(object sender, EventArgs e)
        {
            if (showtype == showType.Waiting || infoType == tipType.BottomTip)
            {
                TopMost = false;
                if (waitCount >= MinWaitCount) WaitClose();
            }
        }
    }
}