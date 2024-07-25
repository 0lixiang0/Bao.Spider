using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bao.Spider.Websites.PCOnline
{
    public class Utils
    {
        #region 从地址中获取分类名
        public static string getCategoryEname(string url)
        {
            // http://product.pconline.com.cn/mobile/
            if (url.EndsWith("/")) url = url.Substring(0, url.Length - 1);

            return url.Substring(url.LastIndexOf("/") + 1);
        }
        #endregion

        #region 从地址中获取产品ID
        public static string getId(string url)
        {
            // http://product.pconline.com.cn/mobile/samsung/357809.html

            //var id = url.Substring(url.LastIndexOf("/") + 1, url.LastIndexOf(".") - url.LastIndexOf("/") - 1);

            url =   url.Substring(url.LastIndexOf("/") + 1);
            var id = url.Substring(0, url.IndexOf("."));

            return id;
        }
        #endregion
    }
}
