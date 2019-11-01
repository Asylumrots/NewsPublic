using NetCoreMyIstence.IRepository;
using NetCoreMyIstence.Model;
using NetCoreMyIstence.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NetCoreMyIstence.IServices
{
    public interface IBannerServices:IBaseRepository<Banner>
    {
        /// <summary>
        /// 获取头部信息
        /// </summary>
        /// <returns>头部信息</returns>
        ResponseModel GetBanner();
        
        /// <summary>
        /// 删除头部信息
        /// </summary>
        /// <param name="id">BannerID</param>
        /// <returns>响应实体模型</returns>
        ResponseModel DelBanner(int id);

        /// <summary>
        /// 添加头部信息
        /// </summary>
        /// <param name="bannerModel">Banner</param>
        /// <returns>响应实体模型</returns>
        ResponseModel AddBanner(BannerModel bannerModel);

        
    }
}
