using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCS_Common
{
    public static class LogHelper
    {
        private static readonly object writeFile = new object();
        private static StreamWriter streamWriter; //写文件  

        /// <summary>
        /// 在本地写入错误日志
        /// </summary>
        /// <param name="exception"></param> 错误信息
        public static void WriteException(Exception exception)
        {
            using (TimedLock.Lock(writeFile))
            {
                WriteLog(exception, string.Empty);
            }
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="info"></param>
        /// <param name="fileDirectory"></param>
        /// <param name="fileName"></param>
        public static void WriteInfoLog(string info, string fileDirectory, string fileName = "log")
        {
            //using (Common.TimedLock.Lock(writeFile))
            lock (writeFile)
            {
                try
                {
                    DateTime dt = DateTime.Now;
                    string directPath = string.Format(@"{0}\" + fileDirectory + "", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);

                    //记录错误日志文件的路径
                    if (!Directory.Exists(directPath))
                    {
                        Directory.CreateDirectory(directPath);
                    }
                    directPath += string.Format(@"\{0}_{1}.log", fileName, dt.ToString("yyyy-MM-dd"));
                    if (streamWriter == null)
                    {
                        InitLog(directPath);
                    }
                    streamWriter.WriteLine("***********************************************************************");
                    streamWriter.WriteLine(dt.ToString("HH:mm:ss"));
                    streamWriter.WriteLine("输出信息：");
                    streamWriter.WriteLine(info);
                }
                catch (Exception e)
                {

                }
                finally
                {
                    if (streamWriter != null)
                    {
                        streamWriter.Flush();
                        streamWriter.Dispose();
                        streamWriter = null;
                    }
                }
            }
        }

        /// <summary>
        /// 在本地写入错误日志
        /// </summary>
        /// <param name="exception">错误信息</param> 
        public static void WriteException(Exception exception, string subName)
        {
            using (TimedLock.Lock(writeFile))
            {
                WriteLog(exception, subName);
            }
        }


        /// <summary>
        /// 在本地写入错误日志
        /// </summary>
        /// <param name="exception">错误信息</param>
        /// <param name="subName">方法名称</param>
        public static void WriteLog(Exception exception, string subName)
        {
            lock (writeFile)
            {
                try
                {
                    DateTime dt = DateTime.Now;
                    string directPath = string.Format(@"{0}\ErrorLog", AppDomain.CurrentDomain.SetupInformation.ApplicationBase);
                    //记录错误日志文件的路径
                    if (!Directory.Exists(directPath))
                    {
                        Directory.CreateDirectory(directPath);
                    }
                    directPath += string.Format(@"\{0}.log", dt.ToString("yyyy-MM-dd"));
                    if (streamWriter == null)
                    {
                        InitLog(directPath);
                    }
                    streamWriter.WriteLine("***********************************************************************");
                    streamWriter.WriteLine(dt.ToString("HH:mm:ss"));
                    streamWriter.WriteLine("输出信息：错误信息");
                    if (!string.IsNullOrEmpty(subName))
                    {
                        streamWriter.WriteLine("调用方法：" + subName);
                    }
                    if (exception != null)
                    {
                        OutputInnerException(exception, streamWriter);
                    }
                }
                finally
                {
                    if (streamWriter != null)
                    {
                        streamWriter.Flush();
                        streamWriter.Dispose();
                        streamWriter = null;
                    }
                }
            }
        }

        private static void OutputInnerException(Exception e, StreamWriter streamWriter)
        {
            streamWriter.WriteLine("----------------------------");
            streamWriter.WriteLine("异常信息：\r\n" + e.Message);
            streamWriter.WriteLine("Source：\r\n" + e.Source);
            streamWriter.WriteLine("调用堆栈:\r\n" + e.StackTrace);
            if (e.InnerException != null)
            {
                OutputInnerException(e.InnerException, streamWriter);
            }
        }
        private static void InitLog(string filPath)
        {
            streamWriter = !File.Exists(filPath) ? File.CreateText(filPath) : File.AppendText(filPath);
        }
    }
}
