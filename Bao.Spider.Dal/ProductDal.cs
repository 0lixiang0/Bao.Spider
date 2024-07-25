using System.Collections.Generic;
using Bao.Spider.Dal.Models;
using Bao.Spider.Dal.ViewModels.Products;
using Bao.Spider.Data.DB;

namespace Bao.Spider.Dal
{
    public class ProductDal : DbBase<Product>
    {
        public static ProductDal DB
        {
            get { return new ProductDal(); }
        }

        #region 查询产品
        public IEnumerable<ProductModel> QueryProduct(QueryModel qm)
        {
            var where1 = new List<string>();
            var where2 = new List<string>();

            var name = qm.Name.Split(' ');
            
            foreach (var s in name)
            {
                where1.Add(" p.`name` LIKE '%" + s + "%' ");
                where2.Add(" p.`desc` LIKE '%" + s + "%' ");
            }

            var sql = $@"SELECT p.id, p.`name`, p.`desc`, p.`price`, c.`cname` AS Category, b.`cname` AS Brand,
                    GROUP_CONCAT(pic.src) AS pics
                FROM b_product p
                    INNER JOIN b_category c ON c.`id` = p.`cid`
                    INNER JOIN b_brand b ON b.`id` = p.`brand_id`
                    INNER JOIN b_product_pic pic ON pic.prod_id = p.id
                WHERE p.`status` = 1 AND (("+ string.Join(" AND ", where1) +") OR ("+ string.Join(" AND ", where2) +")) " +
                @"GROUP BY p.id, p.`name`, p.`desc`, p.`price`, c.`cname`, b.`cname`
                ORDER BY p.id ASC
                LIMIT 0, 10";

            // qm.Name = "%" + qm.Name + "%";

            var list = this.Db.Query<ProductModel>(sql, qm);

            return list;
        }
        #endregion

        #region 根据IDs，返回产品信息
        public IEnumerable<ProductModel> GetList(int[] ids)
        {
            var sql = $@"SELECT p.id, p.`name`, p.`desc`, p.`price`, c.`cname` AS Category, b.`cname` AS Brand,
                    GROUP_CONCAT(pic.src) AS pics
                FROM b_product p
                    INNER JOIN b_category c ON c.`id` = p.`cid`
                    INNER JOIN b_brand b ON b.`id` = p.`brand_id`
                    INNER JOIN b_product_pic pic ON pic.prod_id = p.id
                WHERE p.`status` = 1 AND p.id IN (@0)
                GROUP BY p.id, p.`name`, p.`desc`, p.`price`, c.`cname`, b.`cname`
                ORDER BY p.id DESC";

            var list = this.Db.Query<ProductModel>(sql, ids);

            return list;
        }
        #endregion
    }
}
