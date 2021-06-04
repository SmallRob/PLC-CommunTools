using CommunWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunWPF.Models
{
    internal class HelpModel : MainWindowBase
    {
        /// <summary>
        /// 本地软件的版本号
        /// </summary>
        public string VerInfoNumber
        {
            get; set;
        }

        /// <summary>
        /// 菜单栏 - 帮助 - 本地版本信息
        /// </summary>
        private string _VerInfo;
        public string VerInfo
        {
            get
            {
                return _VerInfo;
            }
            set
            {
                if (_VerInfo != value)
                {
                    _VerInfo = value;
                    RaisePropertyChanged(nameof(VerInfo));
                }
            }
        }

        /// <summary>
        /// 服务器软件的版本信息
        /// </summary>
        public string UpdateInfo
        {
            get; set;
        }

        #region 菜单栏 - 视图 - 精简视图
        /// <summary>
        /// 菜单栏 - 视图 - 精简视图
        /// </summary>
        private bool _ReducedEnable;
        public bool ReducedEnable
        {
            get
            {
                return _ReducedEnable;
            }
            set
            {
                if (_ReducedEnable != value)
                {
                    _ReducedEnable = value;
                    RaisePropertyChanged(nameof(ReducedEnable));
                }
            }
        }

        /// <summary>
        /// 精简视图 - 视图可见性
        /// </summary>
        private string _ViewVisibility;
        public string ViewVisibility
        {
            get
            {
                return _ViewVisibility;
            }
            set
            {
                if (_ViewVisibility != value)
                {
                    _ViewVisibility = value;
                    RaisePropertyChanged(nameof(ViewVisibility));
                }
            }
        }
        #endregion

        #region 状态栏 - 发送文件进度条可见性
        private string _StatusBarProgressBarVisibility;
        public string StatusBarProgressBarVisibility
        {
            get
            {
                return _StatusBarProgressBarVisibility;
            }
            set
            {
                if (_StatusBarProgressBarVisibility != value)
                {
                    _StatusBarProgressBarVisibility = value;
                    RaisePropertyChanged(nameof(StatusBarProgressBarVisibility));
                }
            }
        }

        private double _StatusBarProgressBarValue;
        public double StatusBarProgressBarValue
        {
            get
            {
                return _StatusBarProgressBarValue;
            }
            set
            {
                if (_StatusBarProgressBarValue != value)
                {
                    _StatusBarProgressBarValue = value;
                    RaisePropertyChanged(nameof(StatusBarProgressBarValue));
                }
            }
        }

        private bool _StatusBarProgressBarIsIndeterminate;
        public bool StatusBarProgressBarIsIndeterminate
        {
            get
            {
                return _StatusBarProgressBarIsIndeterminate;
            }
            set
            {
                if (_StatusBarProgressBarIsIndeterminate != value)
                {
                    _StatusBarProgressBarIsIndeterminate = value;
                    RaisePropertyChanged(nameof(StatusBarProgressBarIsIndeterminate));
                }
            }
        }
        #endregion       
    }
}
