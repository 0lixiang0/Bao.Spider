using System.Collections.Generic;
using Bao.Spider.Data.DB;
using Bao.Spider.Dal.Models;
using Bao.Spider.Dal.ViewModels.Products;

namespace Bao.Spider.Dal
{
    public class ParamDal : DbBase<Param>
    {
        public static ParamDal DB
        {
            get { return new ParamDal(); }
        }

        #region 查询参数
        public IEnumerable<ParamModel> QueryParams(int prodid)
        {
            var sql = $@"SELECT p.`name`, i.`text`
                FROM b_param p
                    INNER JOIN b_param_info i ON i.`param_id` = p.`id`
                    INNER JOIN `b_product_param_mapping` m ON m.`param_id` = p.`id` AND m.`param_info_id` = i.`id`
                WHERE m.prod_id = @0;";

            var list = this.Db.Query<ParamModel>(sql, prodid);

            return list;
        }
        #endregion
    }
}
