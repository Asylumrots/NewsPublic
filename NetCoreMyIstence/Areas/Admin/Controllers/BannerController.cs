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
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace NetCoreMyIstence.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : BaseController
    {
        private readonly IBannerServices bannerServices;
        private readonly IHostingEnvironment host;

        public BannerController(IBannerServices bannerServices,IHostingEnvironment host)
        {
            this.bannerServices = bannerServices;
            this.host = host;
        }

        public IActionResult Index()
        {
            var bannerList = bannerServices.GetBanner();
            return View(bannerList);
        }

        [HttpGet]
        public IActionResult BannerAdd()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddBanner(BannerModel bannerModel, IFormCollection collection)
        {
            var files = collection.Files;
            if (files.Count > 0)
            {
                var webRootPath = host.WebRootPath;
                string relativeDirPath = "\\BannerPic";
                string absolutePath = webRootPath + relativeDirPath;

                string[] fileTypes = new string[] { ".gif", ".jpg", ".jpeg", ".png", ".bmp" };
                string extension = Path.GetExtension(files[0].FileName);
                if (fileTypes.Contains(extension.ToLower()))
                {
                    if (!Directory.Exists(absolutePath)) Directory.CreateDirectory(absolutePath);
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                    var filePath = absolutePath + "\\" + fileName;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        files[0].CopyTo(stream);
                    }
                    bannerModel.Image = "/BannerPic/" + fileName;
                    return Json(bannerServices.AddBanner(bannerModel));
                }
                return Json(new ResponseModel { code = 0, message = "图片格式有误" });
            }
            return Json(new ResponseModel { code = 0, message = "请上传图片文件" });
        }

        public JsonResult DelBanner(int id)
        {
            if (id <= 0)
                return Json(new ResponseModel { code = 0, message = "id错误" });
            return Json(bannerServices.DelBanner(id));
        }
    }
}