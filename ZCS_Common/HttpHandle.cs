using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ZCS_Common
{
    public class HttpHandle
    {
        public static bool IsUrl(string strUrl)
        {
            string strRegex = @"(https?)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            if (strUrl != null && strUrl.Trim() != "")
            {
                Regex re = new Regex(strRegex);
                if (re.IsMatch(strUrl))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public static string BuildUrl(string url, string ParamText, string ParamValue)
        {
            Regex reg = new Regex(string.Format("{0}=[^&]*", ParamText), RegexOptions.IgnoreCase);
            Regex reg1 = new Regex("[&]{2,}", RegexOptions.IgnoreCase);
            string _url = reg.Replace(url, "");
            if (_url.IndexOf("?") == -1)
            {
                _url += string.Format("?{0}={1}", ParamText, ParamValue);//?  
            }
            else
            {
                _url += string.Format("&{0}={1}", ParamText, ParamValue);//&  
            }

            _url = reg1.Replace(_url, "&");
            _url = _url.Replace("?&", "?");
            return _url;
        }
        /// <summary>  
        /// Http同步Get同步请求  
        /// </summary>  
        /// <param name="url">Url地址</param>  
        /// <param name="encode">编码(默认UTF8)</param>  
        /// <returns></returns>  
        public static bool HttpGet(string url, out string result, Encoding encode = null)
        {
            result = string.Empty;

            try
            {
                WebClient webClient = new WebClient { Encoding = Encoding.UTF8 };

                if (encode != null)
                {
                    webClient.Encoding = encode;
                }

                result = webClient.DownloadString(url);
                return true;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }

        }

        /// <summary>  
        /// Http同步Get异步请求  
        /// </summary>  
        /// <param name="url">Url地址</param>  
        /// <param name="callBackDownStringCompleted">回调事件</param>  
        /// <param name="encode">编码(默认UTF8)</param>  
        public static void HttpGetAsync(string url,
            DownloadStringCompletedEventHandler callBackDownStringCompleted = null, Encoding encode = null)
        {
            WebClient webClient = new WebClient { Encoding = Encoding.UTF8 };

            if (encode != null)
            {
                webClient.Encoding = encode;
            }

            if (callBackDownStringCompleted != null)
            {
                webClient.DownloadStringCompleted += callBackDownStringCompleted;
            }

            webClient.DownloadStringAsync(new Uri(url));
        }

        /// <summary>  
        ///  Http同步Post同步请求  
        /// </summary>  
        /// <param name="url">Url地址</param>  
        /// <param name="postStr">请求Url数据</param>  
        /// <param name="encode">编码(默认UTF8)</param>  
        /// <returns></returns>  
        public static bool HttpPost(string url, string postStr, out string result, string ContentType = "application/json",
            Encoding encode = null, string token = null)
        {
            result = string.Empty;

            try
            {
                WebClient webClient = new WebClient { Encoding = Encoding.UTF8 };

                if (encode != null)
                {
                    webClient.Encoding = encode;
                }

                if (string.IsNullOrWhiteSpace(postStr))
                {
                    postStr = "";
                }

                byte[] sendData = Encoding.Default.GetBytes(postStr);

                webClient.Headers.Add("Content-Type", ContentType);
                webClient.Headers.Add("ContentLength", sendData.Length.ToString(CultureInfo.InvariantCulture));

                if (token != null)
                {
                    webClient.Headers.Add("Authorization", token);
                }

                byte[] readData = webClient.UploadData(url, "POST", sendData);

                result = Encoding.UTF8.GetString(readData);
                return true;
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return false;
            }
        }

        /// <summary>  
        /// Http同步Post异步请求  
        /// </summary>  
        /// <param name="url">Url地址</param>  
        /// <param name="postStr">请求Url数据</param>  
        /// <param name="callBackUploadDataCompleted">回调事件</param>  
        /// <param name="encode"></param>  
        public static void HttpPostAsync(string url, string postStr = "",
            UploadDataCompletedEventHandler callBackUploadDataCompleted = null, string ContentType = "application/json", Encoding encode = null)
        {
            WebClient webClient = new WebClient { Encoding = Encoding.UTF8 };

            if (encode != null)
            {
                webClient.Encoding = encode;
            }

            byte[] sendData = Encoding.GetEncoding("UTF8").GetBytes(postStr);

            webClient.Headers.Add("Content-Type", ContentType);
            webClient.Headers.Add("ContentLength", sendData.Length.ToString(CultureInfo.InvariantCulture));

            if (callBackUploadDataCompleted != null)
            {
                webClient.UploadDataCompleted += callBackUploadDataCompleted;
            }

            webClient.UploadDataAsync(new Uri(url), "POST", sendData);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="ContentType"></param>
        /// <returns></returns>
        public static string HttpPost(string Url, string postDataStr, string ContentType = "application/json;charset=UTF-8", string Accept = "*/*")
        {

            //强制垃圾回收
            System.GC.Collect();
            //设置最大并发连接数
            System.Net.ServicePointManager.DefaultConnectionLimit = 50;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.Timeout = 5 * 60 * 1000;
            request.ContentType = ContentType;// "application/x-www-form-urlencoded";
            request.Accept = Accept;// "*/*";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //request.CookieContainer = cookie;
            using (Stream myRequestStream = request.GetRequestStream())
            {

                byte[] bs = Encoding.UTF8.GetBytes(postDataStr);
                myRequestStream.Write(bs, 0, bs.Length);
                myRequestStream.Flush();
                myRequestStream.Close();
            }
            string retString = String.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //response.Cookies = cookie.GetCookies(response.ResponseUri); 
                using (StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                }
                response.Close();
            }
            //回收资源
            if (request != null)
            {
                request.Abort();
                request = null;
            }
            return retString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="ContentType"></param>
        /// <returns></returns>
        public static string HttpGet(string Url, string postDataStr, string ContentType = "application/json;charset=UTF-8", string Accept = "*/*")
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = ContentType;// "text/html;charset=UTF-8";
            request.Accept = Accept;// "*/*";

            request.AllowWriteStreamBuffering = false;
            //访问权限验证
            //request.Headers.Add("Authorization:" + HttpRestAuthorization.GetAuthorization());

            request.Proxy = null;
            request.Timeout = 5 * 60 * 1000;
            request.KeepAlive = false;

            String retString = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    retString = sr.ReadToEnd();
                }
            }
            //回收
            if (request != null)
            {
                request.Abort();
                request = null;
            }
            return retString;
        }

        #region 获取和下载文件
        /// <summary>
        /// http下载文件
        /// </summary>
        /// <param name="url">下载文件地址</param>
        /// <param name="path">文件存放地址，包含文件名</param>
        /// <returns></returns>
        public static bool HttpDownload(string url, string path, out string errorMsg)
        {
            string tempPath = System.IO.Path.GetDirectoryName(path) + @"\temp";
            System.IO.Directory.CreateDirectory(tempPath);  //创建临时文件目录
            string tempFile = tempPath + @"\" + System.IO.Path.GetFileName(path) + ".temp"; //临时文件
            if (System.IO.File.Exists(tempFile))
            {
                System.IO.File.Delete(tempFile);    //存在则删除
            }

            errorMsg = string.Empty;
            try
            {
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                request.Method = "GET";
                request.Timeout = 600000;

                //解决大文件超时问题
                request.UseDefaultCredentials = true;

                request.ServicePoint.Expect100Continue = false;
                //是否使用 Nagle 不使用 提高效率 
                request.ServicePoint.UseNagleAlgorithm = false;
                //最大连接数 
                request.ServicePoint.ConnectionLimit = 65500;
                //数据是否缓冲 false 提高效率  
                request.AllowWriteStreamBuffering = false;
                //设置最大并发连接数
                System.Net.ServicePointManager.DefaultConnectionLimit = 50;

                request.Accept = "*/*";
                request.KeepAlive = true;
                request.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");

                request.Proxy = null;
                request.AllowAutoRedirect = false;

                //发送请求并获取相应回应数据
                //HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                using (WebResponse resq = request.GetResponse())
                {
                    /*
                    StreamReader responseStreamReader = new StreamReader(resq.GetResponseStream(), Encoding.UTF8);
                    int length = (int)resq.ContentLength;
                    byte[] bs = new byte[length];
                    */

                    HttpWebResponse response = resq as HttpWebResponse;

                    //直到request.GetResponse()程序才开始向目标网页发送Post请求
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        //创建本地文件写入流
                        //Stream stream = new FileStream(tempFile, FileMode.Create);
                        using (Stream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                        {
                            byte[] bArr = new byte[1024];
                            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                            while (size > 0)
                            {
                                if (fs.CanWrite)
                                {
                                    fs.Write(bArr, 0, size);
                                }
                                size = responseStream.Read(bArr, 0, (int)bArr.Length);
                            }
                            //stream.Close();
                            fs.Close();
                        }
                        responseStream.Close();
                    }

                    if (response != null)
                    {
                        response.Close();
                        response = null;
                    }
                    if (request != null)
                    {
                        request.Abort();
                        request = null;
                    }
                }
                System.IO.File.Move(tempFile, path);
                return true;
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 获得网络文件是否存在方法
        /// </summary>
        /// <param name="url">文件url</param>
        /// <returns></returns>
        public static bool JudgeFileExist(string url)
        {
            try
            {
                //创建根据网络地址的请求对象
                System.Net.HttpWebRequest httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.CreateDefault(new Uri(url));
                httpWebRequest.Method = "HEAD";
                httpWebRequest.Timeout = 1000;
                //返回响应状态是否是成功比较的布尔值
                return (((System.Net.HttpWebResponse)httpWebRequest.GetResponse()).StatusCode == System.Net.HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获得Http路径下文件大小
        /// </summary>
        /// <param name="fileUrl">当前文件Url</param>
        /// <returns></returns>
        public static long GetHttpFileSize(string fileUrl)
        {
            HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(fileUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            long sumLength = response.ContentLength;//获得文件的总大小
            return sumLength;
        }
        #endregion
    }
}
