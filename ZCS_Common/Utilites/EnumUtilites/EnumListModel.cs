using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCS_Common
{
    /// <summary>
    /// 枚举集合的对象类
    /// </summary>
    public class EnumListModel
    {
        /// <summary>
        /// 枚举
        /// </summary>
        public Enum EnumType { get; set; }

        /// <summary>
        /// 枚举ID
        /// </summary>
        public int EnumId { get; set; }

        /// <summary>
        /// 枚举名称
        /// </summary>
        public string EnumName { get; set; }

        /// <summary>
        /// 枚举描述
        /// </summary>
        public string EnumDescrip { get; set; }
    }
}
