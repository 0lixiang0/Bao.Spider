using Bao.Spider.Dal.Models;
using Bao.Spider.Data.DB;

namespace Bao.Spider.Dal
{
    public class BrandDal : DbBase<Brand>
    {
        public static BrandDal DB
        {
            get { return new BrandDal(); }
        }
    }
}
