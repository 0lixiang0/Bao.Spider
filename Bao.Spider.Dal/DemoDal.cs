using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bao.Spider.Data.DB;
using Bao.Spider.Dal.Models;

namespace Bao.Spider.Dal
{
    public class DemoDal : DbBase<Demo>
    {
        public static DemoDal DB
        {
            get { return new DemoDal(); }
        }
    }
}
