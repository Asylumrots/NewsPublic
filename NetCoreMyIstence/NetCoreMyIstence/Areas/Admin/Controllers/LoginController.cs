using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreMyIstence.Tool;
using NetCoreMyIstence.ViewModel;

namespace NetCoreMyIstence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CheckLogin(string username, string password, string code)
        {
            if (code.ToLower() != HttpContext.Session.GetString("Code").ToLower())
            {
                return Json(new ResponseModel { code = 0, message = "验证码错误" });
            }
            if (username == "admin" && password == "123456")
            {
                BaseController.grade = true;
                return Json(new ResponseModel { code = 200, message = "登陆成功" });
            }
            return Json(new ResponseModel { code=0,message="用户名或密码错误"});
        }

        public string CodeString;
        public IActionResult GetAuthCode()
        {
            byte[] img = new VerifyCode().GetVerifyCode(out CodeString);
            //设置验证码Session
            HttpContext.Session.SetString("Code", CodeString);
            return File(img, @"image/Gif");
        }
    }
}