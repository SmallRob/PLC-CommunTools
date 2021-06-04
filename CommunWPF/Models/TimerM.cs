using CommunWPF.ViewModels;
using System;
using System.Timers;

namespace CommunWPF.Models
{
    internal class TimerModel : MainWindowBase
    {
        #region 字段
        private static Timer SystemTimer = null;   /* 该对象持续存在于整个应用程序运行期间 */
        #endregion

        /// <summary>
        /// 菜单栏 - 时间戳
        /// </summary>
        private bool _TimeStampEnable;
        public bool TimeStampEnable
        {
            get
            {
                return _TimeStampEnable;
            }
            set
            {
                if (_TimeStampEnable != value)
                {
                    _TimeStampEnable = value;
                    RaisePropertyChanged(nameof(TimeStampEnable));
                }
            }
        }

        /// <summary>
        /// 状态栏 - 系统时间
        /// </summary>
        private string _SystemTime;
        public string SystemTime
        {
            get
            {
                return _SystemTime;
            }
            set
            {
                if (_SystemTime != value)
                {
                    _SystemTime = value;
                    RaisePropertyChanged(nameof(SystemTime));
                }
            }
        }

        public void InitSystemClockTimer()
        {
            SystemTimer = new Timer
            {
                Interval = 1000
            };

            SystemTimer.Elapsed += SystemTimer_Elapsed;
            SystemTimer.AutoReset = true;
            SystemTimer.Enabled = true;
        }

        private void SystemTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SystemTime = SystemTimeData();
        }

        private string SystemTimeData()
        {
            DateTime _DateTime = DateTime.Now;

            return string.Format(CultureInfos, "{0}年{1}月{2}日 {3}:{4}:{5}",
                _DateTime.Year.ToString("0000", CultureInfos),
                _DateTime.Month.ToString("00", CultureInfos),
                _DateTime.Day.ToString("00", CultureInfos),
                _DateTime.Hour.ToString("00", CultureInfos),
                _DateTime.Minute.ToString("00", CultureInfos),
                _DateTime.Second.ToString("00", CultureInfos));
        }

        public void TimerDataContext()
        {
            TimeStampEnable = false;

            SystemTime = string.Format(CultureInfos, "2019年08月31日 12:13:15");
            InitSystemClockTimer();
        }
    }
}
