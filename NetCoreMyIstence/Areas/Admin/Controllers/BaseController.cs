using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NetCoreMyIstence.Web.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        //设置全局权限变量
        public static bool grade = false;
        // GET: Base
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //统一校验登陆
            if (grade==false)
            {
                //filterContext.HttpContext.Response.Redirect("/Login/Index");
                //返回Result不用执行控制器方法代码返回ActionResult提高性能
                filterContext.Result = Redirect("/Admin/Login/Index");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}