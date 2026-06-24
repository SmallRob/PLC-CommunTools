using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZCS_FormUI.Render
{
    /// <summary>
    /// 事件数据
    /// </summary>
    public class LEventArgs : EventArgs
    {
        /// <summary>
        /// 事件数据
        /// </summary>
        /// <param name="value">值</param>
        public LEventArgs(object value)
        {
            Value = value;
        }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
    }

    /// <summary>
    /// 滚动条事件数据
    /// </summary>
    public class LScrollEventArgs : EventArgs
    {
        /// <summary>
        /// 滚动条事件数据
        /// </summary>
        /// <param name="sliderPosition">滑块距顶部（垂直）/左侧（横向）位置</param>
        /// <param name="showPosition">显示位置（正数）</param>
        public LScrollEventArgs(float sliderPosition, float showPosition)
        {
            SliderPosition = sliderPosition;
            ShowPosition = showPosition;
        }

        /// <summary>
        /// 滑块距顶部（垂直）/左侧（横向）位置
        /// </summary>
        public float SliderPosition { get; set; }
        /// <summary>
        /// 显示位置（正数）
        /// </summary>
        public float ShowPosition { get; set; }
    }
}
