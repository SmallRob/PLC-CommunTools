using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ZCS_Common.Utilites.ImageUtilites;

namespace CommunTools.Common
{
    public class GdiPlusCommon
    {
        [DllImport("gdiplus.dll", ExactSpelling = true)]
        internal static extern int GdipCreateBitmapFromGdiDib(IntPtr bminfo, IntPtr pixdat, ref IntPtr image);

        [DllImport("gdiplus.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        internal static extern int GdipSaveImageToFile(IntPtr image, string filename, [In] ref Guid clsid, IntPtr encparams);

        [DllImport("gdiplus.dll", ExactSpelling = true)]
        internal static extern int GdipDisposeImage(IntPtr image);

        private static ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();      

        private static bool GetCodecClsid(string filename, out Guid clsid)
        {
            clsid = Guid.Empty;
            string ext = Path.GetExtension(filename);
            if (ext == null)
                return false;
            ext = "*" + ext.ToUpper();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FilenameExtension.IndexOf(ext) >= 0)
                {
                    clsid = codec.Clsid;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 保存扫描生成的图片到缓存表记录
        /// </summary>
        /// <param name="bminfo"></param>
        /// <param name="pixdat"></param>
        /// <param name="imgFolder">文件夹缓存路径</param>
        /// <param name="fileName">文件名称</param>
        /// <returns></returns>
        public bool SaveDIBAs(string scanFileName, IntPtr bminfo, IntPtr pixdat,  string imgFolder, string fileName)
        {          
            Guid clsid;
            if (!GetCodecClsid(scanFileName, out clsid))
            {
                throw new Exception($"文件扩展名设置不正确，未知的扩展名：{Path.GetExtension(scanFileName)}");
                return false;
            }

            if (File.Exists(scanFileName)) File.Delete(scanFileName);

            IntPtr img = IntPtr.Zero;
            int st = GdipCreateBitmapFromGdiDib(bminfo, pixdat, ref img);
            if ((st != 0) || (img == IntPtr.Zero))
                return false;

            st = GdipSaveImageToFile(img, scanFileName, ref clsid, IntPtr.Zero);
            GdipDisposeImage(img);

            if (File.Exists(scanFileName))
            {                
                if (!string.IsNullOrEmpty(imgFolder) && !Directory.Exists(imgFolder))
                {
                    Directory.CreateDirectory(imgFolder);
                }         
               
                using (Bitmap bmp = new Bitmap(scanFileName))             
                {                 
                    CompressionImage compessImage = new CompressionImage();
                    if (!compessImage.CompressAsJPG(bmp, "C:\\TempScan\\", 80))
                    {
                        //将待上传文件拷贝到缓存文件夹
                        File.Copy(scanFileName, "C:\\TempScan\\", true);
                    }
                   
                    bmp.Dispose();
                }
            }
            return st == 0;
        }
    }
}
