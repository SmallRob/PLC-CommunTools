using System;
using System.Collections.Generic;
using System.Linq;

namespace Commun.NetCore
{
    public static class ModbusValid
    {
        /// <summary>
        /// CRC计算
        /// </summary>
        /// <param name="value"></param>
        /// <param name="poly"></param>
        /// <param name="crcInit"></param>
        /// <returns></returns>
        public static List<byte> CRC16(List<byte> value, ushort poly = 0xA001, ushort crcInit = 0xFFFF)
        {
            if (value == null || !value.Any())
                throw new ArgumentException("");

            //运算
            ushort crc = crcInit;
            for (int i = 0; i < value.Count; i++)
            {
                crc = (ushort)(crc ^ (value[i]));
                for (int j = 0; j < 8; j++)
                {
                    crc = (crc & 1) != 0 ? (ushort)((crc >> 1) ^ poly) : (ushort)(crc >> 1);
                }
            }
            byte hi = (byte)((crc & 0xFF00) >> 8);  //高位置
            byte lo = (byte)(crc & 0x00FF);         //低位置

            List<byte> buffer = new List<byte>();
            //buffer.AddRange(value);
            buffer.Add(lo);
            buffer.Add(hi);
            return buffer;
        }

        /// <summary>
        /// LRC验证
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static List<byte> LRC(List<byte> value)
        {
            if (value == null) return null;

            int sum = 0;
            for (int i = 0; i < value.Count; i++)
            {
                sum += value[i];
            }

            sum = sum % 256;
            sum = 256 - sum;

            value.Add((byte)sum);
            return value;
        }
    }
}
