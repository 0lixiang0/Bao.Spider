using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bao.Spider.Patch.ImportJDCategory.Model;

namespace Bao.Spider.Patch.ImportJDCategory
{
    public class NiuDb
    {
        public static int AddCategory(NiuCategory m)
        {
            var db = new DbBase<NiuCategory>("Conn_NiuShop");

            var id = db.Insert(m);

            return id.HasValue ? id.Value : 0;
        }

        public static int AddBrand(NiuBrand m)
        {
            var db = new DbBase<NiuBrand>("Conn_NiuShop");

            var id = db.Insert(m);

            return id.HasValue ? id.Value : 0;
        }

        public static int AddAttribute(NiuAttribute m)
        {
            var db = new DbBase<NiuAttribute>("Conn_NiuShop");

            var id = db.Insert(m);

            return id.HasValue ? id.Value : 0;
        }

        public static int AddAttributeValue(NiuAttributeValue m)
        {
            var db = new DbBase<NiuAttributeValue>("Conn_NiuShop");

            var id = db.Insert(m);

            return id.HasValue ? id.Value : 0;
        }
    }
}
