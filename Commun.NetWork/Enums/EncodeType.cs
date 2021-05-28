using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Commun.NetWork.Enums
{
    /// <summary>
    /// 编码格式
    /// </summary>
    /// <remarks>
    /// 936：简体中文GB2312
    /// 54936：简体中文GB18030
    /// 950：繁体中文BIG5
    /// 1252：西欧字符CP1252
    /// 65001：UTF-8编码
    /// </remarks>
    [Flags]
    public enum EncodeType
    {
        /// <summary>
        /// 简体中文GB2312
        /// </summary>
        [Description("GB2312")]
        GB2312 = 936,

        /// <summary>
        /// 简体中文GB18030
        /// </summary>
        [Description("GB18030")]
        GB18030 = 54936,

        /// <summary>
        /// 繁体中文BIG5
        /// </summary>
        [Description("BIG5")]
        BIG5 = 950,

        /// <summary>
        /// 西欧字符CP1252
        /// </summary>
        [Description("CP1252")]
        CP1252 = 1252,

        /// <summary>
        /// UTF-8编码
        /// </summary>
        [Description("UTF-8")]
        UTF8 = 65001
    }
}
