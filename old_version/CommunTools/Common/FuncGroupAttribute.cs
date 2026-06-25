using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunTools.Common
{
    public class FuncGroupAttribute : Attribute
    {
        public FuncGroupAttribute(string groupTag, string groupName)
        {
            _groupTag = groupTag;
            _groupName = groupName;
        }

        private string _groupTag;
        private string _groupName;

        /// <summary>
        /// 分组Tag
        /// </summary>
        public string GroupTag
        {
            get { return _groupTag; }
        }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName
        {
            get { return _groupName; }
        }
    }
}
