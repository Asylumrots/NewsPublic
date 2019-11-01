using System;
using System.Collections.Generic;
using System.Text;
using NetCoreMyIstence.IRepository;
using NetCoreMyIstence.Model;

namespace NetCoreMyIstence.Repository.SqlServer
{
    public class NewsCommentRepository : BaseRepository<NewsComment>, INewsCommentRepsitory
    {
        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="NewsId">新闻Id</param>
        /// <param name="Contents">新闻内容</param>
        /// <returns>数据库表受影响的行数</returns>
        public int AddComment(int NewsId, string Contents)
        {
            var Comment = new NewsComment { NewsId = NewsId, AddTime = DateTime.Now, Contents = Contents };
            return dbContext.Db.Insertable(Comment).ExecuteCommand();
        }

        /// <summary>
        /// 删除一个新闻所有评论
        /// </summary>
        /// <param name="NewsId">新闻Id</param>
        /// <returns>是否删除</returns>
        public bool DelAllComment(int NewsId)
        {
            if (dbContext.Db.Deleteable<NewsComment>().In(n => n.NewsId, new int[] { NewsId }).ExecuteCommand()>0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// 获取新闻评论列表
        /// </summary>
        /// <param name="newsId">新闻Id</param>
        /// <returns>新闻列表</returns>
        public List<NewsComment> GetCommentByNewsId(int newsId)
        {
            return dbContext.Db.Queryable<NewsComment>().Where(u => u.NewsId == newsId).OrderBy(u => u.AddTime, SqlSugar.OrderByType.Desc).ToList();
        }

        /// <summary>
        /// 获取新闻评论数量
        /// </summary>
        /// <param name="newsId">新闻Id</param>
        /// <returns>新闻评论数量</returns>
        public int GetCommentCountByNewsId(int newsId)
        {
            return dbContext.Db.Queryable<NewsComment>().Where(u => u.NewsId == newsId).Count();
        }

        /// <summary>
        /// 获取热度新闻评论
        /// </summary>
        /// <param name="topCount">获取Top数量</param>
        /// <returns>新闻评论列表</returns>
        public List<NewsComment> GetTopNewsComment(int topCount)
        {
            var list = dbContext.Db.Queryable<NewsComment>().OrderBy(u => u.AddTime, SqlSugar.OrderByType.Desc).PartitionBy(it => new { it.NewsId }).ToList();
            list.Reverse();//反序
            if (list.Count>topCount)
            {
                list = list.GetRange(0, topCount);
            }//取前topcount条，在sqlsuger中take报错
            return list;
            //return dbContext.Db.Queryable<NewsComment>().OrderBy(u => u.AddTime, SqlSugar.OrderByType.Desc).Take(topCount).ToList();
        }
        
    }
}
