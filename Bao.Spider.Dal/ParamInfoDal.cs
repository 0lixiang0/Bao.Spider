using Bao.Spider.Data.DB;
using Bao.Spider.Dal.Models;

namespace Bao.Spider.Dal
{
    public class ParamInfoDal : DbBase<ParamInfo>
    {
        public static ParamInfoDal DB
        {
            get { return new ParamInfoDal(); }
        }
    }
}
