using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ZCS_FormUI.Function
{
    public static class ImageFunc
    {
        /// <summary>
        /// 放大图片
        /// </summary>
        /// <param name="image"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Image ZoomImage(Image image, float scale)
        {
            if (image == null)
                throw new ArgumentNullException("image cannot be null");
            if (scale <= 0)
                throw new ArgumentException("scale must be more than zero");
            Bitmap bmp = new Bitmap((int)Math.Ceiling(image.Width * scale), (int)Math.Ceiling(image.Height * scale));
            BitmapData bmpDataNew = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            BitmapData bmpDataOld = ((Bitmap)image).LockBits(new Rectangle(Point.Empty, image.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            byte[] byColorNew = new byte[bmpDataNew.Height * bmpDataNew.Stride];
            byte[] byColorOld = new byte[bmpDataOld.Height * bmpDataOld.Stride];
            Marshal.Copy(bmpDataOld.Scan0, byColorOld, 0, byColorOld.Length);
            for (int x = 0, lenX = bmpDataNew.Width; x < lenX; x++)
            {
                int srcX = (int)(x / scale) << 2;
                for (int y = 0, lenY = bmpDataNew.Height; y < lenY; y++)
                {
                    int offsetOld = (int)(y / scale) * bmpDataOld.Stride + srcX;
                    int offsetNew = y * bmpDataNew.Stride + (x << 2);
                    byColorNew[offsetNew] = byColorOld[offsetOld];
                    byColorNew[offsetNew + 1] = byColorOld[offsetOld + 1];
                    byColorNew[offsetNew + 2] = byColorOld[offsetOld + 2];
                    byColorNew[offsetNew + 3] = byColorOld[offsetOld + 3];
                }
            }
       //Marshal.Copy(byColorOld, 0, bmpDataOld.Scan0, byColorOld.Length);
       ((Bitmap)image).UnlockBits(bmpDataOld);
            Marshal.Copy(byColorNew, 0, bmpDataNew.Scan0, byColorNew.Length);
            bmp.UnlockBits(bmpDataNew);
            return bmp;
        }
    }
}
