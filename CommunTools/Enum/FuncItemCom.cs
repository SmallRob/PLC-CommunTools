﻿using CommunTools.Common;
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
            COM_SerialPort = 4,

            [Description("MQTT协议")]
            [FuncURI("Frm_MQTT")]
            COM_MQTT = 5
        }

        /// <summary>
        /// 硬件协议通信
        /// </summary>
        [Flags]
        [FuncGroup("Proto", "硬件协议通信")]
        public enum Com_ProtoFuncItem
        {
            [Description("SUSI I/O口通信")]
            [FuncURI("Frm_SUSI")]
            Proto_SUSI = 1,

            [Description("Twain扫描仪协议")]
            [FuncURI("Frm_Twain")]
            Proto_Twain = 2,

            [Description("   Modbus通讯$(三菱PCL)")]
            [FuncURI("Frm_BCNet_A")]
            Proto_BCNet_A = 3,

            [Description(" SEC-F控制器$(三菱PCL)")]
            [FuncURI("Frm_MELSEC")]
            Proto_MELSEC_F = 4,

            [Description("   Modbus通讯$(西门子PLC)")]
            [FuncURI("Frm_Modbus")]
            Proto_Modbus = 5,

            [Description(" S7西门子PLC")]
            [FuncURI("Frm_S7")]
            Proto_S7 = 6,

            [Description("   FincTCP通信$(欧姆龙PLC)")]
            [FuncURI("Frm_CS1W")]
            Proto_HL8202 = 7,

            //SECS
            [Description("    SECS协议$半导体自动化")]
            [FuncURI("Frm_SECS")]
            Proto_SECS = 8,

            //松下电工 FP
            [Description("   电工FP控制器$(松下)")]
            [FuncURI("Frm_NAIS_FP")]
            Proto_NAIS_FP = 9,

            [Description("  台达PLC通讯")]
            [FuncURI("Frm_DELTA_PLC")]
            Proto_DELTA_PLC = 10
        }
    }
}
