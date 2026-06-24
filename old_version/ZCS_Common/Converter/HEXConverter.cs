using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZCS_Common
{
    public static class HEXConverter
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

        public static string BitConverterHexFromStr(string strbuff)
        {
            var bytes = Encoding.UTF8.GetBytes(strbuff);
            var hex = BitConverter.ToString(bytes, 0).Replace("-", string.Empty).ToLower();
            return hex;
        }

        /// <summary>
        /// 将HEX格式的字符转换为UTF-8字符
        /// </summary>
        /// <param name="hexStr"></param>
        /// <returns></returns>
        public static string GetBytesFromHexBuffer(string hexStr)
        {
            var inputByteArray = new byte[hexStr.Length / 2];
            for (var x = 0; x < inputByteArray.Length; x++)
            {
                var i = Convert.ToInt32(hexStr.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            return Encoding.UTF8.GetString(inputByteArray);
        }
    }
}
