using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bao.Spider.Dal;
using Bao.Spider.Dal.Models;

namespace Bao.Spider.Patch.RepairPicPath
{
    /// <summary>
    /// 修复图片路径
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            getProducts();
        }

        static void getProducts()
        {
            // 
            var prods = ProductDal.DB.Query().Where(x => x.id <= 34152 && x.pics != "").ToList();
            foreach (var p in prods)
            {
                Console.WriteLine($"[{p.id}]{p.name}");
                //if (string.IsNullOrEmpty(p.pics)) continue;

                var pics = p.pics.Split(',');
                var newpath = new List<string>();
                foreach (var pic in pics)
                {
                    var path = move(pic, p.cid, p.brand_id);

                    newpath.Add(path);
                }

                var originid = Path.GetFileNameWithoutExtension(p.url);

                ProductDal.DB.Update(new { pics = string.Join(",", newpath), origid = originid }, p.id);
            }
            Console.WriteLine("完成");
            Console.ReadKey();
        }

        static string move(string pic, int cid, int bid)
        {
            if (string.IsNullOrEmpty(pic)) return pic;

#if DEBUG
            var baseDir = @"E:\wwwroot\Spider\code\Bao.Spider\Bao.Spider\bin\Debug\";
#else
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
#endif
            baseDir += @"imgs\pconline\";

            var filename = Path.GetFileName(pic);
            var oldpath = Path.Combine(baseDir, filename);

            var newPath = $"{baseDir}{cid}/{bid}/";

            if (!File.Exists(oldpath)) return pic;

            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);

            File.Move(oldpath, newPath + $"{filename}");

            return $"imgs/pconline/{cid}/{bid}/{filename}";
        }
    }
}
