using System.Collections.Generic;
using Bao.Spider.Dal.Models;
using Bao.Spider.Dal.ViewModels.Products;
using Bao.Spider.Data.DB;

namespace Bao.Spider.Dal
{
    public class ProductPicDal : DbBase<ProductPic>
    {
        public static ProductPicDal DB
        {
            get { return new ProductPicDal(); }
        }
    }
}
