using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commun.NetWork
{
    public static class CommunDataValid
    {
        /// <summary>
        /// CRC校验
        /// </summary>
        /// <param name="crccheck"></param>
        /// <returns></returns>
        public static string CRC(string crccheck)
        {
            int crc = 65535;///将CRC寄存器放入FFFF(十进制的65535)
            byte[] crcbyte = new byte[crccheck.Length / 2];///因为两个字符为一个字节所以除以2 
            int k = 0;
            for (int i = 0; i < crcbyte.Length; i++)//每次截取两个字转成字节型
            {
                crcbyte[i] = Convert.ToByte((crccheck.Substring(k, 2)), 16);//每次截取两个字转成10进制字节型
                k = k + 2;
            }
            for (int i = 0; i < crcbyte.Length; i++)///将所有的字节进行CRC检验出来检验码
            {
                crc = crc ^ crcbyte[i];///CRC第一步异或    
                for (int j = 0; j < 8; j++)
                {
                    string str = dectobin(crc);
                    if (str[15] == '0')///注意判断时候一定是字符（'0'）判断而不是(0)也不是("0");
                    {
                        crc = crc >> 1;///crc右移1位             
                    }
                    else
                    {
                        crc = crc >> 1;///crc右移1位
                        crc = crc ^ 40961;///与多项式异或
                    }
                }
            }

            string str1 = Convert.ToString(crc, 16).ToUpper().PadLeft(4, '0');
            return str1;

        }

        public static string dectobin(int dec)///转换二进制
        {
            string bin = "";
            try
            {
                while (dec != 0)
                {
                    bin = (dec % 2).ToString() + bin;///求余
                    dec = dec / 2;///求商

                }
            }
            catch (Exception ex)
            {
                throw new Exception("无法转换成16位的二进制！");
            }

            string str = (Convert.ToInt64(bin)).ToString("0000000000000000");///保持16位二进制
            return str;
        }

        /// <summary>
        /// LRC校验
        /// </summary>
        /// <param name="lrccheck"></param>
        /// <returns></returns>
        public static  string LRC(string lrccheck)
        {
            byte[] lrcbyte = new byte[lrccheck.Length / 2];////为什么除以2？因为MODBUS是两个字符为一个字节
            int k = 0;
            Int64 lrcadd = 0;
            Int64 lrc = 0;
            for (int i = 0; i < lrcbyte.Length; i++)////将传来的数据以两个字符形式存为一个字节  因为MODBUS默认是2个字符2个字节
            {
                lrcbyte[i] = Convert.ToByte(lrccheck.Substring(k, 2), 16);///将俩个字符的字节转换成10进制
                k = k + 2;

            }
            foreach (var item in lrcbyte)///lrc校验是把所有的俩个字符的字节的10进制进行累加后判断是否超过255
            {
                lrcadd = item + lrcadd;///累加所有字节

            }
            if (lrcadd > 255)///lrcadd大于255就用它本身求余256再用256减它得到的数据放到lrc
            {

                lrc = 256 - lrcadd % 256;
            }
            else///////否则就直接减256
            {
                lrc = 256 - lrcadd;
            }
            return Convert.ToString(lrc, 16).ToUpper().PadLeft(2, '0');///最低保留两位十六进制并且是大写的

        }
    }
}
