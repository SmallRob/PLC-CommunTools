using CommunTools.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CommunTools.Enum
{
    public static class FuncItemCom
    {
        /// <summary>
        /// 基础通信
        /// </summary>
        [Flags]
        [FuncGroup("Base", "基础通信")]
        public enum Com_BaseFuncItem
        {
            [Description("TCP/IP客户端")]
            [FuncURI("Frm_TCPClient")]
            TCP_Client = 1,

            [Description("TCP/IP服务端")]
            [FuncURI("Frm_TCPServer")]
            TCP_Server = 2,

            [Description("Socket通信")]
            [FuncURI("Frm_ComSocket")]
            COM_Socket = 3,

            [Description("串口通讯")]
            [FuncURI("Frm_SerialPort")]
            COM_SerialPort = 4
        }

        /// <summary>
        /// 硬件协议通信
        /// </summary>
        [Flags]
        [FuncGroup("Proto", "硬件协议通信")]
        public enum Com_ProtoFuncItem
        {
            [Description("SUSI协议I/O口")]
            [FuncURI("Frm_SUSI")]
            Proto_SUSI = 1,

            [Description("Twain扫描驱动")]
            [FuncURI("Frm_Twain")]
            Proto_Twain = 2,

            [Description("BCNet-A以太通讯(三菱)")]
            [FuncURI("Frm_BCNet")]
            Proto_BCNet_A = 3,

            [Description("MELSEC-F控制器(三菱)")]
            [FuncURI("Frm_MELSEC")]
            Proto_MELSEC_F = 4,

            [Description("CS1W系列(欧姆龙)")]
            [FuncURI("Frm_CS1W")]
            Proto_HL8202 = 5,

            //SECS
            [Description("半导体自动化SECS协议")]
            [FuncURI("Frm_SECS")]
            Proto_SECS = 6,

            //松下电工 FP
            [Description("松下电工FP控制器")]
            [FuncURI("Frm_NAIS_FP")]
            Proto_NAIS_FP = 7,

            [Description("台达PLC通讯")]
            [FuncURI("Frm_DELTA_PLC")]
            Proto_DELTA_PLC = 8
        }
    }
}
