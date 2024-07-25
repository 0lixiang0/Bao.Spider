using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Bao.Spider.Utils
{
    public class HtmlHelper
    {
        #region 根据地址，抓取html内容，并转化成HtmlNode对象
        public static HtmlNode CrawlAndConvert(string url)
        {
            var html = HttpHelper.Get(url);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            HtmlNode node = doc.DocumentNode;

            return node;
        }
        #endregion
    }
}
