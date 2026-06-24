using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace CommunTools.Common.Twain
{

    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public partial class TwainScanCommon : IMessageFilter
    {
        public TwRC rc;
        public IntPtr Handle;

        /// <summary>
        /// 文件名字
        /// </summary>
        public string fileName = string.Empty;

        /// <summary>
        /// 扫描文件路径
        /// </summary>
        private string ScanPath = "";

        /// <summary>
        /// 扫描缓存文件夹路径
        /// </summary>
        private string ImgFolder = "";

        internal string strScanErrorMessage = "";//扫描时错误信息
        internal bool bContinuousScan = false;     //是否连续扫描

        #region 定义扫描相关属性
        private Twain tw;
        private bool msgfilter = false;
        private int picnumber = 0;
        BITMAPINFOHEADER bmi;
        Rectangle bmprect;
        IntPtr dibhand;
        IntPtr bmpptr;
        IntPtr pixptr;
        #endregion


        // 添加一个委托 
        public delegate void PassDataBetweenFormHandler(object sender, PassDataEventArgs e);
        // 添加一个PassDataBetweenFormHandler 类型的事件 
        public event PassDataBetweenFormHandler PassDataBetweenForm;

        public TwainScanCommon(string fileName, IntPtr Handle)
        {
            this.fileName = fileName;
            this.Handle = Handle;

            //扫描初始化（默认使用TWAIN方式进行连接）
            tw = new Twain();
            tw.Init(Handle, out rc);
            //tw.Select();
        }

        public void StartScan()
        {
            //调试时启用此代码，打开设备选择窗口
            //tw.Select();

            if (rc != TwRC.Success)
            {
                MessageBox.Show("设备初始化失败，请检查硬件及驱动！");
            }
            else
            {
                string path = Application.StartupPath + "\\ScannerFile\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (Scanning(path) == false)
                {
                    if (strScanErrorMessage != "")
                    {
                        //手动选择扫描仪
                        tw.Select();
                        if (Scanning(path) == false)
                        {
                            PassDataEventArgs frmArgs = new PassDataEventArgs("exit");
                            PassDataBetweenForm(this, frmArgs);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("没有发现支持TWAIN组件的设备！");
                    }
                    PassDataEventArgs args = new PassDataEventArgs("exit");
                    PassDataBetweenForm(this, args);
                    GC.Collect();
                }
            }
        }

        #region 扫描的一些方法
        private bool Scanning(string filePath)
        {
            if (!msgfilter)
            {
                msgfilter = true;
                Application.AddMessageFilter(this);
            }
            ScanPath = filePath;
            ImgFolder = "C:\\ScanImages\\"; //此处可以自定义扫描文件保存文件夹

            return tw.Acquire(ref strScanErrorMessage, bContinuousScan);
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="dibhandp"></param>
        private void ImageSave(IntPtr dibhandp)
        {
            bmprect = new Rectangle(0, 0, 0, 0);
            if (dibhandp != IntPtr.Zero) dibhand = dibhandp;
            bmpptr = GlobalLock(dibhand);
            pixptr = GetPixelInfo(bmpptr);
        }

        public void EndingScan()
        {
            if (msgfilter)
            {
                tw.Finish();
                RemoveMessageFilter(this);
                msgfilter = false;
            }
        }

        public static void RemoveMessageFilter(IMessageFilter value)
        {
            Application.RemoveMessageFilter(value);
        }
        #endregion

        #region 扫描的具体操作
        public bool PreFilterMessage(ref Message m)
        {
            try
            {
                TwainCommand cmd;
                cmd = tw.PassMessage(ref m);
                if (cmd == TwainCommand.Not)
                {
                    return false;
                }
                PassMSG(cmd);
            }
            catch (Exception ex)
            {
                strScanErrorMessage = ex.Message;
                return true;
            }
            return false;
        }

        public void PassMSG(TwainCommand cmd)
        {
            switch (cmd)
            {
                case TwainCommand.CloseRequest:
                    {
                        strScanErrorMessage = "用户取消扫描";
                        EndingScan();
                        tw.CloseSrc();
                        PassDataEventArgs args = new PassDataEventArgs("exit");
                        PassDataBetweenForm(this, args);
                        GC.Collect();
                        break;
                    }
                case TwainCommand.CloseOk:
                    {
                        strScanErrorMessage = "用户取消扫描";
                        EndingScan();
                        tw.CloseSrc();
                        PassDataEventArgs args = new PassDataEventArgs("exit");
                        PassDataBetweenForm(this, args);
                        GC.Collect();
                        break;
                    }
                case TwainCommand.DeviceEvent:
                    {
                        strScanErrorMessage = "用户取消扫描";
                        GC.Collect();
                        break;
                    }
                case TwainCommand.TransferReady:
                    {
                        ArrayList pics = tw.TransferPictures(ref strScanErrorMessage);
                        picnumber++;
                        if (pics.Count > 0)
                        {
                            for (int i = 0; i < pics.Count; i++)
                            {
                                IntPtr img = (IntPtr)pics[i];
                                ImageSave(img);

                                try
                                {
                                    GdiPlusCommon dip = new GdiPlusCommon();
                                    int picnum = i + 1;
                                    string scanFileName = ScanPath + "\\" + DateTime.Now.ToString("yyyyMMdd") + picnumber.ToString()
                                                + "_" + picnum.ToString() + ".png";
                                    dip.SaveDIBAs(scanFileName, bmpptr, pixptr,  ImgFolder, fileName);
                                }
                                catch (Exception ex)
                                {
                                    strScanErrorMessage = ex.Message;
                                    continue;
                                }
                            }

                            //单张继续扫描
                            if (!bContinuousScan)
                            {
                                if (!tw.MultiScan(Handle, ref strScanErrorMessage))
                                {
                                    PassDataEventArgs args = new PassDataEventArgs("exit");
                                    PassDataBetweenForm(this, args);
                                    //EndingScan();
                                    //tw.CloseSrc();
                                }
                            }
                            else
                            {
                                //否则结束连续扫描
                                PassDataEventArgs args = new PassDataEventArgs("exit");
                                PassDataBetweenForm(this, args);
                                pics.Clear();
                                EndingScan();
                                tw.CloseSrc();
                                GC.Collect();
                            }
                        }
                        else
                        {
                            EndingScan();
                            tw.CloseSrc();
                            PassDataEventArgs args = new PassDataEventArgs("exit");
                            PassDataBetweenForm(this, args);
                            GC.Collect();
                        }
                        break;
                    }
            }
        }

        [DllImport("kerne32.dll")]
        private static extern void CloseHandle(IntPtr hdl);

        [StructLayout(LayoutKind.Sequential, Pack = 2)]
        internal class BITMAPINFOHEADER
        {
            public int biSize;
            public int biWidth;
            public int biHeight;
            public short biPlanes;
            public short biBitCount;
            public int biCompression;
            public int biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public int biClrUsed;
            public int biClrImportant;
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);

        protected IntPtr GetPixelInfo(IntPtr bmpptr)
        {
            bmi = new BITMAPINFOHEADER();
            Marshal.PtrToStructure(bmpptr, bmi);

            bmprect.X = bmprect.Y = 0;
            bmprect.Width = bmi.biWidth;
            bmprect.Height = bmi.biHeight;

            if (bmi.biSizeImage == 0)
                bmi.biSizeImage = ((((bmi.biWidth * bmi.biBitCount) + 31) & ~31) >> 3) * bmi.biHeight;

            int p = bmi.biClrUsed;
            if ((p == 0) && (bmi.biBitCount <= 8))
                p = 1 << bmi.biBitCount;
            p = (p * 4) + bmi.biSize + (int)bmpptr;
            return (IntPtr)p;
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        #endregion

    }
}
