using NPoco;

namespace Bao.Spider.Dal.Models
{
    [TableName("b_param")]
    [PrimaryKey("id")]
    public class Param : BaseEntity
    {
        /// <summary>
        /// 对应的网站ID
        /// </summary>
        [Column("wsid")]
        public int wsid { get; set; }

        /// <summary>
        /// 产品分类ID
        /// </summary>
        [Column("prod_cid")]
        public int prod_cid { get; set; }

        /// <summary>
        /// 参数类别ID
        /// </summary>
        [Column("param_cid")]
        public int param_cid { get; set; }

        /// <summary>
        /// 参数名
        /// </summary>
        [Column("name")]
        public string name { get; set; }
    }
}
