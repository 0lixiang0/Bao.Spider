using System;
using System.Collections.Generic;
using System.Linq;
using Bao.Spider.Dal;
using Bao.Spider.Dal.Models;
using Bao.Spider.Events;
using Bao.Spider.Utils;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

/// <summary>
/// www.pconline.com.cn
/// </summary>
namespace Bao.Spider.Websites.PCOnline
{
    public class Crawler : ICrawler
    {
        #region fields
        public event EventHandler<OnStartEventArgs> OnStart;
        public event EventHandler<OnCompletedEventArgs> OnCompleted;
        public event EventHandler<OnCrawlEventArgs> OnCrawl;
        public event EventHandler<OnErrorEventArgs> OnError;
        public event EventHandler OnClear;

        Dictionary<string, Dictionary<string, string>> config = Config.config;
        #endregion

        #region ctor
        public Crawler()
        {
        }
        #endregion

        #region start

        #region 抓取分类
        public void CrawlCategory()
        {
            this.OnStart?.Invoke(this, new OnStartEventArgs("开始抓取分类"));
            getCategory();
            this.OnCompleted?.Invoke(this, new OnCompletedEventArgs("分类抓取完成"));
        }
        #endregion

        #region 抓取产品列表
        public void CrawlProduct()
        {
            this.OnStart?.Invoke(this, new OnStartEventArgs("开始抓取产品"));

            CrawlBase crawl = null;

            var cbmaps = CategoryBrandMappingDal.DB.Query().Where(x => x.wsid == Config.WSID).ToList();

            Category cat = null;
            Brand brand = null;
            foreach (var cbmap in cbmaps)
            {
                if (cat == null || cat.id != cbmap.cid)
                    cat = CategoryDal.DB.Get(cbmap.cid);
                brand = BrandDal.DB.Get(cbmap.brand_id);

                crawl = null;
                crawl = create_crawl(cat.ename);
                crawl._OnCrawl = this.OnCrawl;
                crawl._OnError = this.OnError;

                this.OnCrawl?.Invoke(this, new OnCrawlEventArgs($"抓取产品，分类：{cat.cname}，品牌：{brand.cname}"));

                crawl.getProducts(cbmap.url, cat.id, cbmap.brand_id);

                this.OnClear?.Invoke(this, null);
            }

            this.OnCompleted?.Invoke(this, new OnCompletedEventArgs("产品抓取完成"));
        }
        #endregion

        #region 抓取产品详情
        public void CrawlDetail()
        {
            this.OnStart?.Invoke(this, new OnStartEventArgs("开始抓取产品明细"));

            CrawlBase crawl = null;

            var cbmaps = CategoryBrandMappingDal.DB.Query().Where(x => x.wsid == Config.WSID && x.status == 0).ToList();
            //this.OnStart?.Invoke(this, new OnStartEventArgs($"CategoryBrandMapping: {cbmaps.Count()}"));

            Category cat = null;
            foreach (var cbmap in cbmaps)
            {
                this.OnCrawl?.Invoke(this, new OnCrawlEventArgs($"cbmap: {cbmap.id}"));

                var products = ProductDal.DB.Query().Where(x => x.wsid == Config.WSID && x.cid == cbmap.cid && x.brand_id == cbmap.brand_id && x.status == 0).ToList();

                if (products.Count() <= 0) continue;

                if (cat == null || cat.id != cbmap.cid)
                    cat = CategoryDal.DB.Query().Where(x => x.wsid == Config.WSID && x.id == cbmap.cid).FirstOrDefault();

                foreach (var prod in products)
                {
                    this.OnCrawl?.Invoke(this, new OnCrawlEventArgs($"抓取产品：{prod.name}，分类：{cat.cname}"));
                    //this.OnError?.Invoke(this, new OnErrorEventArgs(Config.WEBTITLE, prod.url, $"抓取产品：{prod.name}，分类：{cat.cname}"));

                    crawl = null;
                    crawl = create_crawl(cat.ename);
                    crawl._OnCrawl = this.OnCrawl;
                    crawl._OnError = this.OnError;

                    crawl.getProductDetail(prod.url, prod.id, prod.cid, prod.brand_id);

                    //products.Remove(prod);

                    this.OnClear?.Invoke(this, null);
                }

                //cbmaps.Remove(cbmap);
                CategoryBrandMappingDal.DB.Update(new { status = 1 }, cbmap.id);

            }

            this.OnCompleted?.Invoke(this, new OnCompletedEventArgs("产品明细抓取完成"));
        }
        #endregion

        #region local fun
        private string get_cate(string cat)
        {
            foreach (var c in config)
            {
                if (c.Value.ContainsKey(cat))
                    return c.Key;
            }
            return "Default";
        }

