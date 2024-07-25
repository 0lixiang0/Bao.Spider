using NPoco;

namespace Bao.Spider.Dal.Models
{
    [TableName("b_product")]
    [PrimaryKey("id")]
    public class Product : BaseEntity
    {
        /// <summary>
        /// 对应的网站ID
        /// </summary>
        [Column("wsid")]
        public int wsid { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        [Column("cid")]
        public int cid { get; set; }

        /// <summary>
        /// 品牌ID
        /// </summary>
        [Column("brand_id")]
        public int brand_id { get; set; }

        /// <summary>
        /// 产品名
        /// </summary>
        [Column("name")]
        public string name { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [Column("price")]
        public string price { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [Column("desc")]
        public string desc { get; set; }

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
        /// 状态。0=未抓取；1=已抓取
        /// </summary>
        [Column("status")]
        public int status { get; set; }
    }
}
