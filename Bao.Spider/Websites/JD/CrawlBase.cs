using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Bao.Spider.Dal;
using Bao.Spider.Dal.Models;
using Bao.Spider.Events;
using Bao.Spider.Utils;
using Bao.Spider.Framework;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using Jil;
using System.Text.RegularExpressions;

namespace Bao.Spider.Websites.JD
{
    public class CrawlBase
    {
        #region ctor
        public EventHandler<OnCrawlEventArgs> _OnCrawl { get; set; }
        public EventHandler<OnErrorEventArgs> _OnError { get; set; }

        public CrawlBase()
        {
        }
        #endregion

        #region 抓取品牌
        public virtual void getBrand(string origid, int cid)
        {
            /*
             ** 品牌地址：**
             
             这种页面（https://list.jd.com/list.html?cat=737,794,798）的品牌地址：
             https://list.jd.com/list.html?cat=670,686,689&sort=sort_totalsales15_desc&trans=1&md=1&my=list_brand
             
             这种页面（https://coll.jd.com/list.html?sub=1661）的品牌地址：
             https://fhs.jd.com/api/getattrinfo?sub=1661&md=1&fhc=pc&callback=jQuery6227368&my=list_brand
             
             这种页面（https://list.jd.com/list.html?tid=1006340）的品牌地址：
             https://list.jd.com/list.html?tid=1006340&sort=sort_totalsales15_desc&trans=1&md=1&my=list_brand
            */

            var url = $"https://list.jd.com/list.html?cat={origid}&sort=sort_totalsales15_desc&trans=1&md=1&my=list_brand";

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

        #region 解析获取到的品牌数据
        protected virtual void AnalysisBrands(Model.JDBrandModel json, string origid, int cid)
        {
            var brands = json.brands;

            foreach (var b in brands)
            {
                this._OnCrawl?.Invoke(this, new OnCrawlEventArgs("抓取品牌：" + b.name));

                var logo = DownloadImg(b.logo);

                var m = new Brand
                {
                    wsid = Config.WSID,
                    cname = b.name,
                    ename = b.id.ToString(),
                    logo = logo,
                    pinyin = b.pinyin
                };

                int id = 0;
                var list = BrandDal.DB.Query().Where(x => x.wsid == m.wsid && x.ename == m.ename);
                if (list.Count() == 0)
                    id = BrandDal.DB.Insert(m).Value;
                else
                    id = list.FirstOrDefault().id;

                // https://list.jd.com/list.html?cat=737,794,798&ev=exbrand_7888&sort=sort_rank_asc&trans=1&JL=3_品牌_海信（Hisense）
                var brand_url = $"https://list.jd.com/list.html?cat={origid}&ev=exbrand_{b.id.ToString()}&sort=sort_totalsales15_desc&trans=1&JL=3_{b.name}";

                // 写入关系映射表
                var m_cb = new CategoryBrandMapping
                {
                    wsid = Config.WSID,
                    cid = cid,
                    brand_id = id,
                    url = brand_url,
                    origid = b.id.ToString()
                };

                int mid = 0;
                var mlist = CategoryBrandMappingDal.DB.Query().Where(x => x.wsid == m_cb.wsid && x.cid == m_cb.cid && x.brand_id == m_cb.brand_id && x.origid == m_cb.origid);
                if (mlist.Count() == 0)
                    mid = CategoryBrandMappingDal.DB.Insert(m_cb).Value;
                else
                    mid = mlist.FirstOrDefault().id;
            }
        }
        #endregion

        #region 下载图片
        protected virtual string DownloadImg(string src)
        {
            if (string.IsNullOrEmpty(src)) return "";

            src = Tools.CompleUrl(src);

            var path = $"imgs/jd/brand/logo/";
            var filename = HttpHelper.SaveImageFromWeb(src, path);

            return Path.Combine(path, filename);
        }
        #endregion
        

        #region 抓取参数
        public virtual void getParams(string url, int cid)
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


            #region 页面直接输出的参数
            var paras_cate = html.CssSelect(".J_selectorLine").Where(x=>x.HasClass("s-brand")==false && x.Id != "J_selectorPrice").ToList();
            foreach (var pc in paras_cate)
            {
                #region 参数名
                var key = pc.CssSelect(".sl-key span").FirstOrDefault()?.InnerText.Trim().TrimEnd(new char[] { '：' });

                var param_id = insert_param_name(cid, key);
                #endregion

                this._OnCrawl?.Invoke(this, new OnCrawlEventArgs("抓取参数：" + key));

                #region 参数值
                var values = pc.CssSelect(".J_valueList li a");
                foreach (var v in values)
                {
                    var value = v.InnerText.Trim();

                    insert_param_value(param_id, value);
                }
                #endregion
            }
            #endregion

            #region 用脚本输出的参数
            var re = new Regex("other_exts =(.+);", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            var match = re.Match(html.InnerHtml);
            if (match.Success)
            {
                var js = match.Groups[1].Value;
                if (string.IsNullOrEmpty(js) || js == "{}") return;
                List<Model.ParamModel> list;

                try
                {
                    list = JSON.Deserialize<List<Model.ParamModel>>(js);
                }
                catch
                {
                    list = new List<Model.ParamModel>();
                }

                foreach (var item in list)
                {
                    var key = item.name;
                    var values = item.value_name.Split(';');

                    this._OnCrawl?.Invoke(this, new OnCrawlEventArgs("抓取参数：" + key));

                    // 参数名
                    var param_id = insert_param_name(cid, key);

                    // 参数值
                    foreach (var value in values)
                    {
                        insert_param_value(param_id, value);
                    }
                }
            }
            #endregion
        }

        #region 写入参数名
        private int insert_param_name(int cid, string key)
        {
            // 没有参数分类，默认默认为0
            int param_cid = 0;

            var m_param = new Param
            {
                wsid = Config.WSID,
                prod_cid = cid,
                param_cid = param_cid,
                name = key
            };
            int param_id = 0;
            var plist = ParamDal.DB.Query().Where(x => x.wsid == m_param.wsid && x.prod_cid == m_param.prod_cid && x.param_cid == m_param.param_cid && x.name == m_param.name);

            if (plist.Count() == 0)
                param_id = ParamDal.DB.Insert(m_param).Value;
            else
                param_id = plist.FirstOrDefault().id;

            return param_id;
        }
        #endregion

        #region 写入参数值
        private void insert_param_value(int param_id, string value)
        {
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
        }
        #endregion

        #endregion
    }
}
