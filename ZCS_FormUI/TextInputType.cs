using System.ComponentModel;

namespace ZCS_FormUI
{
    public enum TextInputType
    {
        /// <summary>
        /// 不控制输入
        /// </summary>
        [Description("不控制输入")]
        NotControl = 1,
        /// <summary>
        /// 任意数字
        /// </summary>
        [Description("任意数字")]
        Number = 2,
        /// <summary>
        /// 非负数
        /// </summary>
        [Description("非负数")]
        UnsignNumber = 4,
        /// <summary>
        /// 正数
        /// </summary>
        [Description("正数")]
        PositiveNumber = 8,
        /// <summary>
        /// 整数
        /// </summary>
        [Description("整数")]
        Integer = 16,
        /// <summary>
        /// 非负整数
        /// </summary>
        [Description("非负整数")]
        PositiveInteger = 32,
        /// <summary>
        /// 正则验证
        /// </summary>
        [Description("正则验证")]
        Regex = 64
    }
}
