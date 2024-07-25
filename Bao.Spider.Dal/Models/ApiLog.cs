using NPoco;

namespace Bao.Spider.Dal.Models
{
    [TableName("api_log")]
    [PrimaryKey("id")]
    public class ApiLog : BaseEntity
    {
        /// <summary>
        /// controller
        /// </summary>
        [Column("controller")]
        public string controller { get; set; }

        /// <summary>
        /// action
        /// </summary>
        [Column("action")]
        public string action { get; set; }

        /// <summary>
        /// 计数
        /// </summary>
        [Column("hits")]
        public int hits { get; set; }
    }
}
