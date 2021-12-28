using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CommunTools.Enums
{
    public static class SerialPortEnum
    {
        /// <summary>
        /// 奇偶性
        /// </summary>
        [Flags]
        public enum PortParity
        {
            /// <summary>
            /// 0
            /// </summary>
            [Description("None")]
            None = 0,

            /// <summary>
            /// 1
            /// </summary>
            [Description("Odd")]
            Odd = 1,

            /// <summary>
            /// 2
            /// </summary>
            [Description("Even")]
            Even = 2,

            /// <summary>
            /// 3
            /// </summary>
            [Description("Mark")]
            Mark = 3,

            /// <summary>
            /// 4
            /// </summary>
            [Description("Space")]
            Space = 4
        }

        /// <summary>
        /// 停止位
        /// </summary>
        public enum StopBits
        {
            /// <summary>
            /// 1
            /// </summary>
            [Description("1")]
            One = 0,

            /// <summary>
            /// 1.5
            /// </summary>
            [Description("1.5")]
            OnePointFive = 1,

            /// <summary>
            /// 2
            /// </summary>
            [Description("2")]
            Two = 2
        }

        /// <summary>
        /// 波特率
        /// </summary>
        //"4800",
        //"9600",
        //"14400",
        //"19200",
        //"38400",
        //"57600",
        //"115200"

        public enum BandRate
        {
            [Description("4800")]
            Low48 = 0,

            [Description("9600")]
            Low96 = 1,

            [Description("14400")]
            Mid144 = 2,

            [Description("19200")]
            Mid192 = 3,

            [Description("38400")]
            Mid384 = 4,

            [Description("57600")]
            High576 = 5,

            [Description("115200")]
            High1152 = 6
        }

        /// <summary>
        /// 数据位
        /// </summary>

        public enum DataBit
        {
            [Description("8")]
            Eight = 8,

            [Description("9")]
            Nine = 9
        }

        /// <summary>
        /// 握手协议
        /// </summary>
        public enum HandShake
        {
            [Description("None")]
            None,

            [Description("XOnXOff")]
            XOnXOff,

            [Description("RequestToSend")]
            RequestToSend,

            [Description("RequestToSendXOnXOff")]
            RequestToSendXOnXOff
        }
    }
}
