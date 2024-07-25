using NPoco;

namespace Bao.Spider.Dal.Models
{
    [TableName("b_param_info")]
    [PrimaryKey("id")]
    public class ParamInfo : BaseEntity
    {
        /// <summary>
        /// 对应的网站ID
        /// </summary>
        [Column("wsid")]
        public int wsid { get; set; }

        /// <summary>
        /// 参数表ID
        /// </summary>
        [Column("param_id")]
        public int param_id { get; set; }

        /// <summary>
        /// 参数内容
        /// </summary>
        [Column("text")]
        public string text { get; set; }
    }
}
