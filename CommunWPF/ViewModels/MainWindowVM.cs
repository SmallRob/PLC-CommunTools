using Microsoft.Win32;
using CommunWPF.Models;
using CommunWPF.Views;
using System;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace CommunWPF.ViewModels
{
    internal class MainWindowViewModel : MainWindowBase, IDisposable
    {
        #region 字段
        internal SerialPort SerialPortBase = new SerialPort();

        /// <summary>
        /// 保存接收，数据保存路径
        /// </summary>
        private volatile string DataRecvPath = string.Empty;

        /// <summary>
        /// 用于接收区数据超过32MB时，自动清空接收控件中的数据
        /// </summary>
        private volatile Int32 RecvDataDeleteCount = 1;
        #endregion

        public SerialPortModel SerialPortModel
        {
            get; set;
        }
        public SendModel SendModel
        {
            get; set;
        }
        public RecvModel RecvModel
        {
            get; set;
        }
        public TimerModel TimerModel
        {
            get; set;
        }

        public HelpModel HelpModel
        {
            get; set;
        }

        #region 状态栏- 信息描述
        private string _DepictInfo;
        public string DepictInfo
        {
            get
            {
                return _DepictInfo;
            }
            set
            {
                if (_DepictInfo != value)
                {
                    _DepictInfo = value;
                    RaisePropertyChanged(nameof(DepictInfo));
                }
            }
        }
        #endregion

        #region 菜单栏

        #region 文件
        internal void ExitWindow()
        {
            /* 仅当成功在SerialPort对象上调用Open()方法且最近一次没有调用Close()方法时，才返回True */
            if (SerialPortBase.IsOpen)
            {
                CloseSP();
            }
        }
        #endregion

        #region 选项

        #region 字节编码
        internal void ASCIIEnable()
        {
            SerialPortModel.UTF8Enable = false;
            SerialPortModel.UTF16Enable = false;
            SerialPortModel.UTF32Enable = false;

            SerialPortModel.ASCIIEnable = true;

            try
            {
                if (SerialPortModel.ASCIIEnable)
                {
                    SerialPortBase.Encoding = System.Text.Encoding.ASCII;
                }
            }
            catch (ArgumentException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }

        internal void UTF8Enable()
        {
            SerialPortModel.ASCIIEnable = false;
            SerialPortModel.UTF16Enable = false;
            SerialPortModel.UTF32Enable = false;

            SerialPortModel.UTF8Enable = true;

            try
            {
                if (SerialPortModel.UTF8Enable)
                {
                    SerialPortBase.Encoding = System.Text.Encoding.UTF8;
                }
            }
            catch (ArgumentException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }

        internal void UTF16Enable()
        {
            SerialPortModel.ASCIIEnable = false;
            SerialPortModel.UTF8Enable = false;
            SerialPortModel.UTF32Enable = false;

            SerialPortModel.UTF16Enable = true;

            try
            {
                if (SerialPortModel.UTF16Enable)
                {
                    SerialPortBase.Encoding = System.Text.Encoding.Unicode;
                }
            }
            catch (ArgumentException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }

        internal void UTF32Enable()
        {
            SerialPortModel.ASCIIEnable = false;
            SerialPortModel.UTF8Enable = false;
            SerialPortModel.UTF16Enable = false;

            SerialPortModel.UTF32Enable = true;

            try
            {
                if (SerialPortModel.UTF32Enable)
                {
                    SerialPortBase.Encoding = System.Text.Encoding.UTF32;
                }
            }
            catch (ArgumentException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }
        #endregion

        internal void RtsEnable()
        {
            SerialPortModel.RtsEnable = !(SerialPortModel.RtsEnable);

            try
            {
                if (SerialPortModel.RtsEnable)
                {
                    SerialPortBase.RtsEnable = true;
                }
                else
                {
                    SerialPortBase.RtsEnable = false;
                }
            }
            catch (InvalidOperationException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (IOException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }

        internal void DtrEnable()
        {
            SerialPortModel.DtrEnable = !(SerialPortModel.DtrEnable);

            try
            {
                if (SerialPortModel.DtrEnable)
                {
                    SerialPortBase.DtrEnable = true;
                }
                else
                {
                    SerialPortBase.DtrEnable = false;
                }
            }
            catch (InvalidOperationException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (IOException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }

        #region 流控制
        internal void NoneEnable()
        {
            SerialPortModel.XOnXOffEnable = false;
            SerialPortModel.RequestToSendEnable = false;
            SerialPortModel.RequestToSendXOnXOffEnable = false;

            SerialPortModel.NoneEnable = true;

            try
            {
                if (SerialPortModel.NoneEnable)
                {
                    SerialPortBase.Handshake = Handshake.None;
                }
            }
            catch (IOException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }

        internal void RequestToSendEnable()
        {
            SerialPortModel.NoneEnable = false;
            SerialPortModel.XOnXOffEnable = false;
            SerialPortModel.RequestToSendXOnXOffEnable = false;

            SerialPortModel.RequestToSendEnable = true;

            try
            {
                if (SerialPortModel.RequestToSendEnable)
                {
                    SerialPortBase.Handshake = Handshake.RequestToSend;
                }
            }
            catch (IOException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }

        internal void XOnXOffEnable()
        {
            SerialPortModel.NoneEnable = false;
            SerialPortModel.RequestToSendEnable = false;
            SerialPortModel.RequestToSendXOnXOffEnable = false;

            SerialPortModel.XOnXOffEnable = true;

            try
            {
                if (SerialPortModel.XOnXOffEnable)
                {
                    SerialPortBase.Handshake = Handshake.XOnXOff;
                }
            }
            catch (IOException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }

        internal void RequestToSendXOnXOffEnable()
        {
            SerialPortModel.NoneEnable = false;
            SerialPortModel.XOnXOffEnable = false;
            SerialPortModel.RequestToSendEnable = false;

            SerialPortModel.RequestToSendXOnXOffEnable = true;

            try
            {
                if (SerialPortModel.RequestToSendXOnXOffEnable)
                {
                    SerialPortBase.Handshake = Handshake.RequestToSendXOnXOff;
                }
            }
            catch (IOException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }
        #endregion

        internal void TimeStampEnable()
        {
            TimerModel.TimeStampEnable = !(TimerModel.TimeStampEnable);
        }

        #region 发送换行
        internal void NonesEnable()
        {
            SendModel.CrEnable = false;
            SendModel.LfEnable = false;
            SendModel.CrLfEnable = false;

            SendModel.NonesEnable = true;
        }

        internal void CrEnable()
        {
            SendModel.NonesEnable = false;
            SendModel.LfEnable = false;
            SendModel.CrLfEnable = false;

            SendModel.CrEnable = true;
        }

        internal void LfEnable()
        {
            SendModel.NonesEnable = false;
            SendModel.CrEnable = false;
            SendModel.CrLfEnable = false;

            SendModel.LfEnable = true;
        }

        internal void CrLfEnable()
        {
            SendModel.NonesEnable = false;
            SendModel.CrEnable = false;
            SendModel.LfEnable = false;

            SendModel.CrLfEnable = true;
        }
        #endregion

        #endregion

        #region 视图
        internal void ReducedEnable()
        {
            HelpModel.ReducedEnable = !HelpModel.ReducedEnable;

            if (HelpModel.ReducedEnable)
            {
                HelpModel.ViewVisibility = "Collapsed";
            }
            else
            {
                HelpModel.ViewVisibility = "Visible";
            }
        }
        #endregion

        #endregion

        #region 打开/关闭串口
        internal void OpenSP()
        {
            if (SerialPortBase.IsOpen)
            {
                CloseSP();

                return;
            }

            try
            {
                SerialPortBase.PortName = SerialPortModel.Port;
                SerialPortBase.BaudRate = SerialPortModel.BaudRate;
                SerialPortBase.DataBits = SerialPortModel.DataBits;
                SerialPortBase.StopBits = SerialPortModel.StopBits;
                SerialPortBase.Parity = SerialPortModel.Parity;

                SerialPortBase.WriteBufferSize = 1048576;   /* 输出缓冲区的大小为1048576字节 = 1MB */
                SerialPortBase.ReadBufferSize = 2097152;    /* 输入缓冲区的大小为2097152字节 = 2MB */

                /* 字节编码 */
                if (SerialPortModel.ASCIIEnable)
                {
                    SerialPortBase.Encoding = System.Text.Encoding.ASCII;
                }
                else if (SerialPortModel.UTF8Enable)
                {
                    SerialPortBase.Encoding = System.Text.Encoding.UTF8;
                }
                else if (SerialPortModel.UTF16Enable)
                {
                    SerialPortBase.Encoding = System.Text.Encoding.Unicode;
                }
                else if (SerialPortModel.UTF32Enable)
                {
                    SerialPortBase.Encoding = System.Text.Encoding.UTF32;
                }

                /* 发送请求（RTS）信号 */
                if (SerialPortModel.RtsEnable)
                {
                    SerialPortBase.RtsEnable = true;
                }
                else
                {
                    SerialPortBase.RtsEnable = false;
                }

                /* 数据终端就绪（DTR）信号 */
                if (SerialPortModel.DtrEnable)
                {
                    SerialPortBase.DtrEnable = true;
                }
                else
                {
                    SerialPortBase.DtrEnable = false;
                }

                /* 流控制 */
                if (SerialPortModel.NoneEnable)
                {
                    SerialPortBase.Handshake = Handshake.None;
                }
                else if (SerialPortModel.RequestToSendEnable)
                {
                    SerialPortBase.Handshake = Handshake.RequestToSend;
                }
                else if (SerialPortModel.XOnXOffEnable)
                {
                    SerialPortBase.Handshake = Handshake.XOnXOff;
                }
                else if (SerialPortModel.RequestToSendXOnXOffEnable)
                {
                    SerialPortBase.Handshake = Handshake.RequestToSendXOnXOff;
                }

                /* 数据接收事件 */
                SerialPortBase.DataReceived += new SerialDataReceivedEventHandler(SerialPortDataReceived);

                /* 信号状态事件 */
                SerialPortBase.PinChanged += new SerialPinChangedEventHandler(SerialPortPinChanged);

                SerialPortBase.Open();

                if (SerialPortBase.IsOpen)
                {
                    SerialPortModel.Brush = Brushes.GreenYellow;
                    SerialPortModel.OpenClose = string.Format(CultureInfos, "关闭串口");
                    DepictInfo = string.Format(CultureInfos, "成功打开串行端口{0}、波特率{1}、数据位{2}、停止位{3}、校验位{4}",
                        SerialPortBase.PortName,
                        SerialPortBase.BaudRate.ToString(CultureInfos),
                        SerialPortBase.DataBits.ToString(CultureInfos),
                        SerialPortBase.StopBits.ToString(),
                        SerialPortBase.Parity.ToString());

                    SerialPortModel.PortEnable = false;
                    SerialPortModel.BaudRateEnable = false;
                    SerialPortModel.DataBitsEnable = false;
                    SerialPortModel.StopBitsEnable = false;
                    SerialPortModel.ParityEnable = false;

                    if (RecvModel.EnableRecv)
                    {
                        RecvModel.RecvEnable = string.Format(CultureInfos, "允许");
                    }
                    else
                    {
                        RecvModel.RecvEnable = string.Format(CultureInfos, "暂停");
                    }
                }
                else
                {
                    DepictInfo = string.Format(CultureInfos, "串行端口打开失败");
                }
            }
            catch (UnauthorizedAccessException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (ArgumentOutOfRangeException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (ArgumentException e)
            {
                DepictInfo = string.Format(CultureInfos, "串行端口属性{0}为非法参数，请重新输入", e.ParamName);
            }
            catch (IOException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (InvalidOperationException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }

        private void CloseSP()
        {
            try
            {
                /* 
                 * 该方法调用Component.Dispose()方法，同时还会
                 * 调用disposing参数设置为True的受保护的SerialPort.Dispose(Boolean)方法
                 */
                SerialPortBase.Close();   /* 关闭SerialPort对象，并清除接收缓冲区和发送缓冲区 */

                SerialPortModel.Brush = Brushes.Red;
                SerialPortModel.OpenClose = string.Format(CultureInfos, "打开串口");

                DepictInfo = string.Format(CultureInfos, "串行端口关闭成功");

                SerialPortModel.PortEnable = true;
                SerialPortModel.BaudRateEnable = true;
                SerialPortModel.DataBitsEnable = true;
                SerialPortModel.StopBitsEnable = true;
                SerialPortModel.ParityEnable = true;

                RecvModel.RecvAutoSave = string.Format(CultureInfos, "已停止");
            }
            catch (IOException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }
        #endregion

        #region 辅助区
        private bool _SaveRecv;
        public bool SaveRecv
        {
            get
            {
                return _SaveRecv;
            }
            set
            {
                if (_SaveRecv != value)
                {
                    _SaveRecv = value;
                    RaisePropertyChanged(nameof(SaveRecv));
                }

                if (SaveRecv)
                {
                    DepictInfo = string.Format(CultureInfos, "接收数据默认保存在程序基目录，可以点击路径选择操作更换");
                }
                else
                {
                    DepictInfo = string.Format(CultureInfos, "串行端口调试助手");
                }
            }
        }

        private bool _HexSend;
        public bool HexSend
        {
            get
            {
                return _HexSend;
            }
            set
            {
                if (_HexSend != value)
                {
                    _HexSend = value;
                    RaisePropertyChanged(nameof(HexSend));

                    if (HexSend == true)
                    {
                        DepictInfo = string.Format(CultureInfos, "请输入合法十六进制数据，且用空格隔开，比如A0 B1 C2 D3 E4 F5");
                    }
                    else
                    {
                        DepictInfo = string.Format(CultureInfos, "串行端口调试助手");
                    }
                }
            }
        }

        private bool _AutoSend;
        public bool AutoSend
        {
            get
            {
                return _AutoSend;
            }
            set
            {
                if (_AutoSend != value)
                {
                    _AutoSend = value;
                    RaisePropertyChanged(nameof(AutoSend));
                }

                if (AutoSend == true)
                {
                    if (String.IsNullOrWhiteSpace(SendModel.AutoSendNum))
                    {
                        DepictInfo = string.Format(CultureInfos, "请输入正确的发送时间间隔");
                        return;
                    }

                    var _AutoSendNum = Convert.ToInt32(SendModel.AutoSendNum, CultureInfos);

                    if (_AutoSendNum <= 0)
                    {
                        DepictInfo = string.Format(CultureInfos, "请输入正确的发送时间间隔");
                        return;
                    }

                    StartAutoSendTimer(_AutoSendNum);
                }
                else
                {
                    StopAutoSendTimer();
                    DepictInfo = string.Format(CultureInfos, "串行端口调试助手");
                }
            }
        }
        #endregion

        #region 自动发送定时器实现
        private readonly DispatcherTimer AutoSendDispatcherTimer = new DispatcherTimer();

        private void InitAutoSendTimer()
        {
            AutoSendDispatcherTimer.IsEnabled = false;
            AutoSendDispatcherTimer.Tick += AutoSendDispatcherTimer_Tick;
        }

        private async void AutoSendDispatcherTimer_Tick(object sender, EventArgs e)
        {
            await SendAsync().ConfigureAwait(false);
        }

        private void StartAutoSendTimer(int interval)
        {
            AutoSendDispatcherTimer.IsEnabled = true;
            AutoSendDispatcherTimer.Interval = TimeSpan.FromMilliseconds(interval);
            AutoSendDispatcherTimer.Start();
        }

        private void StopAutoSendTimer()
        {
            AutoSendDispatcherTimer.IsEnabled = false;
            AutoSendDispatcherTimer.Stop();
        }
        #endregion

        #region 发送
        internal async Task SendAsync()
        {
            if (!SerialPortBase.IsOpen)
            {
                DepictInfo = string.Format(CultureInfos, "请先打开串行端口");

                return;
            }

            try
            {
                int SendCount = 0;

                if (HexSend)
                {
                    string[] _sendData = SendModel.SendData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    byte[] sendData = new byte[_sendData.Length];

                    foreach (var tmp in _sendData)
                    {
                        sendData[SendCount++] = byte.Parse(tmp, NumberStyles.AllowHexSpecifier, CultureInfos);
                    }

                    await SerialPortBase.BaseStream.WriteAsync(sendData, 0, SendCount).ConfigureAwait(false);

                }
                else
                {
                    SendCount = SerialPortBase.Encoding.GetByteCount(SendModel.SendData);
                    await SerialPortBase.BaseStream.WriteAsync(SerialPortBase.Encoding.GetBytes(SendModel.SendData), 0, SendCount)
                        .ConfigureAwait(false);
                }

                if (SendModel.NonesEnable)
                {
                    SendModel.SendDataCount += SendCount;
                }
                else if (SendModel.CrEnable)
                {
                    await SerialPortBase.BaseStream.WriteAsync(SerialPortBase.Encoding.GetBytes("\r"), 0, 1)
                        .ConfigureAwait(false);
                    SendModel.SendDataCount += (SendCount + 1);
                }
                else if (SendModel.LfEnable)
                {
                    await SerialPortBase.BaseStream.WriteAsync(SerialPortBase.Encoding.GetBytes("\n"), 0, 1)
                        .ConfigureAwait(false);
                    SendModel.SendDataCount += (SendCount + 1);
                }
                else if (SendModel.CrLfEnable)
                {
                    await SerialPortBase.BaseStream.WriteAsync(SerialPortBase.Encoding.GetBytes("\r\n"), 0, 2)
                        .ConfigureAwait(false);
                    SendModel.SendDataCount += (SendCount + 2);
                }
            }
            catch (ArgumentException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (IOException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (OutOfMemoryException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (FormatException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (OverflowException)
            {
                DepictInfo = string.Format(CultureInfos, "请输入合法十六进制数据，且用空格隔开，比如A0 B1 C2 D3 E4 F5");
            }
            catch (IndexOutOfRangeException)
            {
                DepictInfo = string.Format(CultureInfos, "正在试图执行越界访问，请通过菜单栏<帮助>报告问题！");
            }
            catch (ObjectDisposedException)
            {
                DepictInfo = string.Format(CultureInfos, "正在对已释放的对象执行操作，请通过菜单栏<帮助>报告问题！");
            }
            catch (NotFiniteNumberException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }
        #endregion

        #region 发送文件
        internal async Task SendFileAsync()
        {
            if (!SerialPortBase.IsOpen)
            {
                DepictInfo = string.Format(CultureInfos, "请先打开串行端口");

                return;
            }

            try
            {
                OpenFileDialog SendDataOpenFileDialog = new OpenFileDialog
                {
                    Title = string.Format(CultureInfos, "选择发送数据"),
                    DefaultExt = "*.*",
                    Filter = string.Format(CultureInfos, "所有类型|*.*")
                };

                if (SendDataOpenFileDialog.ShowDialog() == true)
                {
                    var filePath = SendDataOpenFileDialog.FileName;

                    if (filePath == null)
                    {
                        return;
                    }

                    HelpModel.StatusBarProgressBarVisibility = "Visible";

                    var fileStream = SendDataOpenFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        var fileContent = reader.ReadToEnd();
                        var SendCount = SerialPortBase.Encoding.GetByteCount(fileContent);

                        DepictInfo = string.Format(CultureInfos, "文件正在发送......");
                        HelpModel.StatusBarProgressBarIsIndeterminate = true;

                        await SerialPortBase.BaseStream.WriteAsync(SerialPortBase.Encoding.GetBytes(fileContent), 0, SendCount)
                            .ConfigureAwait(false);

                        HelpModel.StatusBarProgressBarIsIndeterminate = false;
                        DepictInfo = string.Format(CultureInfos, "文件发送完毕");

                        SendModel.SendDataCount += SendCount;
                    }

                    HelpModel.StatusBarProgressBarVisibility = "Collapsed";
                }
            }
            catch (ArgumentException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (IOException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (OutOfMemoryException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (FormatException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (IndexOutOfRangeException)
            {
                DepictInfo = string.Format(CultureInfos, "正在试图执行越界访问，请通过菜单栏<帮助>报告问题！");
            }
            catch (ObjectDisposedException)
            {
                DepictInfo = string.Format(CultureInfos, "正在对已释放的对象执行操作，请通过菜单栏<帮助>报告问题！");
            }
            catch (NotFiniteNumberException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (InvalidOperationException e)
            {
                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }
        #endregion

        #region 路径选择
        internal void SaveRecvPath()
        {
            SaveFileDialog ReceDataSaveFileDialog = new SaveFileDialog
            {
                Title = string.Format(CultureInfos, "接收数据保存"),
                FileName = string.Format(CultureInfos, "{0}", DateTime.Now.ToString("yyyyMMdd", CultureInfos)),
                DefaultExt = ".txt",
                Filter = string.Format(CultureInfos, "文本文件|*.txt")
            };

            if (ReceDataSaveFileDialog.ShowDialog() == true)
            {
                DataRecvPath = ReceDataSaveFileDialog.FileName;
            }
        }
        #endregion

        #region 清接收区
        internal void ClearReceData()
        {
            RecvModel.RecvData.Delete();

            RecvDataDeleteCount = 1;
        }
        #endregion

        #region 清发送区
        internal void ClearSendData()
        {
            SendModel.SendData = string.Empty;
        }
        #endregion

        #region 清空计数
        internal void ClearCount()
        {
            RecvModel.RecvDataCount = 0;
            SendModel.SendDataCount = 0;
        }
        #endregion

        #region 数据接收事件实现
        private void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if ((SerialPort)sender == null)
            {
                return;
            }

            SerialPort _SerialPort = (SerialPort)sender;

            int _BytesToRead = _SerialPort.BytesToRead;
            byte[] _RecvData = new byte[_BytesToRead];

            _SerialPort.Read(_RecvData, 0, _BytesToRead);

            if (RecvModel.EnableRecv)
            {
                if (TimerModel.TimeStampEnable)
                {
                    DateTime _DateTime = DateTime.Now;
                    RecvModel.RecvData.Append("[ " + _DateTime.ToString("yyyy/MM/dd HH:mm:ss:ffff", CultureInfos) + " ]");
                }

                if (RecvModel.HexRecv)
                {
                    foreach (var tmp in _RecvData)
                    {
                        RecvModel.RecvData.Append(string.Format(CultureInfos, "{0:X2} ", tmp));
                    }
                }
                else
                {
                    RecvModel.RecvData.Append(_SerialPort.Encoding.GetString(_RecvData));
                }

                if (TimerModel.TimeStampEnable)
                {
                    RecvModel.RecvData.Append("\r\n");
                }
            }

            if (SaveRecv)
            {
                RecvModel.RecvAutoSave = string.Format(CultureInfos, "保存中");

                SaveRecvData(_SerialPort.Encoding.GetString(_RecvData));
            }
            else
            {
                RecvModel.RecvAutoSave = string.Format(CultureInfos, "已停止");
            }

            RecvModel.RecvDataCount += _RecvData.Length;

            if (RecvModel.RecvDataCount > (32768 * RecvDataDeleteCount))
            {
                RecvModel.RecvData.Delete();

                RecvDataDeleteCount += 1;   /* 接收区数据达到32MB（32768KB）或其倍数，则自动清空接收区数据 */
            }
        }

        private async void SaveRecvData(string ReceData)
        {
            try
            {
                /* 从路径的字符串长度判断路径是否合法（路径字符串长度小于1个必定不合法） */
                if (DataRecvPath.Length < 1)
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\ReceData\\");

                    DataRecvPath = AppDomain.CurrentDomain.BaseDirectory +
                        "\\ReceData\\" + DateTime.Now.ToString("yyyyMMdd", CultureInfos) + ".txt";

                    using (StreamWriter DefaultReceDataPath = new StreamWriter(DataRecvPath, true))
                    {
                        await DefaultReceDataPath.WriteAsync(ReceData).ConfigureAwait(false);
                    }
                }
                else
                {
                    using (StreamWriter DefaultReceDataPath = new StreamWriter(DataRecvPath, true))
                    {
                        await DefaultReceDataPath.WriteAsync(ReceData).ConfigureAwait(false);
                    }
                }
            }
            catch (IOException e)
            {
                SaveRecv = false;
                RecvModel.RecvAutoSave = string.Format(CultureInfos, "已停止");

                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (ArgumentException e)
            {
                SaveRecv = false;
                RecvModel.RecvAutoSave = string.Format(CultureInfos, "已停止");

                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (UnauthorizedAccessException e)
            {
                SaveRecv = false;
                RecvModel.RecvAutoSave = string.Format(CultureInfos, "已停止");

                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (NotSupportedException e)
            {
                SaveRecv = false;
                RecvModel.RecvAutoSave = string.Format(CultureInfos, "已停止");

                DepictInfo = e.Message.Replace("\r\n", "");
            }
            catch (IndexOutOfRangeException)
            {
                SaveRecv = false;
                RecvModel.RecvAutoSave = string.Format(CultureInfos, "已停止");

                DepictInfo = string.Format(CultureInfos, "正在试图执行越界访问，请通过菜单栏<帮助>报告问题！");
            }
            catch (ObjectDisposedException)
            {
                SaveRecv = false;
                RecvModel.RecvAutoSave = string.Format(CultureInfos, "已停止");

                DepictInfo = string.Format(CultureInfos, "正在对已释放的对象执行操作，请通过菜单栏<帮助>报告问题！");
            }
            catch (AppDomainUnloadedException)
            {
                SaveRecv = false;
                RecvModel.RecvAutoSave = string.Format(CultureInfos, "已停止");

                DepictInfo = string.Format(CultureInfos, "正在访问已卸载的应用程序域，请通过菜单栏<帮助>报告问题！");
            }
            catch (System.Security.SecurityException e)
            {
                SaveRecv = false;
                RecvModel.RecvAutoSave = string.Format(CultureInfos, "已停止");

                DepictInfo = e.Message.Replace("\r\n", "");
            }
        }
        #endregion

        #region 信号状态事件实现
        private void SerialPortPinChanged(object sender, SerialPinChangedEventArgs e)
        {
            if ((SerialPort)sender == null)
            {
                return;
            }

            if (e == null)
            {
                return;
            }

            SerialPort _SerialPort = (SerialPort)sender;

            switch (e.EventType)
            {
                case SerialPinChange.CDChanged:
                    if (_SerialPort.CDHolding)
                    {
                        SerialPortModel.DcdBrush = Brushes.GreenYellow;
                    }
                    else
                    {
                        SerialPortModel.DcdBrush = Brushes.Black;
                    }
                    break;
                case SerialPinChange.CtsChanged:
                    if (_SerialPort.CtsHolding)
                    {
                        SerialPortModel.CtsBrush = Brushes.GreenYellow;
                    }
                    else
                    {
                        SerialPortModel.CtsBrush = Brushes.Black;
                    }
                    break;
                case SerialPinChange.DsrChanged:
                    if (_SerialPort.DsrHolding)
                    {
                        SerialPortModel.DsrBrush = Brushes.GreenYellow;
                    }
                    else
                    {
                        SerialPortModel.DsrBrush = Brushes.Black;
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region RecvTextBox Mouse Double Support
        internal void EnableRecv()
        {
            RecvModel.EnableRecv = !RecvModel.EnableRecv;

            if (RecvModel.EnableRecv)
            {
                RecvModel.RecvEnable = string.Format(CultureInfos, "允许");
            }
            else
            {
                RecvModel.RecvEnable = string.Format(CultureInfos, "暂停");
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            SerialPortModel = new SerialPortModel();
            SerialPortModel.SerialPortDataContext();

            SendModel = new SendModel();
            SendModel.SendDataContext();

            RecvModel = new RecvModel();
            RecvModel.RecvDataContext();

            TimerModel = new TimerModel();
            TimerModel.TimerDataContext();

            SaveRecv = false;
            HexSend = false;
            AutoSend = false;
            InitAutoSendTimer();

            DepictInfo = string.Format(CultureInfos, "串端调试助手");
        }

        #region IDisposable Support
        private bool disposedValue = false;   /* 冗余检测 */

        /// <summary>
        /// 释放组件所使用的非托管资源，并且有选择的释放托管资源（可以看作是Dispose()的安全实现）
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            /* 检查是否已调用dispose */
            if (!disposedValue)
            {
                if (disposing)
                {
                    /* 释放托管资源（如果需要的话） */

                    /* SerialPort属于托管资源，但其本身却拥有非托管资源，所以需要实现IDisposable */
                    SerialPortBase.DataReceived -= SerialPortDataReceived;
                    SerialPortBase.PinChanged -= SerialPortPinChanged;
                    SerialPortBase.Dispose();
                }

                /* 释放非托管资源（如果有的话） */

                disposedValue = true;   /* 处理完毕 */
            }
        }

        /// <summary>
        /// 实现IDisposable，释放组件所使用的所有资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);   /* this: CommunWPF.ViewModels.MainWindowViewmodel */
        }
        #endregion
    }
}
