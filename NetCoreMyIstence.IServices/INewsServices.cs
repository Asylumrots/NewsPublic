using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using NetCoreMyIstence.Model;
using NetCoreMyIstence.ViewModel;

namespace NetCoreMyIstence.IServices
{
    public interface INewsServices:IBaseServices<News>
    {
        /// <summary>
        /// 获取页面新闻
        /// </summary>
        /// <returns>响应实体模型</returns>
        ResponseModel GetNewsCount();

        /// <summary>
        /// 获取新闻总数
        /// </summary>
        /// <returns>响应实体模型</returns>
        ResponseModel GetHomeNews();

        /// <summary>
        /// 获取热度评论的新闻列表
        /// </summary>
        /// <returns>响应实体模型</returns>
        ResponseModel GetNewCommentNewsList();

        /// <summary>
        /// 获取所有新闻(按照发布时间倒序)
        /// </summary>
        /// <returns>新闻列表</returns>
        List<News> GetAllDesc();

        /// <summary>
        /// 获取新闻总数
        /// </summary>
        /// <returns>新闻总数</returns>
        int GetNewsConunt();

        /// <summary>
        /// 获取类别新闻列表
        /// </summary>
        /// <param name="ClassifyId">新闻类别Id</param>
        /// <returns>响应实体模型</returns>
        ResponseModel GetNewsByClassifyId(int ClassifyId);

        /// <summary>
        /// 获取分页新闻数据
        /// </summary>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">分页序列</param>
        /// <param name="total">总数</param>
        /// <param name="wheres">where查询</param>
        /// <returns>实体响应模型</returns>
        ResponseModel NewsPageQuery(int pageSize, int pageIndex, ref int total, List<Expression<Func<News, bool>>> wheres);

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns>响应实体模型</returns>
        ResponseModel DelNews(int id);

        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="news">添加的新闻实体</param>
        /// <returns>响应实体模型</returns>
        ResponseModel AddNews(AddNews news);
    }
}
