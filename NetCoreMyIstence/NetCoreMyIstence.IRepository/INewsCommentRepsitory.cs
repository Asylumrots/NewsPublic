using NetCoreMyIstence.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreMyIstence.IRepository
{
    public interface INewsCommentRepsitory : IBaseRepository<NewsComment>
    {
        /// <summary>
        /// 获取热度新闻评论
        /// </summary>
        /// <param name="topCount">获取Top数量</param>
        /// <returns>新闻评论列表</returns>
        List<NewsComment> GetTopNewsComment(int topCount);

        /// <summary>
        /// 获取新闻评论列表
        /// </summary>
        /// <param name="newsId">新闻Id</param>
        /// <returns>新闻列表</returns>
        List<NewsComment> GetCommentByNewsId(int newsId);

        /// <summary>
        /// 获取新闻评论数量
        /// </summary>
        /// <param name="newsId">新闻Id</param>
        /// <returns>新闻评论数量</returns>
        int GetCommentCountByNewsId(int newsId);

        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="NewsId">新闻Id</param>
        /// <param name="Contents">新闻内容</param>
        /// <returns>数据库表受影响的行数</returns>
        int AddComment(int NewsId, string Contents);

        /// <summary>
        /// 删除一个新闻所有评论
        /// </summary>
        /// <param name="NewsId">新闻Id</param>
        /// <returns>是否删除</returns>
        bool DelAllComment(int NewsId);

    }
}
