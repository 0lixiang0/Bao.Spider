using System;
using System.Collections.Generic;
using Jil;

namespace Bao.Spider.Websites.JD.Model
{
    [Serializable]
    public class CategoryModel
    {
        public string url { get; set; }
        public string name { get; set; }
        
        public List<CategoryModel> sub { get; set; }
    }
}
