using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bao.Spider.Dal.ViewModels.Products
{
    public class CategoryModel
    {
        /// <summary>
        /// 分类名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 子分类
        /// </summary>
        public CategoryModel Children { get; set; }
    }
}