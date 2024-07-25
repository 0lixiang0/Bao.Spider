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
using Bao.Spider.Framework;

namespace Bao.Spider.Websites.PCOnline
{
    public abstract class CrawlBase
    {
        #region ctor
        public EventHandler<OnCrawlEventArgs> _OnCrawl { get; set; }
        public EventHandler<OnErrorEventArgs> _OnError { get; set; }

        /// <summary>
        /// 抓取页数
        /// </summary>
        private int crawl_page_size = 0;

        public CrawlBase()
        {
            crawl_page_size = Tools.ReadConfig("crawl_page_size").ToInt(0);
        }
        #endregion

        #region 抓取产品列表
        public virtual void getProducts(string url, int cid, int bid)
        {
            var host = HttpHelper.GetHost(url);

            HtmlNode html = HtmlHelper.CrawlAndConvert(url);

            // 如果抓取内容为空，则返回
            if (html.InnerLength == 0)
            {
                this._OnError?.Invoke(this, new OnErrorEventArgs(Config.WEBTITLE, url, "产品列表，抓取到的内容为空"));
                return;
            }

            var prods = html.CssSelect("#JlistItems .item .item-wrap").ToList();

            int i = 1;
            foreach (var prod in prods)
            {
                var detail = prod.CssSelect(".item-detail");

                var a = detail.CssSelect(".item-title a.item-title-name").FirstOrDefault();
                var href = Tools.CompleUrl(a.GetAttributeValue("href"));
                var text = a.InnerText;

                var desc = detail.CssSelect(".item-title-des").FirstOrDefault()?.InnerText;

                var price = prod.CssSelect(".item-sales .price.price-now a").FirstOrDefault()?.InnerText.TrimStart('￥');

                var oid = Utils.getId(href);

                var m = new Product
                {
                    wsid = Config.WSID,
                    cid = cid,
                    brand_id = bid,
                    name = text,
                    price = price,
                    desc = desc,
                    url = href,
                    origid = oid,
                    status = 0
                };

                var list = ProductDal.DB.Query().Where(x => x.wsid == m.wsid && x.origid == m.origid);
                if (list.Count() == 0)
                    ProductDal.DB.Add(m);

                this._OnCrawl?.Invoke(this, new OnCrawlEventArgs($"{i}：" + text));

                // 抓取详情
                //getProductDetail(href, cid, bid);

                i++;
            };

            // 分页
            var pager = html.CssSelect("#Jpager");
            //var pageTotal = pager.CssSelect("#pageTotal").FirstOrDefault().GetAttributeValue("value");
            var current = pager.CssSelect(".page-current").FirstOrDefault().InnerText;
            var next = pager.CssSelect("a.page-next").FirstOrDefault();


            // 判断抓取页数
            if (crawl_page_size > 0 && current.ToInt(1) >= crawl_page_size)
                return;

            // 最后一页
            if (next == null) return;

            var ipage = 0;
            if (!int.TryParse(current, out ipage))
                ipage = 0;

            this._OnCrawl?.Invoke(this, new OnCrawlEventArgs($"开始抓取第{(ipage + 1).ToString()}页"));

            var next_url = host + next.GetAttributeValue("href");
            getProducts(next_url, cid, bid);
        }
        #endregion

        #region 抓取产品详情
        public virtual void getProductDetail(string url, int prodid, int cid, int bid)
        {
            HtmlNode html = HtmlHelper.CrawlAndConvert(url);


            // 如果抓取内容为空，则返回
            if (html.InnerLength == 0 || html.InnerHtml.Trim() == "" || html.InnerHtml.StartsWith("ERROR"))
            {
                this._OnError?.Invoke(this, new OnErrorEventArgs(Config.WEBTITLE, url, "产品详情，抓取到的内容为空"));
                this._OnError?.Invoke(this, new OnErrorEventArgs(Config.WEBTITLE, url, html.InnerHtml));

                return;
            }
            //var info = html.CssSelect(".pro-info");
            //var oid = Utils.getId(url);

            //var text = info.CssSelect(".pro-tit h1").FirstOrDefault().InnerText;
            //var desc = info.CssSelect(".pro-des .pro-des-span").FirstOrDefault()?.InnerText;
            //var price = html.CssSelect(".product-detail-main .product-price .r-price a").FirstOrDefault().InnerText.TrimStart('￥');

            #region 图片
            this._OnCrawl?.Invoke(this, new OnCrawlEventArgs($"开始抓取图片。。"));

            // 图片
            var imgs = html.CssSelect("#JareaTop .smallpics>ul>li>a>img");
            if (imgs.Count() > 3)
                imgs = imgs.Take(3);

            var c = imgs.Count();

            int i = 1;
            // 下载图片
            var pics = new List<string>();
            foreach (var img in imgs)
            {
                this._OnCrawl?.Invoke(this, new OnCrawlEventArgs($"{i}/{imgs.Count()}"));

                var p = img.GetAttributeValue("data-bigpic");
                var src = Tools.CompleUrl(p);

                var path = $"imgs/pconline/{cid}/{bid}/";
                var filename = HttpHelper.SaveImageFromWeb(src, path);

                //pics.Add(Path.Combine(path, filename));

                // 新增图片
                var m = new ProductPic
                {
                    wsid = Config.WSID,
                    prod_id = prodid,
                    src =Path.Combine(path, filename)
                };

                ProductPicDal.DB.Add(m);

                i++;
            }
            #endregion

            // 参数
            var nav = html.CssSelect("#Jnav ul>li>a[title$=参数]").FirstOrDefault();
            if (nav != null)
                getParams(Tools.CompleUrl(nav.GetAttributeValue("href")), cid, prodid);

            // 更改状态
            ProductDal.DB.Update(new { status = 1 }, prodid);
        }
        #endregion

