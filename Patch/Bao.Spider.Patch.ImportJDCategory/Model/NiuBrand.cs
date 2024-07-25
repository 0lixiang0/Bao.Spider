using NPoco;

namespace Bao.Spider.Patch.ImportJDCategory.Model
{
    [TableName("ns_goods_brand")]
    [PrimaryKey("brand_id")]
    public class NiuBrand
    {
        /// <summary>
        /// 索引ID
        /// </summary>
        public int brand_id { get; set; }

        /// <summary>
        /// 品牌名称
        /// </summary>
        public string brand_name { get; set; }

        /// <summary>
        /// 品牌首字母
        /// </summary>
        public string brand_initial { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public string brand_pic { get; set; }

        /// <summary>
        /// 品牌所属分类名称
        /// </summary>
        public string category_name { get; set; }

        /// <summary>
        /// 一级分类ID
        /// </summary>
        public int category_id_1 { get; set; }

        /// <summary>
        /// 二级分类ID
        /// </summary>
        public int category_id_2 { get; set; }

        /// <summary>
        /// 三级分类ID
        /// </summary>
        public int category_id_3 { get; set; }

    }
}
