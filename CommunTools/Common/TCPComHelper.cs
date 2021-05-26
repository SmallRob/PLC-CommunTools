using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunTools.Common
{
    public static class TCPComHelper
    {
        public static string GetBufferFormatHex(byte[] bts)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte itemBt in bts)
            {
                sb.Append(itemBt.ToString("X2") + " ");
            }
            return sb.ToString();
        }
    }
}
