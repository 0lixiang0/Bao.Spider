using NPoco;

namespace Bao.Spider.Dal.Models
{
    [TableName("b_demo")]
    [PrimaryKey("id")]
    public class Demo : BaseEntity
    {
        [Column("txt")]
        public string txt { get; set; }

        [Column("val")]
        public string val { get; set; }
    }
}
