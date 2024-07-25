using NPoco;

namespace Bao.Spider.Patch.ImportJDCategory.Model
{
    [TableName("ns_attribute")]
    [PrimaryKey("attr_id")]
    public class NiuAttribute
    {
        /// <summary>
        /// 商品属性ID
        /// </summary>
        public int attr_id { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string attr_name { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public int is_use { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public int create_time { get; set; }

    }
}
