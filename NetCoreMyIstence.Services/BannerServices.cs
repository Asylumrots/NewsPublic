using NetCoreMyIstence.IRepository;
using NetCoreMyIstence.IServices;
using NetCoreMyIstence.Model;
using NetCoreMyIstence.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreMyIstence.Services
{
    public class BannerServices:BaseServices<Banner>,IBannerServices
    {
        private readonly IBannerRepository bannerRepository;

        public BannerServices(IBannerRepository bannerRepository)
            :base(bannerRepository)
        {
            this.bannerRepository = bannerRepository;
        }

        /// <summary>
        /// 添加头部信息
        /// </summary>
        /// <param name="bannerModel">Banner</param>
        /// <returns>响应实体模型</returns>
        public ResponseModel AddBanner(BannerModel bannerModel)
        {
            Banner banner = new Banner();
            try
            {
                banner.Id = bannerModel.Id;
                banner.Image = bannerModel.Image;
                banner.Remark = bannerModel.Remark;
                banner.Url = bannerModel.Url;
                banner.AddTime = DateTime.Now;
            }
            catch { return new ResponseModel { code = 0, message = "内部添加错误,请重试" }; }
            bool add = bannerRepository.AddEntity(banner);
            if(add==false)
                return new ResponseModel { code = 0, message = "添加失败" };
            return new ResponseModel { code = 200, message = "添加成功" };
        }

        /// <summary>
        /// 删除头部信息
        /// </summary>
        /// <param name="id">BannerID</param>
        /// <returns>响应实体模型</returns>
        public ResponseModel DelBanner(int id)
        {
            var banner = bannerRepository.GetbyId(id);
            if (banner == null)
                return new ResponseModel { code = 0, message = "Banner不存在" };
            bool del = bannerRepository.DelEntity(banner);
            if(del==false)
                return new ResponseModel { code = 0, message = "删除失败" };
            return new ResponseModel { code = 200, message = "删除成功" };
        }

        /// <summary>
        /// 获取头部信息
        /// </summary>
        /// <returns>头部信息</returns>
        public ResponseModel GetBanner()
        {
            return new ResponseModel { code=200,message="获取所有Banner",data= bannerRepository.GetAll() };
        }
    }
}
