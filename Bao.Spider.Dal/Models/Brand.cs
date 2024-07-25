﻿using NPoco;

namespace Bao.Spider.Dal.Models
{
    [TableName("b_brand")]
    [PrimaryKey("id")]
    public class Brand : BaseEntity
    {
        /// <summary>
        /// 对应的网站ID
        /// </summary>
        [Column("wsid")]
        public int wsid { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        [Column("cname")]
        public string cname { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        [Column("ename")]
        public string ename { get; set; }

        /// <summary>
        /// Logo
        /// </summary>
        [Column("logo")]
        public string logo { get; set; }

        /// <summary>
        /// 首字母
        /// </summary>
        [Column("pinyin")]
        public string pinyin { get; set; }
    }
}
