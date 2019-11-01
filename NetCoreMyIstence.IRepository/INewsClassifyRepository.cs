using NetCoreMyIstence.Model;
using NetCoreMyIstence.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreMyIstence.IRepository
{
    public interface INewsClassifyRepository : IBaseRepository<NewsClassify>
    {
        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="ClassifyId">类别Id</param>
        /// <returns>分类名称</returns>
        string GetClassifyName(int ClassifyId);

        /// <summary>
        /// 获取最大的类别Id
        /// </summary>
        /// <returns>最大的类别Id</returns>
        int GetMaxClassifyId();

        /// <summary>
        /// 获取所有新闻类别信息根据顺序排序
        /// </summary>
        /// <returns>新闻类别信息列表</returns>
        List<NewsClassify> GetAllOrderBySort();
    }
}
