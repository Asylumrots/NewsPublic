using NetCoreMyIstence.IRepository;
using NetCoreMyIstence.IServices;
using NetCoreMyIstence.Model;
using NetCoreMyIstence.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreMyIstence.Services
{
    public class NewsCommentServices:BaseServices<NewsComment>,INewsCommentServices
    {
        private readonly INewsCommentRepsitory newsCommentRepsitory;
        private readonly INewsRepository newsRepository;

        public NewsCommentServices(INewsCommentRepsitory newsCommentRepsitory,INewsRepository newsRepository)
            :base(newsCommentRepsitory)
        {
            this.newsCommentRepsitory = newsCommentRepsitory;
            this.newsRepository = newsRepository;
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="NewsId">新闻Id</param>
        /// <param name="Contents">评论内容</param>
        /// <returns>响应实体模型</returns>
        public ResponseModel AddComment(int NewsId, string Contents)
        {
            int n= newsCommentRepsitory.AddComment(NewsId, Contents);
            if (n>0)
            {
                return new ResponseModel
                {
                    code = 200,
                    message = "添加成功",
                    data=new {
                        contents = Contents,
                        floor = "#" + (newsCommentRepsitory.GetCommentCountByNewsId(NewsId)),
                        addTime = DateTime.Now
                    }
                };
            }
            return new ResponseModel { code = 0, message = "添加评论失败" };
        }

        /// /// <summary>
        /// 删除响应评论
        /// </summary>
        /// <param name="id">评论id</param>
        /// <returns>响应实体模型</returns>
        public ResponseModel DelComment(int id)
        {
            var entity = newsCommentRepsitory.GetbyId(id);
            if (entity == null)
                return new ResponseModel { code = 0, message = "评论不存在" };
            bool del = newsCommentRepsitory.DelEntity(entity);
            if (del == false)
                return new ResponseModel { code = 0, message = "删除失败" };
            return new ResponseModel { code = 200, message = "删除成功" };
        }

        /// <summary>
        /// 获取响应评论列表
        /// </summary>
        /// <returns>响应实体模型</returns>
        public ResponseModel GetComment()
        {
            var list = newsCommentRepsitory.GetAll();
            if (list.Count > 0)
            {
                List<CommentModel> CommentList = new List<CommentModel>();
                foreach (var item in list)
                {
                    CommentList.Add(new CommentModel
                    {
                        Id=item.Id,
                        NewsName=newsRepository.GetbyId(item.NewsId).Title,
                        Contents=item.Contents,
                        AddTime=item.AddTime,
                        Remark=item.Remark,
                    });
                }
                return new ResponseModel { code = 200, message = "获取评论列表成功",data =  CommentList};
            }
            else
            {
                return new ResponseModel { code = 0, message = "获取评论列表失败" };
            }
        }

        /// <summary>
        /// 获取新闻评论列表
        /// </summary>
        /// <param name="newsId">新闻Id</param>
        /// <returns>新闻评论列表</returns>
        public List<NewsComment> GetCommentByNewsId(int newsId)
        {
            return newsCommentRepsitory.GetCommentByNewsId(newsId);
        }

        /// <summary>
        /// 获取新闻评论数量
        /// </summary>
        /// <param name="newsId">新闻Id</param>
        /// <returns>新闻评论数量</returns>
        public int GetCommentCountByNewsId(int newsId)
        {
            return newsCommentRepsitory.GetCommentCountByNewsId(newsId);
        }

        /// <summary>
        /// 获取热度新闻评论列表
        /// </summary>
        /// <param name="topCount">要获取的Top数量</param>
        /// <returns>热度新闻评论列表</returns>
        public List<NewsComment> GetTopNewsComment(int topCount)
        {
            return newsCommentRepsitory.GetTopNewsComment(topCount);
        }
    }
}
