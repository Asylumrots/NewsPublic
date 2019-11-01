using NetCoreMyIstence.IRepository;
using NetCoreMyIstence.IServices;
using NetCoreMyIstence.Model;
using NetCoreMyIstence.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NetCoreMyIstence.Services
{
    public class NewsServices:BaseServices<News>,INewsServices
    {
        private readonly INewsRepository newsRepository;
        private readonly INewsCommentRepsitory newsCommentRepsitory;
        private readonly INewsClassifyRepository newsClassifyRepository;

        //依赖注入
        public NewsServices(INewsRepository newsRepository,INewsCommentRepsitory newsCommentRepsitory,INewsClassifyRepository newsClassifyRepository)
            : base(newsRepository)
        {
            this.newsRepository = newsRepository;
            this.newsCommentRepsitory = newsCommentRepsitory;
            this.newsClassifyRepository = newsClassifyRepository;
        }

        /// <summary>
        /// 获取所有新闻(按照发布时间倒序)
        /// </summary>
        /// <returns>新闻列表</returns>
        public List<News> GetAllDesc()
        {
            return newsRepository.GetAllDesc();
        }

        /// <summary>
        /// 获取页面新闻
        /// </summary>
        /// <returns>响应实体模型</returns>
        public ResponseModel GetHomeNews()
        {
            List<News> list = newsRepository.GetAllDesc();
            var lists = new List<NewsModel>();
            foreach (var item in list)
            {
                lists.Add(
                    new NewsModel
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Image = item.Image,
                        PublishDate = item.PublishDate.ToLongDateString(),
                        Contents = item.Contents.Length > 50 ? item.Contents.Substring(0, 50)+"..." : item.Contents,
                        Remark = item.Remark,
                        CommentCount = newsCommentRepsitory.GetCommentCountByNewsId(item.Id),
                    }
                );
            }
            return new ResponseModel { code = 200, message = "页面新闻信息", data = lists };
        }

        /// <summary>
        /// 获取热度评论的新闻列表
        /// </summary>
        /// <returns>响应实体模型</returns>
        public ResponseModel GetNewCommentNewsList()
        {
            var list = newsCommentRepsitory.GetTopNewsComment(3);//top3
            List<NewsModel> newsList = new List<NewsModel>();
            foreach (var item in list)
            {
                var items = newsRepository.GetbyId(item.NewsId);
                newsList.Add(new NewsModel {
                    Id = items.Id,
                    Title = items.Title,
                    Image = items.Image,
                    PublishDate = items.PublishDate.ToLongDateString(),
                });
            }
            return new ResponseModel { code = 200, message = "最新评论新闻", data = newsList };
        }

        /// <summary>
        /// 获取新闻总数
        /// </summary>
        /// <returns>响应实体模型</returns>
        public ResponseModel GetNewsCount()
        {
            int n =newsRepository.GetNewsCount();
            return new ResponseModel { code = 200, message = "新闻总数", data = n };
        }

        /// <summary>
        /// 获取新闻总数
        /// </summary>
        /// <returns>新闻总数</returns>
        public int GetNewsConunt()
        {
            return newsRepository.GetNewsCount();
        }

        /// <summary>
        /// 获取类别新闻列表
        /// </summary>
        /// <param name="ClassifyId">新闻类别Id</param>
        /// <returns>响应实体模型</returns>
        public ResponseModel GetNewsByClassifyId(int ClassifyId)
        {
            var list = newsRepository.GetNewsByClassifyId(ClassifyId);
            var lists = new List<NewsModel>();
            foreach (var item in list)
            {
                lists.Add(
                    new NewsModel {
                        Id = item.Id,
                        Title=item.Title,
                        Image=item.Image,
                        PublishDate=item.PublishDate.ToShortDateString(),
                        Contents=item.Contents.Length>50? item.Contents.Substring(0, 50)+"..." : item.Contents,
                        Remark=item.Remark,
                        CommentCount= newsCommentRepsitory.GetCommentCountByNewsId(item.Id),
                    }
                );
            }
            return new ResponseModel { code = 200,message="类别新闻列表" ,data = lists };
        }

        /// <summary>
        /// 获取分页新闻数据
        /// </summary>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">分页序列</param>
        /// <param name="total">总数</param>
        /// <param name="wheres">where查询</param>
        /// <returns>实体响应模型</returns>
        public ResponseModel NewsPageQuery(int pageSize, int pageIndex, ref int total, List<Expression<Func<News, bool>>> wheres)
        {
            var list = newsRepository.NewsPageQuery(pageSize, pageIndex, ref total, wheres);
            ResponseModel response = new ResponseModel { code = 200, message = "获取分页数据" };
            response.data = new List<NewsModel>();
            foreach (var news in list)
            {
                response.data.Add(new NewsModel
                {
                    Id = news.Id,
                    ClassifyName = newsClassifyRepository.GetClassifyName(Convert.ToInt32(news.NewsClassifyId)),
                    Title = news.Title,
                    Image = news.Image,
                    Contents = news.Contents.Length > 50 ? news.Contents.Substring(0, 50) + "..." : news.Contents,
                    PublishDate = news.PublishDate.ToLongDateString(),
                    Remark = news.Remark
                });
            }
            return response;
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id">新闻id</param>
        /// <returns>响应实体模型</returns>
        public ResponseModel DelNews(int id)
        {
            var model = newsRepository.GetbyId(id);
            bool delComment = newsCommentRepsitory.DelAllComment(id);
            bool del = newsRepository.DelEntity(model);
            if (del == false || delComment==false)
                return new ResponseModel { code = 0, message = "删除失败" };
            return new ResponseModel { code = 200, message = "删除成功" };
        }

        /// <summary>
        /// 添加新闻
        /// </summary>
        /// <param name="news">添加的新闻实体</param>
        /// <returns>响应实体模型</returns>
        public ResponseModel AddNews(AddNews news)
        {
            News n;
            try
            {
                n = new News
                {
                    NewsClassifyId = news.NewsClassifyId.ToString(),
                    Title = news.Title,
                    Image = news.Image,
                    Contents = news.Contents,
                    PublishDate = DateTime.Now,
                    Remark = news.Remark
                };
            }
            catch
            {
                return new ResponseModel { code = 0, message = "添加内部信息错误,请重新填写" };
            }
            bool add = newsRepository.AddEntity(n);
            if (add == false)
                return new ResponseModel { code = 0, message = "添加失败" };
            return new ResponseModel { code = 200, message = "添加成功" };
        }
    }
}
