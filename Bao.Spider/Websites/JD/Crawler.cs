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
using System.Text;

namespace Bao.Spider.Websites.JD
{
    public class Crawler : ICrawler
    {
        #region fields
        public event EventHandler<OnStartEventArgs> OnStart;
        public event EventHandler<OnCompletedEventArgs> OnCompleted;
        public event EventHandler<OnCrawlEventArgs> OnCrawl;
        public event EventHandler<OnErrorEventArgs> OnError;
        public event EventHandler OnClear;

        Dictionary<string, List<string>> config = Config.config;

        CrawlBase crawl = null;
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

            var category_json_path = Path.Combine(AppContext.BaseDirectory, "data/jd_category.json");
            var json = "";
            using (StreamReader sr = new StreamReader(category_json_path, Encoding.UTF8))
            {
                json = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
            }

            var list = JSON.Deserialize<List<Model.CategoryModel>>(json);

            getCategory(list, 1);

            this.OnCompleted?.Invoke(this, new OnCompletedEventArgs("分类抓取完成"));
        }
        #endregion

        #region 抓取品牌
        public void CrawlProduct()
        {
            this.OnStart?.Invoke(this, new OnStartEventArgs("抓取品牌"));

            var list = CategoryDal.DB.Query().Where(x => x.wsid == Config.WSID && x.origid != "" && x.status == 1).ToList();
            foreach (var item in list)
            {
                var isSub = CategoryDal.DB.Query().Where(x => x.wsid == Config.WSID && x.pid == item.id).Count();
                if (isSub > 0)
                {
                    CategoryDal.DB.Update(new { status = 0 }, item.id);
                    continue;
                }

                if (string.IsNullOrEmpty(item.origid)) continue;

                #region 最后一级时，抓取品牌
                crawl = null;
                crawl = create_crawl(item.url);
                crawl._OnCrawl = this.OnCrawl;
                crawl._OnError = this.OnError;

                // 
                crawl.getBrand(item.origid, item.id);
                #endregion

                CategoryDal.DB.Update(new { status = 0 }, item.id);

                this.OnClear?.Invoke(this, null);
            }
            this.OnCompleted?.Invoke(this, new OnCompletedEventArgs("品牌抓取完成"));
        }
        #endregion

        #region 抓取参数
        public void CrawlDetail()
        {
            this.OnStart?.Invoke(this, new OnStartEventArgs("抓取参数"));

            var list = CategoryDal.DB.Query().Where(x => x.wsid == Config.WSID && x.origid != "" && x.status == 1).ToList();
            foreach (var item in list)
            {
                var isSub = CategoryDal.DB.Query().Where(x => x.wsid == Config.WSID && x.pid == item.id).Count();
                if (isSub > 0)
                {
                    CategoryDal.DB.Update(new { status = 0 }, item.id);
                    continue;
                }

                if (string.IsNullOrEmpty(item.origid)) continue;

                #region 最后一级时，抓取参数
                crawl = null;
                crawl = create_crawl(item.url);
                crawl._OnCrawl = this.OnCrawl;
                crawl._OnError = this.OnError;

                // 
                crawl.getParams(item.url, item.id);
                #endregion

                CategoryDal.DB.Update(new { status = 0 }, item.id);
                this.OnClear?.Invoke(this, null);
            }

            this.OnCompleted?.Invoke(this, new OnCompletedEventArgs("参数抓取完成"));
        }
        #endregion

        #endregion


        #region 抓取分类
        void getCategory(List<Model.CategoryModel> list, int pid)
        {
            foreach (var item in list)
            {
                this.OnCrawl?.Invoke(this, new OnCrawlEventArgs($"{Utils.CompletionCategoryUrl(item.url)}|{item.name}"));

                var url = Utils.CompletionCategoryUrl(item.url);
                var cat = Utils.GetCat(item.url);

                #region 写入数据库
                int id = 0;

                var m = new Category
                {
                    wsid = Config.WSID,
                    pid = pid,
                    cname = item.name,
                    ename = "",
                    url = url,
                    origid = cat,
                    status = 1
                };

                var l = CategoryDal.DB.Query().Where(x => x.pid == m.pid && x.cname == m.cname);
                if (l.Count() == 0)
                    id = CategoryDal.DB.Insert(m).Value;
                else
                id = l.FirstOrDefault().id;
                #endregion

                if (item.sub == null)
                {
                    continue;
                }

                getCategory(item.sub, id);
            }
        }
        #endregion

        #region local fun
        private CrawlBase create_crawl(string url)
        {
            var key = "";
            foreach (var c in config)
            {
                foreach (var item in c.Value)
                {
                    if (url.Contains(item))
                    {
                        key = c.Key;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(key))
                    break;
            }
            
            switch (key)
            {
                case "Default":
                    return new Default();
                case "CollSub":
                    return new CollSub();
                case "ListTid":
                    return new ListTid();
                default:
                    return new Default();
            }
        }
        #endregion
    }
}
