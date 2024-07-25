using NPoco;

namespace Bao.Spider.Dal.Models
{
    [TableName("b_website")]
    [PrimaryKey("id")]
    public class Website : BaseEntity
    {
        [Column("name")]
        public string name { get; set; }

        [Column("url")]
        public string url { get; set; }

        [Column("status")]
        public int status { get; set; }
    }
}
