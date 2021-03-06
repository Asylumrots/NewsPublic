﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreMyIstence.IServices;
using NetCoreMyIstence.Model;
using NetCoreMyIstence.Models;
using NetCoreMyIstence.ViewModel;
using NetCoreMyIstence.Tool;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace NetCoreMyIstence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsController : BaseController
    {
        private readonly INewsServices newsServices;
        private readonly INewsClassifyServices newsClassifyServices;
        private readonly IHostingEnvironment host;

        public NewsController(INewsServices newsServices,INewsClassifyServices newsClassifyServices,IHostingEnvironment host)
        {
            this.newsServices = newsServices;
            this.newsClassifyServices = newsClassifyServices;
            this.host = host;
        }

        #region 页面视图
        /// <summary>
        /// 新闻页面视图
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var list = newsClassifyServices.GetAll();
            return View(list);
        }

        /// <summary>
        /// 添加新闻页面视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NewsAdd()
        {
            var list = newsClassifyServices.GetAll();
            ResponseModel response;
            if (list == null)
                response = new ResponseModel { code = 200, message = "类别列表获取失败" };
            response = new ResponseModel { code = 200, message = "类别列表获取成功", data = list };
            return View(response);
        }

        /// <summary>
        /// 新闻类别页面视图
        /// </summary>
        /// <returns></returns>
        public IActionResult NewsClassify()
        {
            var list = newsClassifyServices.GetNewsClassify();
            return View(list);
        }

        /// <summary>
        /// 编辑新闻类别视图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NewsClassifyEdit(int id)
        {
            var newsClassify = newsClassifyServices.GetNewsClassifyById(id);
            return View(newsClassify);
        }

        /// <summary>
        /// 添加新闻类别视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NewsClassifyAdd()
        {
            return View();
        }
        #endregion

        /// <summary>
        /// 获取新闻信息
        /// </summary>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="classifyId">类别ID</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetNews(int pageIndex, int pageSize, int classifyId, string keyword)
        {
            List<Expression<Func<News, bool>>> wheres = new List<Expression<Func<News, bool>>>();
            if (classifyId > 0)
            {
                wheres.Add(c => c.NewsClassifyId == classifyId.ToString());
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                wheres.Add(c => c.Title.Contains(keyword));
            }
            int total = 0;
            var news = newsServices.NewsPageQuery(pageSize, pageIndex, ref total, wheres);
            return Json(new { total = total, data = news.data });
        }

        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="news"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddNews(AddNews news,IFormCollection collection)
        {
            if (news.NewsClassifyId <= 0 || string.IsNullOrEmpty(news.Title) || string.IsNullOrEmpty(news.Contents))
                return Json(new ResponseModel { code = 0, message = "参数有误" });
            var files = collection.Files;
            if (files.Count > 0)
            {
                string webRootPath = host.WebRootPath;
                string relativeDirPath = "\\NewsPic";
                string absolutePath = webRootPath + relativeDirPath;
                string[] fileTypes = new string[] { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };//定义允许上传的图片格式
                string extension = Path.GetExtension(files[0].FileName);
                if (fileTypes.Contains(extension))
                {
                    if (!Directory.Exists(absolutePath)) Directory.CreateDirectory(absolutePath);
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                    var filePath = absolutePath + "\\" + fileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        files[0].CopyTo(stream);
                    }
                    news.Image = "/NewsPic/" + fileName;
                    return Json(newsServices.AddNews(news));
                }
                return Json(new ResponseModel { code = 0, message = "图片格式有误" });
            }
            return Json(new ResponseModel { code = 0, message = "请上传图片文件" });
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns></returns>
        public JsonResult DelNews(int id)
        {
            return Json(newsServices.DelNews(id));
        }

        /// <summary>
        /// 添加新闻类别
        /// </summary>
        /// <param name="newsClassifyModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult NewsClassifyAdd(NewsClassifyModel newsClassifyModel)
        {
            return Json(newsClassifyServices.NewsClassifyAdd(newsClassifyModel));
        }

        /// <summary>
        /// 编辑新闻类别
        /// </summary>
        /// <param name="newsClassifyModel"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult NewsClassifyEdit(NewsClassifyModel newsClassifyModel)
        {
            //判断Id
            if (newsClassifyModel.Id<=0)
                return Json(new ResponseModel { code = 0, message = "请填写新闻类别序号" });
            return Json(newsClassifyServices.EditNewsClassify(newsClassifyModel));
        }
    }
}