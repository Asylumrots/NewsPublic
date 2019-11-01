using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreMyIstence.IServices;
using NetCoreMyIstence.Model;
using NetCoreMyIstence.Models;
using NetCoreMyIstence.ViewModel;
using NetCoreMyIstence.Tool;

namespace NetCoreMyIstence.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBannerServices bannerServices;
        private readonly INewsServices newsServices;
        private readonly INewsClassifyServices newsClassifyServices;
        private readonly INewsCommentServices newsCommentServices;

        //依赖注入
        public HomeController(IBannerServices bannerServices,INewsServices newsServices,INewsClassifyServices newsClassifyServices,INewsCommentServices newsCommentServices)
        {
            this.bannerServices = bannerServices;
            this.newsServices = newsServices;
            this.newsClassifyServices = newsClassifyServices;
            this.newsCommentServices = newsCommentServices;
        }
        
        public IActionResult Index()
        {
            List<NewsClassify> list = newsClassifyServices.GetAllOrderBySort();
            return View(list);
        }

        public IActionResult Wrong()
        {
            return View();
        }

        /// <summary>
        /// 获取新闻总数
        /// </summary>
        /// <returns>新闻总数</returns>
        public JsonResult GetTotalNews()
        {
            return Json(newsServices.GetNewsCount());
        }

        /// <summary>
        /// 获取页面新闻
        /// </summary>
        /// <returns>页面新闻</returns>
        public JsonResult GetHomeNews()
        {
            return Json(newsServices.GetHomeNews());
        }

        /// <summary>
        /// 获取最近评论新闻列表
        /// </summary>
        /// <returns>最近评论新闻列表</returns>
        public JsonResult GetNewCommentNewsList()
        {
            return Json(newsServices.GetNewCommentNewsList());
        }

        /// <summary>
        /// 获取头部信息
        /// </summary>
        /// <returns>头部信息</returns>
        public JsonResult GetBanner()
        {
            return Json(bannerServices.GetBanner());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
