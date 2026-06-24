using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CommunTools.Common.Twain
{
    public class GetGraphics
    {
        private Graphics _g = null;
        private Bitmap _bitmap = null;
        public GetGraphics(IntPtr dibhandp)
        {

            bmprect = new Rectangle(0, 0, 0, 0);
            dibhand = dibhandp;
            bmpptr = GlobalLock(dibhand);
            pixptr = GetPixelInfo(bmpptr);
            this._bitmap = new Bitmap(bmprect.Width, bmprect.Height);
            this._g = Graphics.FromImage(this._bitmap);
        }

        /// <summary>
        /// 顺时针旋转90度
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Bitmap KiRotate90(Bitmap img)
        {
            try
            {
                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                //顺时针旋转90度     RotateFlipType.Rotate90FlipNone
                //逆时针旋转90度     RotateFlipType.Rotate270FlipNone
                //水平翻转    RotateFlipType.Rotate180FlipY
                //垂直翻转    RotateFlipType.Rotate180FlipX 
                return img;
            }
            catch
            {
                return null;
            }
        }

        public Bitmap GetImage()
        {
            IntPtr hdc = this._g.GetHdc();
            SetDIBitsToDevice(hdc, 0, 0, bmprect.Width, bmprect.Height,
                    0, 0, 0, bmprect.Height, pixptr, bmpptr, 0);

            this._g.ReleaseHdc(hdc);

            Pen p = new Pen(Color.Red, 10);
            Bitmap newbitmip = new Bitmap(this._bitmap.Width, this._bitmap.Height);
            Graphics g = Graphics.FromImage(newbitmip);
            g.DrawImage(this._bitmap, 0, 0, this._bitmap.Width, this._bitmap.Height);
            //g.DrawLine(p, 0, 0, 20, 20);

            return KiRotate90(newbitmip);
        }

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


        BITMAPINFOHEADER bmi;
        Rectangle bmprect;
        IntPtr dibhand;
        IntPtr bmpptr;
        IntPtr pixptr;

        [DllImport("gdi32.dll", ExactSpelling = true)]
        internal static extern int SetDIBitsToDevice(IntPtr hdc, int xdst, int ydst,
                                                int width, int height, int xsrc, int ysrc, int start, int lines,
                                                IntPtr bitsptr, IntPtr bmiptr, int color);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalFree(IntPtr handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string outstr);

    }
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
}
