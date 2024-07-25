using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO.Compression;
using System.IO;
using System.Drawing;

namespace Bao.Spider.Utils
{
    public class HttpHelper
    {
        #region fields
        static CookieContainer CookiesContainer { get; set; }//定义Cookie容器
        #endregion

        #region get 请求
        /// <summary>
        /// 异步创建爬虫
        /// </summary>
        /// <param name="uri">爬虫URL地址</param>
        /// <param name="proxy">代理服务器</param>
        /// <returns>网页源代码</returns>
        public static string Get(string uri, string proxy = null)
        {
            var html = string.Empty;
            var encoding = Encoding.UTF8;
            var charset = "utf-8";

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.Accept = "*/*";
                request.ServicePoint.Expect100Continue = false;//加快载入速度
                request.ServicePoint.UseNagleAlgorithm = false;//禁止Nagle算法加快载入速度
                request.AllowWriteStreamBuffering = false;//禁止缓冲加快载入速度
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");//定义gzip压缩页面支持
                request.ContentType = "application/x-www-form-urlencoded;charset=" + charset;//定义文档类型及编码
                request.AllowAutoRedirect = false;//禁止自动跳转
                                                  //设置User-Agent，伪装成Google Chrome浏览器
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
                request.Timeout = 5000;//定义请求超时时间为5秒
                request.KeepAlive = true;//启用长连接
                request.Method = "GET";//定义请求方式为GET              
                if (proxy != null) request.Proxy = new WebProxy(proxy);//设置代理服务器IP，伪装请求地址
                request.CookieContainer = CookiesContainer;//附加Cookie容器
                request.ServicePoint.ConnectionLimit = int.MaxValue;//定义最大连接数

                // 获取请求响应
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    // 301
                    //if (response.StatusCode == HttpStatusCode.Moved || response.StatusCode == HttpStatusCode.MovedPermanently)
                    //{
                    //    var new_url = response.Headers["Location"];
                    //    return Get(new_url);
                    //}

                    foreach (Cookie cookie in response.Cookies) CookiesContainer.Add(cookie);//将Cookie加入容器，保存登录状态

                    if (response.ContentEncoding.ToLower().Contains("gzip"))//解压
                    {
                        using (GZipStream stream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress))
                        {
                            using (StreamReader reader = new StreamReader(stream, encoding))
                            {
                                html = reader.ReadToEnd();
                            }
                        }
                    }
                    else if (response.ContentEncoding.ToLower().Contains("deflate"))//解压
                    {
                        using (DeflateStream stream = new DeflateStream(response.GetResponseStream(), CompressionMode.Decompress))
                        {
                            using (StreamReader reader = new StreamReader(stream, encoding))
                            {
                                html = reader.ReadToEnd();
                            }

                        }
                    }
                    else
                    {
                        using (Stream stream = response.GetResponseStream())//原始
                        {
                            using (StreamReader reader = new StreamReader(stream, encoding))
                            {

                                html = reader.ReadToEnd();
                            }
                        }
                    }


                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return "ERROR: \r\n" + html;
                    }
                }
                request.Abort();

            }
            catch (Exception ex)
            {

            }
            return html;
        }
        #endregion


        #region 保存web图片到本地
        /// <summary>
        /// 保存web图片到本地
        /// </summary>
        /// <param name="imgUrl">web图片路径</param>
        /// <param name="path">保存路径</param>
        /// <returns></returns>
        public static string SaveImageFromWeb(string imgUrl, string path)
        {
            if (path.Equals(""))
                throw new Exception("未指定保存文件的路径");

            var fullpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);

            if (!Directory.Exists(fullpath))
                Directory.CreateDirectory(fullpath);

            string fileName = DateTime.Now.ToString("yyMMddHHmmssfff") + new Random().Next(1000, 9999).ToString();

            string imgName = imgUrl.ToString().Substring(imgUrl.ToString().LastIndexOf("/") + 1);
            string defaultType = ".jpg";
            string[] imgTypes = new string[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            string imgType = imgUrl.ToString().Substring(imgUrl.ToString().LastIndexOf("."));
            string imgPath = "";
            foreach (string it in imgTypes)
            {
                if (imgType.ToLower().Equals(it))
                    break;
                if (it.Equals(".bmp"))
                    imgType = defaultType;
            }

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imgUrl);
                request.UserAgent = "Mozilla/6.0 (MSIE 6.0; Windows NT 5.1; Natas.Robot)";
                request.Timeout = 3000;

                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();

                if (response.ContentType.ToLower().StartsWith("image/"))
                {
                    byte[] arrayByte = new byte[1024];
                    int imgLong = (int)response.ContentLength;
                    int l = 0;

                    if (fileName == "")
                        fileName = imgName;

                    FileStream fs = new FileStream(fullpath + fileName + imgType, FileMode.Create);
                    while (l < imgLong)
                    {
                        int i = stream.Read(arrayByte, 0, 1024);
                        fs.Write(arrayByte, 0, i);
                        l += i;
                    }

                    fs.Close();
                    stream.Close();
                    response.Close();
                    imgPath = fileName + imgType;
                    return imgPath;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }
            finally
            {
                
            }
        }
        #endregion


        #region 从给定字符串中获取域名
        /// <summary>
        /// 从给定字符串中获取域名
        /// </summary>
        /// <param name="url"></param>
        /// <returns>如：http://product.pconline.com.cn</returns>
        public static string GetHost(string url)
        {
            Uri uri = new Uri(url);
            return uri.Scheme + "://" + uri.Host;
        }
        #endregion
    }
}
