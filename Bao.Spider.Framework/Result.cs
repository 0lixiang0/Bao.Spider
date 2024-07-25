using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bao.Spider.Framework
{
    public class Result
    {
        public bool status { get; set; }

        public string msg { get; set; }

        public object data { get; set; }

        public Result(bool status, string msg, object data)
        {
            this.status = status;
            this.msg = msg;
            this.data = data;
        }

        public Result(bool status, object data)
        {
            this.status = status;
            this.msg = "SUCCESS";
            this.data = data;
        }

        public Result(object data)
        {
            this.status = true;
            this.msg = "SUCCESS";
            this.data = data;
        }
    }
}
