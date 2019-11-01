using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreMyIstence.IServices;
using NetCoreMyIstence.Model;
using NetCoreMyIstence.Models;
using NetCoreMyIstence.ViewModel;
using NetCoreMyIstence.Tool;

namespace NetCoreMyIstence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : BaseController
    {
        private readonly INewsCommentServices newsCommentServices;

        public CommentController(INewsCommentServices newsCommentServices)
        {
            this.newsCommentServices = newsCommentServices;
        }
        public IActionResult Index()
        {
            var list = newsCommentServices.GetComment();
            return View(list);
        }

        public JsonResult DelComment(int id)
        {
            if (id<=0)
                return Json(new ResponseModel { code = 0, message = "评论不存在" });
            return Json(newsCommentServices.DelComment(id));
        }
    }
}