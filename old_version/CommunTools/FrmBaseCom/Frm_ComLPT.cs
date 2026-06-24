using Com_CSSkin;
using Commun.NetWork.MQTT;
using CommunTools.Common;
using CommunTools.Common.Twain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CommunTools
{
    /// <summary>
    /// LPT并口通讯
    /// </summary>
    public partial class Frm_ComLPT : CSSkinMain
    {
        public Frm_ComLPT()
        {
            InitializeComponent();
        }

        static int portAddress = 0x378; // Typically, 0x378 is the address for LPT1
        static int portDecData = 0xFF;

        public class PortControl // Import dll to project
        {
            [DllImport("inpout32.dll", EntryPoint = "Out32")]
            public static extern void Output(int portAddress, int data); // decimal

            [DllImport("inpout32.dll", EntryPoint = "Inp32")]
            public static extern int Input(int portAddress);

            public static void WriteDataPort(int data)
            {
                Output(portAddress, data);
            }

            public static void WriteControlPort(int data)
            {
                data = data ^ 0xB;
                data = data & 0xF;
                Output(0x037A, data);
            }

            public static int ReadControlPort()
            {
                int data = Input(0x037A);
                data = data ^ 0xB;
                data = data & 0xF;
                return data;
            }

            public static int ReadStatusPort()
            {
                int ValueGet = Input(0x0379);

                ValueGet = ValueGet ^ 0x80;
                ValueGet = ValueGet & 0xF0;
                ValueGet = ValueGet >> 4;

                return ValueGet;
            }
        }

        private void btnInp_BtnClick(object sender, EventArgs e)
        {
            // Read a value from the parallel port
            Console.WriteLine("Reading value from the parallel port...");
            int value = PortControl.ReadControlPort();
            Console.WriteLine($"Value read from port: {value:X}");
            richTextBox_Send.AppendText("读取数据 " + value + "\n");
        }

        private void btnOut_BtnClick(object sender, EventArgs e)
        {
            // Write a value to the parallel port
            Console.WriteLine("Writing value to the parallel port...");
            PortControl.WriteDataPort(portDecData); // Sending 0xFF (all bits high)
            richTextBox_Send.AppendText ("写入数据 " + PortControl.ReadStatusPort()+"\n");

        }
    }
}
