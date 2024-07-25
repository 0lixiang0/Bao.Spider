using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Configuration;

namespace Bao.Spider.Utils
{
    public class Tools
    {
        #region 保存文件
        public static void SaveFile(string filename, string content)
        {
            filename = filename.Replace("/", "_");

            var base_dir = AppContext.BaseDirectory;
            var path = Path.Combine(base_dir, "data/");

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            path += filename + ".json";

            using (StreamWriter sw = new StreamWriter(path, false, Encoding.Default))
            {
                sw.WriteLine(content);
                sw.Close();
            }
        }
        #endregion

        #region 补全缺少 “http:”的地址
        /// <summary>
        /// 补全缺少 “http:”的地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string CompleUrl(string url)
        {
            if (!url.StartsWith("https:") && url.StartsWith("//"))
                return "https:" + url;

            if (!url.StartsWith("https://"))
                return "https://" + url;

            return url;
        }
        #endregion

        #region MD5
        public static string MD5(string str)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] bs = Encoding.UTF8.GetBytes(str);
            byte[] hs = md5.ComputeHash(bs);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hs)
            {
                // 以十六进制格式格式化
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
        #endregion

        #region 读取app.config
        public static string ReadConfig(string key)
        {
            var s = ConfigurationManager.AppSettings[key];
            return s ?? "";
        }
        #endregion
    }
}
