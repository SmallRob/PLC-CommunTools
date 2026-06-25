using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CommunTools.Common.Twain
{
    public enum TwainCommand
    {
        Not = -1,
        Null = 0,
        TransferReady = 1,
        CloseRequest = 2,
        CloseOk = 3,
        DeviceEvent = 4
    }

    public class Twain
    {
        private const short CountryUSA = 1;
        private const short LanguageUSA = 13;

        public Twain()
        {
            appid = new TwIdentity();
            appid.Id = IntPtr.Zero;
            appid.Version.MajorNum = 1;
            appid.Version.MinorNum = 1;
            appid.Version.Language = LanguageUSA;
            appid.Version.Country = CountryUSA;
            appid.Version.Info = "Hack 1";
            appid.ProtocolMajor = TwProtocol.Major;
            appid.ProtocolMinor = TwProtocol.Minor;
            appid.SupportedGroups = (int)(TwDG.Image | TwDG.Control);
            appid.Manufacturer = "NETMaster";
            appid.ProductFamily = "Freeware";
            appid.ProductName = "Hack";

            srcds = new TwIdentity();
            srcds.Id = IntPtr.Zero;

            evtmsg.EventPtr = Marshal.AllocHGlobal(Marshal.SizeOf(winmsg));
        }

        ~Twain()
        {
            Marshal.FreeHGlobal(evtmsg.EventPtr);
        }

        /// <summary>
        /// 扫描初始化
        /// </summary>
        /// <param name="hwndp"></param>
        /// <param name="rcResult"></param>
        public void Init(IntPtr hwndp, out TwRC rcResult)
        {
            Finish();
            TwRC rc = DSMparent(appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.OpenDSM, ref hwndp);
            if (rc == TwRC.Success)
            {
                rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetDefault, srcds);
                if (rc == TwRC.Success)
                    hwnd = hwndp;
                else
                    rc = DSMparent(appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref hwndp);
            }
            rcResult = rc;
        }

        /// <summary>
        /// 选择扫描设备
        /// </summary>
        /// <returns></returns>
        public bool Select()
        {
            TwRC rc;
            CloseSrc();
            if (appid.Id == IntPtr.Zero)
            {
                Init(hwnd, out rc);
                if (appid.Id == IntPtr.Zero)
                    return false;
            }
            rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.UserSelect, srcds);
            return true;
        }

        /// <summary>
        /// 启动扫描
        /// </summary>
        /// <param name="strScanErrorMessage"></param>
        /// <returns></returns>        
        public bool Acquire(ref string strScanErrorMessage, bool MultiScan)
        {
            try
            {
                TwRC rc;
                CloseSrc();
                if (appid.Id == IntPtr.Zero)
                {
                    Init(hwnd, out rc);
                    if (appid.Id == IntPtr.Zero)
                        strScanErrorMessage = "未找到扫描仪设备，请查看是否安装扫描仪驱动！";
                    return false;
                }
                rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.OpenDS, srcds);
                if (rc != TwRC.Success)
                {
                    strScanErrorMessage = "未找到扫描仪设备";
                    return false;
                }

                if (MultiScan)
                {
                    //如果是连续扫描
                    TwCapability cap = new TwCapability(TwCap.XferCount, -1);
                    rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
                }
                else
                {
                    //否则只扫描一张
                    TwCapability cap = new TwCapability(TwCap.XferCount, 1);
                    rc = DScap(appid, srcds, TwDG.Control, TwDAT.Capability, TwMSG.Set, cap);
                }

                if (rc != TwRC.Success)
                {
                    strScanErrorMessage = "未找到扫描仪设备";
                    CloseSrc();
                    return false;
                }

                TwUserInterface guif = new TwUserInterface();
                guif.ShowUI = 1;
                guif.ModalUI = 1;
                guif.ParentHand = hwnd;
                rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif);
                if (rc != TwRC.Success)
                {
                    strScanErrorMessage = "扫描仪中没有纸";
                    CloseSrc();
                    GC.Collect();
                    return false;
                }
            }
            catch (Exception e)
            {
                strScanErrorMessage = e.Message;
                GC.Collect();
                return false;
            }
            //GC.Collect();
            return true;
        }

        /// <summary>
        /// 单张连续扫描
        /// </summary>
        /// <param name="hwndp"></param>
        /// <param name="strScanErrorMessage"></param>
        /// <returns></returns>
        public bool MultiScan(IntPtr hwndp, ref string strScanErrorMessage)
        {
            try
            {

                TwRC rc;
                Init(hwndp, out rc);

                //rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.GetDefault, srcds);
                //if (rc == TwRC.Success)
                //    hwnd = hwndp;
                //else
                //    rc = DSMparent(appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref hwndp);

                if (rc != TwRC.Success)
                {
                    strScanErrorMessage = "设备连接中断";
                    return false;
                }
                else
                {
                    Acquire(ref strScanErrorMessage, false);
                    return true;
                }

                /*
                TwUserInterface guif = new TwUserInterface();
                //连续扫描，则不显示扫描设置窗口，guif.ShowUI = 0；如果不是连续扫描，则显示扫描设置窗口，guif.ShowUI = 1；
                guif.ShowUI = 0;
                guif.ModalUI = 1;
                guif.ParentHand = hwnd;

                TwRC rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.EnableDS, guif);

                if (rc != TwRC.Success)
                {
                    strScanErrorMessage = "扫描仪中没有纸";
                    //CloseSrc();
                    return false;
                }
                */
            }
            catch (Exception e)
            {
                strScanErrorMessage = e.Message;
                return false;
            }
            //return true;
        }

        /// <summary>
        /// 获取扫描图片
        /// </summary>
        public ArrayList TransferPictures(ref string strScanErrorMessage)
        {
            ArrayList pics = new ArrayList();
            if (srcds.Id == IntPtr.Zero)
                return pics;

            TwRC rc;
            IntPtr hbitmap = IntPtr.Zero;
            TwPendingXfers pxfr = new TwPendingXfers();

            do
            {
                pxfr.Count = 0;
                hbitmap = IntPtr.Zero;

                TwImageInfo iinf = new TwImageInfo();
                rc = DSiinf(appid, srcds, TwDG.Image, TwDAT.ImageInfo, TwMSG.Get, iinf);
                if (rc != TwRC.Success)
                {
                    CloseSrc();
                    return pics;
                }

                rc = DSixfer(appid, srcds, TwDG.Image, TwDAT.ImageNativeXfer, TwMSG.Get, ref hbitmap);
                if (rc != TwRC.XferDone)
                {
                    CloseSrc();
                    return pics;
                }

                rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.EndXfer, pxfr);
                if (rc != TwRC.Success)
                {
                    CloseSrc();
                    return pics;
                }

                pics.Add(hbitmap);
            }
            while (pxfr.Count != 0);

            rc = DSpxfer(appid, srcds, TwDG.Control, TwDAT.PendingXfers, TwMSG.Reset, pxfr);
            return pics;
        }

        /// <summary>
        /// 监听消息
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        TwRC rc;
        public TwainCommand PassMessage(ref Message m)
        {
            if (srcds.Id == IntPtr.Zero)
                return TwainCommand.Not;

            int pos = GetMessagePos();

            winmsg.hwnd = m.HWnd;
            winmsg.message = m.Msg;
            winmsg.wParam = m.WParam;
            winmsg.lParam = m.LParam;
            winmsg.time = GetMessageTime();
            winmsg.x = (short)pos;
            winmsg.y = (short)(pos >> 16);

            Marshal.StructureToPtr(winmsg, evtmsg.EventPtr, false);
            evtmsg.Message = 0;
            rc = DSevent(appid, srcds, TwDG.Control, TwDAT.Event, TwMSG.ProcessEvent, ref evtmsg);
            if (rc == TwRC.NotDSEvent)
                return TwainCommand.Not;
            if (evtmsg.Message == (short)TwMSG.XFerReady)
                return TwainCommand.TransferReady;
            if (evtmsg.Message == (short)TwMSG.CloseDSReq)
                return TwainCommand.CloseRequest;
            if (evtmsg.Message == (short)TwMSG.CloseDSOK)
                return TwainCommand.CloseOk;
            if (evtmsg.Message == (short)TwMSG.DeviceEvent)
                return TwainCommand.DeviceEvent;

            return TwainCommand.Null;
        }

        /// <summary>
        /// 关闭扫描源
        /// </summary>
        public void CloseSrc()
        {
            TwRC rc;
            if (srcds.Id != IntPtr.Zero)
            {
                try
                {
                    TwUserInterface guif = new TwUserInterface();
                    rc = DSuserif(appid, srcds, TwDG.Control, TwDAT.UserInterface, TwMSG.DisableDS, guif);
                }
                catch { }
                try
                {
                    rc = DSMident(appid, IntPtr.Zero, TwDG.Control, TwDAT.Identity, TwMSG.CloseDS, srcds);
                }
                catch { }
            }
        }

        /// <summary>
        /// 扫描完成
        /// </summary>
        public void Finish()
        {
            TwRC rc;
            CloseSrc();
            if (appid.Id != IntPtr.Zero)
                rc = DSMparent(appid, IntPtr.Zero, TwDG.Control, TwDAT.Parent, TwMSG.CloseDSM, ref hwnd);
            appid.Id = IntPtr.Zero;
        }

        #region 组件引用

        private IntPtr hwnd;
        private TwIdentity appid;
        private TwIdentity srcds;
        private TwEvent evtmsg;
        private WINMSG winmsg;

        // ------ DSM entry point DAT_ variants:
        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSMparent([In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr refptr);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSMident([In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwIdentity idds);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSMstatus([In, Out] TwIdentity origin, IntPtr zeroptr, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat);


        // ------ DSM entry point DAT_ variants to DS:
        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSuserif([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, TwUserInterface guif);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSevent([In, Out] TwIdentity origin, [In, Out] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref TwEvent evt);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSstatus([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwStatus dsmstat);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DScap([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwCapability capa);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSiinf([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwImageInfo imginf);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSixfer([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, ref IntPtr hbitmap);

        [DllImport("twain_32.dll", EntryPoint = "#1")]
        private static extern TwRC DSpxfer([In, Out] TwIdentity origin, [In] TwIdentity dest, TwDG dg, TwDAT dat, TwMSG msg, [In, Out] TwPendingXfers pxfr);


        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalAlloc(int flags, int size);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern bool GlobalUnlock(IntPtr handle);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalFree(IntPtr handle);

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern int GetMessagePos();
        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern int GetMessageTime();


        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreateDC(string szdriver, string szdevice, string szoutput, IntPtr devmode);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        private static extern bool DeleteDC(IntPtr hdc);

        public static int ScreenBitDepth
        {
            get
            {
                IntPtr screenDC = CreateDC("DISPLAY", null, null, IntPtr.Zero);
                int bitDepth = GetDeviceCaps(screenDC, 12);
                bitDepth *= GetDeviceCaps(screenDC, 14);
                DeleteDC(screenDC);
                return bitDepth;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        internal struct WINMSG
        {
            public IntPtr hwnd;
            public int message;
            public IntPtr wParam;
            public IntPtr lParam;
            public int time;
            public int x;
            public int y;
        }
        #endregion
    }
}