        #region 抓取参数
        protected virtual void getParams(string url, int prod_cid, int prod_id)
        {
            this._OnCrawl?.Invoke(this, new OnCrawlEventArgs("正在分析产品参数。。"));

            HtmlNode html = HtmlHelper.CrawlAndConvert(url);

            // 如果抓取内容为空，则返回
            if (html.InnerLength == 0)
            {
                this._OnError?.Invoke(this, new OnErrorEventArgs(Config.WEBTITLE, url, "产品参数，抓取到的内容为空"));
                return;
            }

            this._OnCrawl?.Invoke(this, new OnCrawlEventArgs("开始抓取参数"));

            List<string> list = new List<string>();

            var paras_cate = html.CssSelect("#area-detailparams table.dtparams-table").ToList();
            foreach (var pc in paras_cate)
            {
                var m = new ParamCategory
                {
                    wsid = Config.WSID,
                    name = pc.CssSelect("thead th i").FirstOrDefault()?.InnerText.Trim()
                };
                if (string.IsNullOrEmpty(m.name)) continue;

                int param_cid = 0;
                var param_clist = ParamCategoryDal.DB.Query().Where(x => x.wsid == m.wsid && x.name == m.name);
                if (param_clist.Count() == 0)
                    param_cid = ParamCategoryDal.DB.Insert(m).Value;
                else
                    param_cid = param_clist.FirstOrDefault().id;

                #region 抓取详细参数
                var paras = pc.CssSelect("tbody tr").ToList();
                foreach (var p in paras)
                {
                    string key = p.CssSelect("th").FirstOrDefault()?.InnerText.Trim();
                    string value = "";

                    var values = p.CssSelect("td a.poptxt");
                    if (values.Count() > 0)
                        value = string.Join(",", values.Select(x => x.InnerText.Trim()));
                    else
                        value = p.CssSelect("td").FirstOrDefault()?.InnerText.Trim();

                    if (string.IsNullOrEmpty(key)) continue;

                    this._OnCrawl?.Invoke(this, new OnCrawlEventArgs($"{key}：{value}"));

                    #region 添加到参数名表
                    var m_param = new Param
                    {
                        wsid = Config.WSID,
                        prod_cid = prod_cid,
                        param_cid = param_cid,
                        name = key
                    };
                    int param_id = 0;
                    var plist = ParamDal.DB.Query().Where(x => x.wsid == m_param.wsid && x.prod_cid == m_param.prod_cid && x.param_cid == m_param.param_cid && x.name == m_param.name);

                    if (plist.Count() == 0)
                        param_id = ParamDal.DB.Insert(m_param).Value;
                    else
                        param_id = plist.FirstOrDefault().id;
                    #endregion

                    #region 添加到参数值表中
                    var m_info = new ParamInfo
                    {
                        wsid = Config.WSID,
                        param_id = param_id,
                        text = value
                    };
                    int info_id = 0;
                    var infolist = ParamInfoDal.DB.Query().Where(x => x.wsid == m_info.wsid && x.param_id == m_info.param_id && x.text == m_info.text);

                    if (infolist.Count() == 0)
                        info_id = ParamInfoDal.DB.Insert(m_info).Value;
                    else
                        info_id = infolist.FirstOrDefault().id;
                    #endregion

                    #region 添加到参数与产品的关联表中
                    var m_mapping = new ProductParamMapping
                    {
                        wsid = Config.WSID,
                        prod_id = prod_id,
                        param_id = param_id,
                        param_info_id = info_id
                    };
                    int mapping_id = 0;
                    var maplist = ProductParamMappingDal.DB.Query().Where(x => x.wsid == m_mapping.wsid && x.prod_id == m_mapping.prod_id && x.param_id == m_mapping.param_id && x.param_info_id == m_mapping.param_info_id);

                    if (maplist.Count() == 0)
                        mapping_id = ProductParamMappingDal.DB.Insert(m_mapping).Value;
                    else
                        mapping_id = maplist.FirstOrDefault().id;
                    #endregion
                }
                #endregion
            }
        }
        #endregion

    }
}
