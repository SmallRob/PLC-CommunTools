using CommunTools.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CommunTools.Enums
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
            /// <summary>
            /// TCP服务端
            /// </summary>
            [Description("  TCP/IP服务端")]
            [FuncURI("Frm_TCPServer")]
            TCP_Server = 1,

            /// <summary>
            /// TCP客户端
            /// </summary>
            [Description("  TCP/IP客户端")]
            [FuncURI("Frm_TCPClient")]
            TCP_Client = 2,

            /// <summary>
            /// 串口转发端
            /// </summary>
            [Description(" 串口接收转发端")]
            [FuncURI("Frm_SerialServer")]
            COM_SerialServer = 3,

            /// <summary>
            /// 串口发送端
            /// </summary>
            [Description(" 串口通讯发送端")]
            [FuncURI("Frm_SerialPort")]
            COM_SerialPort = 4,

            /// <summary>
            /// Socket服务端
            /// </summary>
            [Description("  Socket服务端")]
            [FuncURI("Frm_SocketServer")]
            COM_SocketServer = 5,

            /// <summary>
            /// Socket客户端
            /// </summary>
            [Description("  Socket客户端")]
            [FuncURI("Frm_ComSocket")]
            COM_SocketClient = 6,

            /// <summary>
            /// WebSocket
            /// </summary>
            [Description("WebSocket服务端")]
            [FuncURI("Frm_WebSocketServer")]
            COM_WebSocketServer = 7,

            /// <summary>
            /// WebSocket
            /// </summary>
            [Description("WebSocket客户端")]
            [FuncURI("Frm_ComWebSocket")]
            COM_WebSocket = 8,

            /// <summary>
            /// WebSocket
            /// </summary>
            [Description("    UDP通讯")]
            [FuncURI("Frm_ComUDP")]
            COM_ComUDP = 9
        }

        /// <summary>
        /// 硬件协议通信
        /// </summary>
        [Flags]
        [FuncGroup("Proto", "硬件协议通信")]
        public enum Com_ProtoFuncItem
        {
            //[Description(" SUSI I/O口通信")]
            //[FuncURI("Frm_SUSI")]
            //Proto_SUSI = 1,

            [Description("    MQTT协议")]
            [FuncURI("Frm_MQTT")]
            Proto_MQTT = 1,

            [Description("    施耐德PLC")]
            [FuncURI("Frm_SCHNEIDER")]
            Proto_SCHND = 2,

            //松下电工 FP
            [Description("   电工FP控制器$  (松下PLC)")]
            [FuncURI("Frm_NAIS_FP")]
            Proto_NAIS_FP = 3,

            [Description("   FincTCP通信$(欧姆龙PLC)")]
            [FuncURI("Frm_CS1W")]
            Proto_HL8202 = 4,

            [Description("   Modbus通讯$(三菱PLC)")]
            [FuncURI("Frm_BCNet_A")]
            Proto_BCNet_A = 5,

            [Description("  SEC-F控制器$(三菱PLC)")]
            [FuncURI("Frm_MELSEC")]
            Proto_MELSEC_F = 6,

            [Description("  西门子S7 PLC")]
            [FuncURI("Frm_S7")]
            Proto_S7 = 7,

            [Description("   Modbus通讯$(西门子PLC)")]
            [FuncURI("Frm_Modbus")]
            Proto_Modbus = 8,

            //SECS
            [Description("  SECS PLC协议$半导体自动化")]
            [FuncURI("Frm_SECS")]
            Proto_SECS = 9,        

            [Description("  台达PLC通讯")]
            [FuncURI("Frm_DELTA_PLC")]
            Proto_DELTA_PLC = 10
        }

        /// <summary>
        /// 硬件协议通信
        /// </summary>
        [Flags]
        [FuncGroup("Other", "其他硬件通信")]
        public enum Com_OtherFuncItem
        {
            [Description("Twain扫描仪协议")]
            [FuncURI("Frm_Twain")]
            Proto_Twain = 1
        }
    }
}
