using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunTools.Enums
{
    [Flags]
    public enum EnumEncoding
    {
        [Description("ASCII")]
        ASCII = 0,

        [Description("UTF8")]
        UTF8 = 1,

        [Description("Unicode")]
        Unicode = 2,

        [Description("UTF32")]
        UTF32 = 3
    }
}
