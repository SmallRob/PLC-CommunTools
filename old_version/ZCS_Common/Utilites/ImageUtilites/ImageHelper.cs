using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCS_Common.Utilites.ImageUtilites
{
    public static class ImageHelper
    {
        /// <summary>
        /// 获取图片的内存字节大小
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public static long GetImageMemorySize(Image image)
        {
            long jpegByteSize;
            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                jpegByteSize = ms.Length;
            }
            return jpegByteSize;
        }

    }
}
