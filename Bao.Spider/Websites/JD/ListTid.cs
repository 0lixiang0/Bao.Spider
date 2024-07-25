using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bao.Spider.Dal;
using Bao.Spider.Dal.Models;
using Bao.Spider.Events;
using Bao.Spider.Utils;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using Jil;
using Bao.Spider.Framework;
using System.Text;

namespace Bao.Spider.Websites.JD
{
    public class ListTid : CrawlBase
    {
        #region 抓取品牌
        public override void getBrand(string origid, int cid)
        {
            /*
             ** 品牌地址：**
             这种页面（https://list.jd.com/list.html?tid=1006340）的品牌地址：
             https://list.jd.com/list.html?tid=1006340&sort=sort_totalsales15_desc&trans=1&md=1&my=list_brand
            */

            var url = $"https://list.jd.com/list.html?tid={origid}&sort=sort_totalsales15_desc&trans=1&md=1&my=list_brand";

            HtmlNode html = HtmlHelper.CrawlAndConvert(url);

            if (html.InnerLength == 0)
            {
                this._OnError?.Invoke(this, new OnErrorEventArgs(Config.WEBTITLE, url, "品牌，抓取到的内容为空"));
                return;
            }

            var json = JSON.Deserialize<Model.JDBrandModel>(html.InnerHtml);
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
