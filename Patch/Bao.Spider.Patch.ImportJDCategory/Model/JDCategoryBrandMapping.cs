using NPoco;

namespace Bao.Spider.Patch.ImportJDCategory.Model
{
    [TableName("b_category_brand_mapping")]
    [PrimaryKey("id")]
    public class JDCategoryBrandMapping
    {
        public int id { get; set; }

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
