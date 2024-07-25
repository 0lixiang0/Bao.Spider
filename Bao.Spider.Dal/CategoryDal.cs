using Bao.Spider.Data.DB;
using Bao.Spider.Dal.Models;

namespace Bao.Spider.Dal
{
    public class CategoryDal : DbBase<Category>
    {
        public CategoryDal() : base()
        {
            
        }

        public static CategoryDal DB
        {
            get { return new CategoryDal(); }
        }
    }
}
