using NPoco;

namespace Bao.Spider.Dal.Models
{
    [TableName("b_param_category")]
    [PrimaryKey("id")]
    public class ParamCategory : BaseEntity
    {
        /// <summary>
        /// 对应的网站ID
        /// </summary>
        [Column("wsid")]
        public int wsid { get; set; }

        /// <summary>
        /// 参数类别名
        /// </summary>
        [Column("name")]
        public string name { get; set; }
    }
}
