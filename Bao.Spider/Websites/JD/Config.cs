using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bao.Spider.Websites.JD
{
    public class Config
    {
        #region const
        public const string WEBTITLE = "京东";
        public const int WSID = 2;
        #endregion

        #region ctor
        public static Dictionary<string, List<string>> config;

        static Config()
        {
            config = new Dictionary<string, List<string>>();

            @default();
            coll_sub();
            list_tid();
        }
        #endregion

        #region default
        static void @default()
        {
            // https://list.jd.com/list.html?cat=737,794,798
            var m = new List<string>();
            m.Add("list.jd.com/list.html?cat=");

            config.Add("Default", m);
        }
        #endregion

        #region coll_sub
        static void coll_sub()
        {
            // https://coll.jd.com/list.html?sub=1661
            var m = new List<string>();
            m.Add("coll.jd.com/list.html?sub=");

            config.Add("CollSub", m);
        }
        #endregion

        #region list_tid
        static void list_tid()
        {
            // https://list.jd.com/list.html?tid=1006340
            var m = new List<string>();
            m.Add("list.jd.com/list.html?tid=");

            config.Add("ListTid", m);
        }
        #endregion
    }
}
