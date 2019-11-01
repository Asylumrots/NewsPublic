using System;
using System.Collections.Generic;
using System.Text;
using NetCoreMyIstence.Model;
using NetCoreMyIstence.ViewModel;

namespace NetCoreMyIstence.IServices
{
    public interface INewsCommentServices:IBaseServices<NewsComment>
    {
        /// <summary>
        /// 获取热度新闻评论列表
        /// </summary>
        /// <param name="topCount">要获取的Top数量</param>
        /// <returns>热度新闻评论列表</returns>
        List<NewsComment> GetTopNewsComment(int topCount);

        /// <summary>
        /// 获取新闻评论列表
        /// </summary>
        /// <param name="newsId">新闻Id</param>
        /// <returns>新闻评论列表</returns>
        List<NewsComment> GetCommentByNewsId(int newsId);

        /// <summary>
        /// 获取新闻评论数量
        /// </summary>
        /// <param name="newsId">新闻Id</param>
        /// <returns>新闻评论数量</returns>
        int GetCommentCountByNewsId(int newsId);

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="NewsId">新闻Id</param>
        /// <param name="Contents">评论内容</param>
        /// <returns>响应实体模型</returns>
        ResponseModel AddComment(int NewsId, string Contents);

        /// <summary>
        /// 获取响应评论列表
        /// </summary>
        /// <returns>响应实体模型</returns>
        ResponseModel GetComment();
        
        /// /// <summary>
        /// 删除响应评论
        /// </summary>
        /// <param name="id">评论id</param>
        /// <returns>响应实体模型</returns>
        ResponseModel DelComment(int id);
    }
}
