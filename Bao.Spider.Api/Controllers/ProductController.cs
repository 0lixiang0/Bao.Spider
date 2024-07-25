using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Bao.Spider.Framework;
using Bao.Spider.Dal;
using Bao.Spider.Dal.ViewModels.Products;

namespace Bao.Spider.Api.Controllers
{
    public class ProductController : ApiController
    {
        #region get list
        public JsonResult<Result> Get([FromUri]QueryModel qm)
        {
            //Logger.Default.Info($"name:{qm.Name}");
            var prods = ProductDal.DB.QueryProduct(qm).ToList();

            foreach (var prod in prods)
            {
                prod.pics = string.Join(",", prod.pics.Split(',').Take(3));
            }

            var result = new Result(prods);
            return Json(result); 
        }
        #endregion

        #region get products
        public JsonResult<Result> GetProducts([FromUri]string ids)
        {
            var iids = ids.Split(',').Select(x => {
                var r = 0;
                if (int.TryParse(x, out r))
                    return r;
                return 0;
            }).ToArray();

            var prods = ProductDal.DB.GetList(iids);

            var result = new List<object>();
            foreach (var p in prods)
            {
                result.Add(new
                {
                    p.id,
                    p.name,
                    p.price,
                    p.desc,
                    pics = string.Join(",", p.pics.Split(',').Take(3)),
                    p.Category,
                    p.Brand,
                    param = ParamDal.DB.QueryParams(p.id)
                });
            }
            return Json(new Result(result));
        }
        #endregion

        #region get params
        [Obsolete]
        public JsonResult<Result> GetParams([FromUri]int prodid)
        {
            var @params = ParamDal.DB.QueryParams(prodid);

            var result = new Result(@params);
            return Json(result);
        }
        #endregion
    }
}