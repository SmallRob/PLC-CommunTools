using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunTools.Common
{
    public class FuncURIAttribute : Attribute
    {
        public FuncURIAttribute(string funcUri)
        {
            _funcUri = funcUri;
        }

        private string _funcUri;

        /// <summary>
        /// 功能Uri
        /// </summary>
        public string FuncUri
        {
            get { return _funcUri; }
        }
    }
}
