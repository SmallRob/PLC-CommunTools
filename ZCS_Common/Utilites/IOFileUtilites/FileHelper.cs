using System;
using System.IO;
using System.Text;

namespace ZCS_Common
{
    public static class FileHelper
    {
        #region 文件读写
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="code"></param>
        /// <param name="stringTempCode"></param>
        public static void WriteFile(string filePath, Encoding code, string stringTempCode)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(filePath, true, code);
                sw.WriteLine(stringTempCode);
                sw.Flush();
            }
            catch (Exception)
            {

            }
            finally
            {
                sw.Close();
            }
        }

        public static string ReadFile(string filePath, Encoding code)
        {
            StreamReader sr = null;

            string stringTempCode = "";
            try
            {
                // 读取模板文件
                sr = new StreamReader(filePath, code);
                stringTempCode = sr.ReadToEnd();

                return stringTempCode;
            }
            catch (Exception)
            {
                sr.Close();
                return null;
            }
            finally
            {
                sr.Close();
            }
        }
        #endregion

        /// <summary>
        /// 验证文件名
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool ValidateFileName(string fileName)
        {
            return fileName.LastIndexOfAny(Path.GetInvalidFileNameChars()) < 0;
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool TryCreateFile(string filePath, out string message)
        {
            #region Check
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
            #endregion
            try
            {
                if (File.Exists(filePath))
                {
                    message = null;
                    return true;
                }

                using (FileStream _stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                }
            }
            catch (Exception exc)
            {
                message = exc.Message;
                return false;
            }

            message = null;
            return true;
        }

        #region 目录及文件删除
        /// <summary>
        /// 删除子目录及下边文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="DirectoryName"></param>
        public static void DeleteDirectory(string path, string DirectoryName)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                if (dir.Exists)
                {
                    DirectoryInfo[] childs = dir.GetDirectories();
                    foreach (DirectoryInfo child in childs)
                    {
                        if (child.Name == DirectoryName)
                        {
                            child.Delete(true);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 删除目录下文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="DirectoryName"></param>
        public static void DeleteFile(string path, string FileName)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                FileInfo[] childs = dir.GetFiles();
                foreach (FileInfo fi in childs)
                {
                    if (fi.Name == FileName)
                    {
                        fi.Delete();
                    }
                }
            }
            GC.Collect();
        }
        #endregion
    }
}
