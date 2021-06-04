using System;
using System.Text;

namespace CommunWPF.Interfaces
{
    public interface ITextBoxAppend
    {
        /// <summary>
        /// 移除所有字符
        /// </summary>
        void Delete();

        /// <summary>
        /// 从startIndex开始移除length个字符
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        void Delete(int startIndex, int length);

        /// <summary>
        /// 追加字符串
        /// </summary>
        /// <param name="value"></param>
        void Append(string value);

        /// <summary>
        /// 将字符串插入到指定位置
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        void Append(string value, int index);

        /// <summary>
        /// 获取当前值
        /// </summary>
        /// <returns></returns>
        string GetCurrentValue();

        /// <summary>
        /// 移除所有字符事件的处理方法
        /// </summary>
        event EventHandler<BufferClearingHandlerEventArgs> BufferClearingHandler;

        /// <summary>
        /// 追加字符串事件的处理方法
        /// </summary>
        event EventHandler<BufferAppendedHandlerEventArgs> BufferAppendedHandler;
    }

    #region 接口实现
    public class IClassTextBoxAppend : ITextBoxAppend
    {
        readonly StringBuilder stringBuilder = new StringBuilder();

        readonly BufferClearingHandlerEventArgs bufferClearingHandlerEventArgs =
            new BufferClearingHandlerEventArgs();

        readonly BufferAppendedHandlerEventArgs bufferAppendedHandlerEventArgs =
            new BufferAppendedHandlerEventArgs();

        public void Delete()
        {
            stringBuilder.Clear();

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                BufferClearingHandler(this, bufferClearingHandlerEventArgs);
            });
        }

        public void Delete(int startIndex, int length)
        {
            stringBuilder.Remove(startIndex, length);
        }

        public void Append(string value)
        {
            stringBuilder.Append(value);

            bufferAppendedHandlerEventArgs.AppendedText = value;

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                BufferAppendedHandler(this, bufferAppendedHandlerEventArgs);
            });
        }

        public void Append(string value, int index)
        {
            if (index == stringBuilder.Length)
            {
                stringBuilder.Append(value);
            }
            else
            {
                stringBuilder.Insert(index, value);
            }
        }

        public string GetCurrentValue()
        {
            return stringBuilder.ToString();
        }

        public event EventHandler<BufferClearingHandlerEventArgs> BufferClearingHandler;

        public event EventHandler<BufferAppendedHandlerEventArgs> BufferAppendedHandler;
    }

    public class BufferClearingHandlerEventArgs : EventArgs
    {
        public string ClearingText { get; set; }
    }

    public class BufferAppendedHandlerEventArgs : EventArgs
    {
        public string AppendedText { get; set; }
    }
    #endregion
}
