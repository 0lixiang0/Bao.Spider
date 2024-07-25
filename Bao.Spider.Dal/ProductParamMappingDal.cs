using Bao.Spider.Data.DB;
using Bao.Spider.Dal.Models;

namespace Bao.Spider.Dal
{
    public class ProductParamMappingDal : DbBase<ProductParamMapping>
    {
        public static ProductParamMappingDal DB
        {
            get { return new ProductParamMappingDal(); }
        }
    }
}
