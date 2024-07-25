using Bao.Spider.Data.DB;
using Bao.Spider.Dal.Models;

namespace Bao.Spider.Dal
{
    public class WebsiteDal : DbBase<Website>
    {
        public static WebsiteDal DB
        {
            get { return new WebsiteDal(); }
        }
    }
}
