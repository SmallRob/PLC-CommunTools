using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace CommunWPF.ViewModels
{
    internal class MainWindowBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 提供区域性信息
        /// </summary>
        internal CultureInfo CultureInfos = new CultureInfo(CultureInfo.CurrentUICulture.Name);

        /// <summary>
        /// 提供属性更改事件的方法
        /// </summary>
        /// <param name="propertyName"></param>
        internal void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;

            if (handler != null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
