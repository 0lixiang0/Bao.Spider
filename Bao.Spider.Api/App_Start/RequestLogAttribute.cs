using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Bao.Spider.Dal;

namespace Bao.Spider.Api
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class RequestLogAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var controller = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            var action = actionContext.ActionDescriptor.ActionName;

            ApiLogDal.DB.Count(controller, action);
        }
    }
}