        private CrawlBase create_crawl(string ename)
        {
            switch (get_cate(ename))
            {
                case "Default":
                default:
                    return new Default();
                case "PC":
                    return new PC();
            }
        }
        #endregion

        #endregion


        #region 抓取分类
        void getCategory()
        {
            var url = "http://product.pconline.com.cn/category.html";

            HtmlNode html = HtmlHelper.CrawlAndConvert(url);

            if (html.InnerLength == 0)
            {
                this.OnError?.Invoke(this, new OnErrorEventArgs(Config.WEBTITLE, url, "产品分类，抓取到的内容为空"));
                return;
            }

            var cats = html.CssSelect("#showboxNav ul.cate-nav li").ToList();

            //List<Category> category = new List<Category>();

            foreach (var cat in cats)
            {
                var a = cat.FirstChild;
                var text = a.InnerText.Trim();
                var href = a.GetAttributeValue("data-href");

                //
                if (!Config.CrawlIds.Contains(href)) continue;

                this.OnCrawl?.Invoke(this, new OnCrawlEventArgs("抓取分类：" + text));

                var m = new Category
                {
                    wsid = Config.WSID,
                    pid = 0,
                    cname = text,
                    ename = "",
                    url = "",
                    origid = href,
                    status = 1
                };

                int id = 0;
                var list = CategoryDal.DB.Query().Where(x => x.pid == m.pid && x.cname == m.cname);
                if (list.Count() == 0)
                    id = CategoryDal.DB.Insert(m).Value;
                else
                    id = list.FirstOrDefault().id;

                getChildCategory(html, href, id);
            };
        }
        #endregion

        #region 子分类
        void getChildCategory(HtmlNode html, string href, int pid)
        {
            var cats = html.CssSelect($"{href} div.list-detail a").ToList();

            foreach (var cat in cats)
            {
                var url = Tools.CompleUrl(cat.GetAttributeValue("href").Trim());

                var text = cat.InnerText.Trim();
                var ename = Utils.getCategoryEname(url);

                this.OnCrawl?.Invoke(this, new OnCrawlEventArgs("抓取子分类：" + text));

                var m = new Category
                {
                    wsid = Config.WSID,
                    pid = pid,
                    cname = text,
                    ename = ename,
                    url = url,
                    origid = "",
                    status = 1
                };

                int id = 0;
                var list = CategoryDal.DB.Query().Where(x => x.wsid == m.wsid && x.pid == m.pid && x.cname == m.cname);
                if (list.Count() == 0)
                    id = CategoryDal.DB.Insert(m).Value;
                else
                    id = list.FirstOrDefault().id;

                getBrand(url, id);

                this.OnClear?.Invoke(this, null);
            };
        }
        #endregion

        #region 抓取品牌
        void getBrand(string url, int prod_cid)
        {
            var host = HttpHelper.GetHost(url);

            HtmlNode html = HtmlHelper.CrawlAndConvert(url);

            if (html.InnerLength == 0)
            {
                this.OnError?.Invoke(this, new OnErrorEventArgs(Config.WEBTITLE, url, "品牌，抓取到的内容为空"));
                return;
            }

            var bras = html.CssSelect("#tab-all .con-item a").ToList();

            foreach(var bra in bras)
            {
                var oid = bra.GetAttributeValue("id").Substring(6);
                var text = bra.InnerText;
                var href = host + bra.GetAttributeValue("href");
                var ename = Utils.getCategoryEname(href);

                this.OnCrawl?.Invoke(this, new OnCrawlEventArgs("抓取品牌：" + text));

                var m = new Brand
                {
                    wsid = Config.WSID,
                    cname = text,
                    ename = ename
                };

                int id = 0;
                var list = BrandDal.DB.Query().Where(x => x.wsid == m.wsid && x.ename == m.ename);
                if (list.Count() == 0)
                    id = BrandDal.DB.Insert(m).Value;
                else
                    id = list.FirstOrDefault().id;

                // 写入关系映射表
                var m_cb = new CategoryBrandMapping
                {
                    wsid = Config.WSID,
                    cid = prod_cid,
                    brand_id = id,
                    url = href,
                    origid = oid
                };

                int mid = 0;
                var mlist = CategoryBrandMappingDal.DB.Query().Where(x => x.wsid == m_cb.wsid && x.cid == m_cb.cid && x.brand_id == m_cb.brand_id && x.origid == m_cb.origid);
                if (mlist.Count() == 0)
                    mid = CategoryBrandMappingDal.DB.Insert(m_cb).Value;
                else
                    mid = mlist.FirstOrDefault().id;

                // 抓取产品
                //getProduct(href, prod_cid, id);
            };
        }
        #endregion
    }
}
