using NPoco;

namespace Bao.Spider.Dal.Models
{
    [TableName("b_product_param_mapping")]
    [PrimaryKey("id")]
    public class ProductParamMapping : BaseEntity
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
        /// 参数ID
        /// </summary>
        [Column("param_id")]
        public int param_id { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        [Column("param_info_id")]
        public int param_info_id { get; set; }
    }
}
