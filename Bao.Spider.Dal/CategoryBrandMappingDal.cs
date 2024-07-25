using Bao.Spider.Dal.Models;
using Bao.Spider.Data.DB;

namespace Bao.Spider.Dal
{
    public class CategoryBrandMappingDal : DbBase<CategoryBrandMapping>
    {
        public static CategoryBrandMappingDal DB
        {
            get { return new CategoryBrandMappingDal(); }
        }
    }
}
