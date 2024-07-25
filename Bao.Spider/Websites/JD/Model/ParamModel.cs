using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bao.Spider.Websites.JD.Model
{
    public class ParamModel
    {
        /*
         "name":"能效等级",
         "id":1200,
         "value_icon":";;;;",
         "attr_infos":null,
         "value_id":"1654;1655;1656;86829;11",
         "value_name":"1级;2级;3级;政府节能;其他" 
         */
        public string name { get; set; }
        public int id { get; set; }
        public string value_icon { get; set; }
        public string attr_infos { get; set; }
        public string value_id { get; set; }
        public string value_name { get; set; }
    }
}
