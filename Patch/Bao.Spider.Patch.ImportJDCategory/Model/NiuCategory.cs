using NPoco;

namespace Bao.Spider.Patch.ImportJDCategory.Model
{
    [TableName("ns_goods_category")]
    [PrimaryKey("category_id")]
    public class NiuCategory
    {
        /// <summary>
        ///  
        /// </summary>
        public int category_id { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public string category_name { get; set; }

        /// <summary>
        /// 商品分类简称
        /// </summary>
        public string short_name { get; set; }

        /// <summary>
        ///  
        /// </summary>
        public int pid { get; set; }

        /// <summary>
        ///  层级
        /// </summary>
        public int level { get; set; }

        /// <summary>
        /// 是否显示 1 显示 0 不显示
        /// </summary>
        public int is_visible { get; set; } = 1;

    }
}
