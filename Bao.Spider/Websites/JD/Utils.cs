using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bao.Spider.Websites.JD
{
    public class Utils
    {
        #region 补全分类地址
        public static string CompletionCategoryUrl(string s)
        {
            if (s.Contains(".jd.com"))
            {
                if (!s.StartsWith("https://") || !s.StartsWith("http://"))
                    s = $"https://" + s;
                return s;
            }

            var host = "https://list.jd.com/list.html?cat=";

            s = s.Replace("-", ",");
            return host + s;
        }
        #endregion

        #region 从地址中获取 cat 值
        public static string GetCat(string s)
        {
            // list.jd.com/list.html?cat=737,794,870&ev=1554_584893&JL=3_空调类别_壁挂式空调#J_crumbsBar
            if (s.Contains(".jd.com"))
            {
                if (!s.StartsWith("https://") || !s.StartsWith("http://"))
                    s = $"https://" + s;

                Uri uri = new Uri(s);
                var query = uri.Query;

                if (query == "") return "";

                query = query.Substring(1);
                var queryArr = query.Split('&');
                foreach (var q in queryArr)
                {
                    var kv = q.Split('=');
                    if (kv[0] != "cat" && kv[0] != "sub" && kv[0] != "tid") continue;

                    return kv[1];
                }

                return "";
            }
            s = s.Replace("-", ",");
            return s;
        }
        #endregion
    }
}
