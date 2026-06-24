using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZCS_FormUI.Render
{
    /// <summary>
    /// 方向
    /// </summary>
    public enum ScrollOrientation
    {
        /// <summary>
        /// 水平方向（从左到右）
        /// </summary>
        Horizontal_LR,
        /// <summary>
        /// 水平方向（从右到左）
        /// </summary>
        Horizontal_RL,
        /// <summary>
        /// 垂直方向（从上到上）
        /// </summary>
        Vertical_BT,
        /// <summary>
        /// 垂直方向（从上到下）
        /// </summary>
        Vertical_TB
    }

    /// <summary>
    /// 鼠标状态
    /// </summary>
    public enum MouseStatus
    {
        /// <summary>
        /// 鼠标进入
        /// </summary>
        Enter,
        /// <summary>
        /// 鼠标离开
        /// </summary>
        Leave,
        /// <summary>
        /// 鼠标按下
        /// </summary>
        Down,
        /// <summary>
        /// 鼠标按下释放
        /// </summary>
        Up
    }

    /// <summary>
    /// 滚动条方向
    /// </summary>
    public enum OrientationScrollBar
    {
        /// <summary>
        /// 水平方向
        /// </summary>
        Horizontal,
        /// <summary>
        /// 垂直方向
        /// </summary>
        Vertical
    }

    /// <summary>
    /// 主题颜色
    /// </summary>
    public enum ShowColor
    {
        Back,
        Bounce,
        Circle,
        Quadratic,
        Cubic,
        Quartic,
        Quintic,
        Elastic,
        Exponential,
        Sine
    }

    public enum LEaseMode
    {
        EaseIn,
        EaseOut,
        EaseInOut
    }
}
