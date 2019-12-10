using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreMyIstence.IServices;
using NetCoreMyIstence.Model;
using NetCoreMyIstence.ViewModel;

namespace NetCoreMyIstence.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsServices newsServices;
        private readonly INewsCommentServices newsCommentServices;
        private readonly INewsClassifyServices newsClassifyServices;
        //依赖注入
        public NewsController(INewsServices newsServices,INewsCommentServices newsCommentServices,INewsClassifyServices newsClassifyServices)
        {
            this.newsServices = newsServices;
            this.newsCommentServices = newsCommentServices;
            this.newsClassifyServices = newsClassifyServices;
        }

        /// <summary>
        /// 新闻详细页面
        /// </summary>
        /// <param name="id">新闻Id</param>
        /// <returns>View页面</returns>
        public IActionResult Detail(int id)
        {
            ViewData["Title"] = "详情页";
            if (id<=0)
                Response.Redirect("/Home/Index");
            News news = newsServices.GetbyId(id);
            ViewData["News"] = new ResponseModel();
            ViewData["RecommendNews"] = new ResponseModel();
            ViewData["Comments"] = new ResponseModel();
            if (news != null)
            {
                ViewData["Title"] = news.Title + " -详情页";
                ViewData["News"] = news;
                ViewData["NewsConunt"] = newsServices.GetNewsConunt();
                ViewData["NewsCommnetCount"] = newsCommentServices.GetCommentCountByNewsId(id);
                ViewData["NewsClassify"] = newsClassifyServices.GetClassifyName(Convert.ToInt32(news.NewsClassifyId));
                ViewData["Comments"] = newsCommentServices.GetCommentByNewsId(news.Id);
            }
            else
                Response.Redirect("/Home/Index");
            //页面头部类别显示
            List<NewsClassify> list = newsClassifyServices.GetAllOrderBySort();
            return View(list);
        }

        /// <summary>
        /// 新闻类别页面
        /// </summary>
        /// <param name="id">类别ID</param>
        /// <returns></returns>
        public IActionResult Classify(int id)
        {
            ViewData["Title"] = "分类页";
            if (id <= 0)
                Response.Redirect("/Home/Index");
            NewsClassify newsClassify = newsClassifyServices.GetbyId(id);
            ViewData["ClassifyName"] = "分类页";
            ViewData["NewsList"] = new ResponseModel();
            ViewData["NewCommentNews"] = new ResponseModel();
            if (newsClassify != null)
            {
                ViewData["ClassifyName"] = newsClassify.Name;
                ViewData["Title"]= newsClassify.Name;
                ViewData["NewsList"] = newsServices.GetNewsByClassifyId(id);
            }
            else
                Response.Redirect("/Home/Index");
            //页面头部类别显示
            List<NewsClassify> list = newsClassifyServices.GetAllOrderBySort();
            return View(list);
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="NewsId">新闻Id</param>
        /// <param name="Contents">评论内容</param>
        /// <returns>Json</returns>
        public JsonResult AddComment(int NewsId, string Contents)
        {
            if (NewsId<=0)
            {
                return Json(new ResponseModel { code = 0, message = "新闻不存在", });
            }
            if (string.IsNullOrEmpty(Contents))
            {
                return Json(new ResponseModel { code = 0, message = "内容不能为空", });
            }
            return Json( newsCommentServices.AddComment(NewsId, Contents) );
        }
    }
}