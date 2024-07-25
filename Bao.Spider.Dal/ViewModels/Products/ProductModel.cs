using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bao.Spider.Dal.ViewModels.Products
{
    public class ProductModel
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 产品名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public string price { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string desc { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string pics { get; set; }

        /// <summary>
        /// 分类名
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 品牌名
        /// </summary>
        public string Brand { get; set; }
    }
}