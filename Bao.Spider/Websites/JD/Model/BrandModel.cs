using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bao.Spider.Websites.JD.Model
{
    public class JDBrandModel
    {
        public List<BrandModel> brands { get; set; }
    }
    public class BrandModel
    {
        public object id { get; set; }
        public string logo { get; set; }
        public string name { get; set; }
        public string pinyin { get; set; }
    }
}
