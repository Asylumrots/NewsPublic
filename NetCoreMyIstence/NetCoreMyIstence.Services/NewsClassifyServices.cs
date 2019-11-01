using NetCoreMyIstence.IRepository;
using NetCoreMyIstence.IServices;
using NetCoreMyIstence.Model;
using NetCoreMyIstence.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreMyIstence.Services
{
    public class NewsClassifyServices:BaseServices<NewsClassify>,INewsClassifyServices
    {
        private readonly INewsClassifyRepository newsClassifyRepository;

        public NewsClassifyServices(INewsClassifyRepository newsClassifyRepository)
            :base(newsClassifyRepository)
        {
            this.newsClassifyRepository = newsClassifyRepository;
        }

        /// <summary>
        /// 编辑新闻类别信息
        /// </summary>
        /// <param name="newsClassifyModel">新闻类别视图模型</param>
        /// <returns>响应实体模型</returns>
        public ResponseModel EditNewsClassify(NewsClassifyModel newsClassifyModel)
        {
            var NewsClassify = newsClassifyRepository.GetbyId(newsClassifyModel.Id);
            if (NewsClassify == null)
                return new ResponseModel { code = 0, message = "新闻类别不存在" };
            try
            {
                NewsClassify.Id = newsClassifyModel.Id;
                NewsClassify.Name = newsClassifyModel.Name;
                NewsClassify.Sort = Convert.ToInt32(newsClassifyModel.Sort);
                NewsClassify.Remark = newsClassifyModel.Remark;
            }
            catch { return new ResponseModel { code = 0, message = "修改内部错误，青重新编辑" }; }
            bool edit = newsClassifyRepository.ModifyEntity(NewsClassify);
            if (edit==false)
                return new ResponseModel { code = 0, message = "修改失败" };
            return new ResponseModel { code = 200, message = "修改成功" };

        }

        /// <summary>
        /// 获取所有新闻类别信息根据顺序排序
        /// </summary>
        /// <returns>新闻类别信息列表</returns>
        public List<NewsClassify> GetAllOrderBySort()
        {
            return newsClassifyRepository.GetAllOrderBySort();
        }

        /// <summary>
        /// 获取新闻类别名称
        /// </summary>
        /// <param name="ClassifyId">类别Id</param>
        /// <returns>新闻类别名称</returns>
        public string GetClassifyName(int ClassifyId)
        {
            return newsClassifyRepository.GetClassifyName(ClassifyId);
        }

        /// <summary>
        /// 获取响应新闻类别
        /// </summary>
        /// <returns>响应实体模型</returns>
        public ResponseModel GetNewsClassify()
        {
            var list = newsClassifyRepository.GetAll();
            if (list.Count>0)
            {
                List<NewsClassifyModel> NewsClassifyList = new List<NewsClassifyModel>();
                foreach (var item in list)
                {
                    NewsClassifyList.Add(new NewsClassifyModel
                    {
                        Id=item.Id,
                        Name=item.Name,
                        Remark=item.Remark,
                        Sort=item.Sort.ToString(),
                    });
                }
                return new ResponseModel { code = 200, message = "获取评论列表成功", data = NewsClassifyList };
            }
            else
            {
                return new ResponseModel { code = 0, message = "获取新闻类别失败" };
            }
        }

        /// <summary>
        /// 获取响应新闻类别
        /// </summary>
        /// <param name="id">新闻类别Id</param>
        /// <returns>响应实体模型</returns>
        public ResponseModel GetNewsClassifyById(int id)
        {
            var newsClassify = newsClassifyRepository.GetbyId(id);
            if (newsClassify==null)
                return new ResponseModel { code = 0, message = "类别不存在" };
            return new ResponseModel { code = 200, message = "获取类别成功",data=newsClassify };
        }

        /// <summary>
        /// 添加新闻信息信息
        /// </summary>
        /// <param name="newsClassifyModel">新闻类别视图模型</param>
        /// <returns>响应实体模型</returns>
        public ResponseModel NewsClassifyAdd(NewsClassifyModel newsClassifyModel)
        {
            NewsClassify newsClassify = new NewsClassify();
            try
            {
                int n = newsClassifyRepository.GetMaxClassifyId();
                newsClassify.Id = n + 1;
                newsClassify.Name = newsClassifyModel.Name;
                newsClassify.Sort = Convert.ToInt32(newsClassifyModel.Sort);
                newsClassify.Remark = newsClassifyModel.Remark;
            }
            catch { return new ResponseModel { code = 0, message = "信息添加过程失败，请重新填写" }; }
            bool add = newsClassifyRepository.AddEntity(newsClassify);
            if(add==false)
                return new ResponseModel { code = 0, message = "添加失败" };
            return new ResponseModel { code = 200, message = "添加成功" };
        }
    }
}
