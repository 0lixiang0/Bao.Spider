using NPoco;

namespace Bao.Spider.Dal.Models
{
    [TableName("b_product_pic")]
    [PrimaryKey("id")]
    public class ProductPic : BaseEntity
    {
        /// <summary>
        /// 对应的网站ID
        /// </summary>
        [Column("wsid")]
        public int wsid { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Column("prod_id")]
        public int prod_id { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [Column("src")]
        public string src { get; set; }
    }
}
