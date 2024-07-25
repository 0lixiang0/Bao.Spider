using Bao.Spider.Data.DB;
using Bao.Spider.Dal.Models;

namespace Bao.Spider.Dal
{
    public class ParamCategoryDal : DbBase<ParamCategory>
    {
        public static ParamCategoryDal DB
        {
            get { return new ParamCategoryDal(); }
        }
    }
}
