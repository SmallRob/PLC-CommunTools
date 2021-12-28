using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommunTools.Common
{
    public static class ByteDataCommon
    {
        /// <summary>
        /// 十六进制数组形式的CRC—A001
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] CRC_16_A001(byte[] data)
        {
            //int len = data.Length;
            //ushort tt;
            if (data.Length > 0)
            {
                ushort crc = 0xFFFF;
                for (int i = 0; i < data.Length; i++)
                {
                    crc = (ushort)(crc ^ (data[i]));
                    for (int j = 0; j < 8; j++)
                    {
                        #region
                        //也可以用功能相同
                        /*tt = (ushort)(crc & 1);
                        crc = (ushort)(crc >> 1);
                        crc = (ushort)(crc & 0x7fff);
                        if (tt == 1)
                        {
                            crc = (ushort)(crc ^ 0xa001);
                        }
                        crc = (ushort)(crc & 0xffff);*/
                        #endregion
                        crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ 0xA001) : (ushort)(crc >> 1);

                    }
                }

                byte hi = (byte)((crc & 0xFFFF) >> 8);//高位置
                byte lo = (byte)(crc & 0xFFFF);//低位置
                return new byte[] { hi, lo };

            }
            return new byte[] { 0, 0 };
        }

        public static byte byLength(byte[] by)//总长度
        {
            byte myBy;
            myBy = Convert.ToByte(by.Length);
            return myBy;
        }

        public static Int16 int16Length(byte[] by)//总长度
        {
            Int16 myBy;
            myBy = Convert.ToByte(by.Length);
            return myBy;
        }
    }
}
