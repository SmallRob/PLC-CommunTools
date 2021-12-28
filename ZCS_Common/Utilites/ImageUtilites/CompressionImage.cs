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
    public class CompressionImage
    {
        /// <summary>
        /// 获取指定mimeType的ImageCodecInfo
        /// </summary>
        /// <param name="mimeType"></param>
        /// <returns></returns>
        public static ImageCodecInfo GetImageCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }

        /// <summary>
        /// 获取inputStream中的Bitmap对象
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public static Bitmap GetBitmapFromStream(Stream inputStream)
        {
            Bitmap bitmap = new Bitmap(inputStream);
            return bitmap;
        }

        /// <summary>
        /// 将Bitmap对象压缩为JPG图片类型
        /// </summary>
        /// <param name="bmp">源bitmap对象</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="quality">压缩质量，越大照片越清晰，推荐80</param>
        public bool CompressAsJPG(Bitmap bmp, string saveFilePath, int quality)
        {
            bool flag = false;
            try
            {
                //如果小于预设的高度或宽度 则不压缩
                if (bmp.Size.Height <= 1024 || bmp.Size.Width <= 768)
                    quality = 100;
                else
                {
                    if (quality > 100 || quality <= 0) quality = 80;

                    long fileSize = ImageHelper.GetImageMemorySize((Image)bmp);
                    if (fileSize / 1024.00 > 600)
                    {
                        quality = 60;
                    }
                    else if (fileSize / 1024.00 < 60) quality = 100;
                }
                EncoderParameter p = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                EncoderParameters ps = new EncoderParameters(1);
                ps.Param[0] = p;

                string str = saveFilePath.Substring(saveFilePath.LastIndexOf("\\"));
                string filePath = saveFilePath.Substring(0, saveFilePath.Length - str.Length) + "\\";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                if (!File.Exists(saveFilePath))
                {
                    //每次new一个新的bmp对象并及时释放，避免出现占用错误
                    using (Bitmap bmpSave = new Bitmap(bmp))
                    {
                        //新建第二个bitmap类型的变量
                        Bitmap oImage = new Bitmap(bmpSave);

                        //将原来的图片流释放，将图片文件进行解锁。
                        bmpSave.Dispose();
                        oImage.Save(saveFilePath, GetImageCodecInfo("image/jpeg"), ps);

                        //释放bmp文件资源
                        oImage.Dispose();
                    }
                    flag = true;
                }
                else
                {
                    string tmpFileName = saveFilePath.Substring(0, saveFilePath.LastIndexOf('\\') + 1) + System.IO.Path.GetFileNameWithoutExtension(saveFilePath) + "_Tmp" + new Random().Next() + System.IO.Path.GetExtension(saveFilePath);

                    File.Move(saveFilePath, tmpFileName);
                    bmp.Save(saveFilePath, ImageFormat.Bmp);
                    File.Delete(tmpFileName);

                    flag = true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return flag;
        }

        /// <summary>
        /// 将inputStream中的对象压缩为JPG图片类型
        /// </summary>
        /// <param name="inputStream">源Stream对象</param>
        /// <param name="saveFilePath">目标图片的存储地址</param>
        /// <param name="quality">压缩质量，越大照片越清晰，推荐80</param>
        public void CompressAsJPG(Stream inputStream, string saveFilePath, int quality)
        {
            Bitmap bmp = GetBitmapFromStream(inputStream);
            CompressAsJPG(bmp, saveFilePath, quality);
        }
    }
}
