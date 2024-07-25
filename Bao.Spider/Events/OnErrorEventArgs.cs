using System;

namespace Bao.Spider.Events
{
    public class OnErrorEventArgs
    {
        /// <summary>
        /// 网站名
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// 要抓取的网页地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 异常
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }

        public OnErrorEventArgs(string website, string url, Exception exception)
        {
            this.Website = website;
            this.Url = url;
            this.Exception = exception;
        }

        public OnErrorEventArgs(string website, string url, string msg) : this(website, url, new Exception(msg))
        {
        }
    }
}
