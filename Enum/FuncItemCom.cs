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
            [Description("SUSI网口协议")]
            [FuncURI("Frm_SUSI")]
            Proto_SUSI = 1,

            [Description("Twain扫描驱动")]
            [FuncURI("Frm_Twain")]
            Proto_Twain = 2
        }
    }
}
