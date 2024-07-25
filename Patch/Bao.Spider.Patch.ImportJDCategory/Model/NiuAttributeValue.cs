using NPoco;

namespace Bao.Spider.Patch.ImportJDCategory.Model
{
    [TableName("ns_attribute_value")]
    [PrimaryKey("attr_value_id")]
    public class NiuAttributeValue
    {
        /// <summary>
        /// 属性值ID
        /// </summary>
        public int attr_value_id { get; set; }

        /// <summary>
        /// 属性值名称
        /// </summary>
        public string attr_value_name { get; set; }

        /// <summary>
        /// 属性ID
        /// </summary>
        public int attr_id { get; set; }

        /// <summary>
        /// 属性对应相关数据
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 属性对应输入类型1.直接2.单选3.多选
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// 是否使用
        /// </summary>
        public int is_search { get; set; } = 1;

    }
}
