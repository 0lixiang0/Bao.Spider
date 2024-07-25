using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;

namespace Bao.Spider.Patch.ImportJDCategory.Model
{
    [TableName("b_brand")]
    [PrimaryKey("id")]
    public class JDBrand
    {
        public int id { get; set; }

        /// <summary>
        /// 对应的网站ID
        /// </summary>
        [Column("wsid")]
        public int wsid { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        [Column("cname")]
        public string cname { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        [Column("ename")]
        public string ename { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        [Column("logo")]
        public string logo { get; set; }

        /// <summary>
        /// 首字母
        /// </summary>
        [Column("pinyin")]
        public string pinyin { get; set; }
    }
}
