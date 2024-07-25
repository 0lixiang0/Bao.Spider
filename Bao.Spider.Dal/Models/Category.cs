using NPoco;

namespace Bao.Spider.Dal.Models
{
    [TableName("b_category")]
    [PrimaryKey("id")]
    public class Category : BaseEntity
    {
        /// <summary>
        /// 对应的网站ID
        /// </summary>
        [Column("wsid")]
        public int wsid { get; set; }

        /// <summary>
        /// 上级分类ID
        /// </summary>
        [Column("pid")]
        public int pid { get; set; }

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
        /// 地址
        /// </summary>
        [Column("url")]
        public string url { get; set; }

        /// <summary>
        /// 网站上原先的ID
        /// </summary>
        [Column("origid")]
        public string origid { get; set; }

        /// <summary>
        /// 状态。0=不抓取；1=抓取
        /// </summary>
        [Column("status")]
        public int status { get; set; }
    }
}
