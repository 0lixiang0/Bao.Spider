using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPoco;
using Bao.Spider.Patch.ImportJDCategory.Model;

namespace Bao.Spider.Patch.ImportJDCategory
{
    public class JDDb
    {
        public static List<JDCategory> GetCategory(int pid)
        {
            var db = new DbBase<JDCategory>("Conn_JD");
            var list = db.Query().Where(x => x.wsid == 2 && x.pid == pid).ToList();

            return list;
        }

        public static List<JDBrand> GetBrand(int cid)
        {
            var db1 = new DbBase<JDCategoryBrandMapping>("Conn_JD");
            var cbs = db1.Query().Where(x => x.wsid == 2 && x.cid == cid).ToList();

            if (cbs.Count() <= 0) return null;

            List<JDBrand> list = new List<JDBrand>();

            var db2 = new DbBase<JDBrand>("Conn_JD");
            foreach (var cb in cbs)
            {
                var m = db2.Query().Where(a => a.wsid == 2 && a.id == cb.brand_id).FirstOrDefault
                    ();
                if (m != null)
                    list.Add(m);
            }

            return list;
        }

        public static (JDCategory c1, JDCategory c2) GetCategoryParentsId(int cid)
        {
            var db = new DbBase<JDCategory>("Conn_JD");

            // 第三级
            var c3 = db.Query().Where(a => a.wsid == 2 && a.id == cid).FirstOrDefault();
            
            // 第二级
            var c2 = db.Query().Where(b => b.wsid == 2 && b.id == c3.pid).FirstOrDefault();

            // 第一级
            var c1 = db.Query().Where(c => c.wsid == 2 && c.id == c2.pid).FirstOrDefault();

            return (c1, c2);
        }

        public static List<JDParam> GetParam(int cid)
        {
            var db = new DbBase<JDParam>("Conn_JD");
            var list = db.Query().Where(x => x.wsid == 2 && x.prod_cid == cid && x.name != "大家说").ToList();

            if (list.Count() <= 0) return null;

            return list;
        }

        public static string GetParamInfo(int pid)
        {
            var db = new DbBase<JDParamInfo>("Conn_JD");
            var list = db.Query().Where(x => x.wsid == 2 && x.param_id == pid).ToList();

            if (list.Count() <= 0) return "";

            var values = list.Select(x => x.text).ToList();

            return string.Join(",", values);
        }
    }
}
