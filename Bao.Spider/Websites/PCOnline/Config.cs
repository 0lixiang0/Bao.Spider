using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bao.Spider.Websites.PCOnline
{
    public class Config
    {
        #region const
        public const string WEBTITLE = "太平洋电脑";
        public const int WSID = 1;


        // 手机通讯, 笔记本, 电脑配件, 数码产品, 数码配件, 智能穿戴, 整机类, 数字家电, 外设, 扩展配件, 服务器, 网络设备, 无线网络, 办公设备, 耗材
        public static string[] CrawlIds = new string[] { "#c1", "#c3", "#c4", "#c5", "#c6", "#c7", "#c8", "#c9", "#c10", "#c11", "#c12", "#c13", "#c14", "#c15", "#c17"};
        #endregion

        #region ctor
        public static Dictionary<string, Dictionary<string, string>> config;

        static Config()
        {
            config = new Dictionary<string, Dictionary<string, string>>();

            @default();
            pc();
        }
        #endregion

        #region default
        static void @default()
        {
            var m = new Dictionary<string, string>();

            // 整机
            m.Add("pc", "台式机");
            m.Add("easy_pc", "一体机");

            // 笔记本
            m.Add("notebook", "笔记本");
            m.Add("tabletpc", "平板电脑");

            config.Add("Default", m);
        }
        #endregion

        #region pc
        static void pc()
        {
            var m = new Dictionary<string, string>();

            // 整机
            m.Add("pc", "台式机");

            config.Add("PC", m);
        }
        #endregion
    }
}
