using Bao.Spider.Events;
using Bao.Spider.Utils;
using HtmlAgilityPack;
using Jil;

namespace Bao.Spider.Websites.JD
{
    public class CollSub : CrawlBase
    {
        #region 抓取品牌
        public override void getBrand(string origid, int cid)
        {
            /*
             ** 品牌地址：**
             这种页面（https://coll.jd.com/list.html?sub=1661）的品牌地址：
             https://fhs.jd.com/api/getattrinfo?sub=1661&md=1&fhc=pc&callback=jQuery6227368&my=list_brand
            */

            var url = $"https://fhs.jd.com/api/getattrinfo?sub={origid}&md=1&fhc=pc&callback=jQuery6227368&my=list_brand";

            HtmlNode html = HtmlHelper.CrawlAndConvert(url);

            if (html.InnerLength == 0)
            {
                this._OnError?.Invoke(this, new OnErrorEventArgs(Config.WEBTITLE, url, "品牌，抓取到的内容为空"));
                return;
            }

            var js = html.InnerHtml;
            var start = js.IndexOf("{");
            js = js.Substring(start, js.Length - start - 1);

            var json = JSON.Deserialize<Model.JDBrandModel>(js);
            if (json == null)
            {
                this._OnError?.Invoke(this, new OnErrorEventArgs(Config.WEBTITLE, url, "品牌，无法解析抓取到的内容"));
                return;
            }

            AnalysisBrands(json, origid, cid);
        }
        #endregion
    }
}
