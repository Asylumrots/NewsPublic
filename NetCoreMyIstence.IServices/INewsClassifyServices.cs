using NetCoreMyIstence.Model;
using NetCoreMyIstence.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreMyIstence.IServices
{
    public interface INewsClassifyServices:IBaseServices<NewsClassify>
    {
        /// <summary>
        /// 获取新闻类别名称
        /// </summary>
        /// <param name="ClassifyId">类别Id</param>
        /// <returns>新闻类别名称</returns>
        string GetClassifyName(int ClassifyId);

        /// <summary>
        /// 获取所有新闻类别信息根据顺序排序
        /// </summary>
        /// <returns>新闻类别信息列表</returns>
        List<NewsClassify> GetAllOrderBySort();

        /// <summary>
        /// 获取响应新闻类别
        /// </summary>
        /// <returns>响应实体模型</returns>
        ResponseModel GetNewsClassify();

        /// <summary>
        /// 获取响应新闻类别
        /// </summary>
        /// <param name="id">新闻类别Id</param>
        /// <returns>响应实体模型</returns>
        ResponseModel GetNewsClassifyById(int id);

        /// <summary>
        /// 编辑新闻类别信息
        /// </summary>
        /// <param name="newsClassifyModel">新闻类别视图模型</param>
        /// <returns>响应实体模型</returns>
        ResponseModel EditNewsClassify(NewsClassifyModel newsClassifyModel);

        /// <summary>
        /// 添加新闻信息信息
        /// </summary>
        /// <param name="newsClassifyModel">新闻类别视图模型</param>
        /// <returns>响应实体模型</returns>
        ResponseModel NewsClassifyAdd(NewsClassifyModel newsClassifyModel);
    }
}
