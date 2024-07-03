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

        public class PortControl // Import dll to project
        {
            [DllImport("inpout32.dll", EntryPoint = "Out32")]
            public static extern void Output(int portAddress, int value); // decimal

            [DllImport("inpout32.dll", EntryPoint = "Inp32")]
            public static extern int Input(int portAddress);
        }

        static int portAddress = 0x378; // Typically, 0x378 is the address for LPT1
        static int portDecData = 0xFF;

        private void btnInp_BtnClick(object sender, EventArgs e)
        {
            // Read a value from the parallel port
            Console.WriteLine("Reading value from the parallel port...");
            int value = PortControl.Input(portAddress);
            Console.WriteLine($"Value read from port: {value:X}"); 
        }

        private void btnOut_BtnClick(object sender, EventArgs e)
        {
            // Write a value to the parallel port
            Console.WriteLine("Writing value to the parallel port...");
            PortControl.Output(portAddress, portDecData); // Sending 0xFF (all bits high)
        }
    }
}
