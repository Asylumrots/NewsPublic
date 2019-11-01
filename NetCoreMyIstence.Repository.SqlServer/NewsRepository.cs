using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using NetCoreMyIstence.IRepository;
using NetCoreMyIstence.Model;
using SqlSugar;

namespace NetCoreMyIstence.Repository.SqlServer
{
    public class NewsRepository : BaseRepository<News>, INewsRepository
    {
        /// <summary>
        /// 获取新闻数量
        /// </summary>
        /// <returns>新闻总数</returns>
        public int GetNewsCount()
        {
            return dbContext.NewsDb.Count(u => true);
        }

        /// <summary>
        /// 获取所有新闻(按照发布时间倒序)
        /// </summary>
        /// <returns>新闻列表</returns>
        public List<News> GetAllDesc()
        {
            return dbContext.Db.Queryable<News>().OrderBy(u => u.PublishDate, SqlSugar.OrderByType.Desc).ToList();
        }

        /// <summary>
        /// 获取类别新闻列表
        /// </summary>
        /// <param name="ClassifyId">新闻类别Id</param>
        /// <returns>新闻列表</returns>
        public List<News> GetNewsByClassifyId(int ClassifyId)
        {
            return dbContext.Db.Queryable<News>().Where(u => u.NewsClassifyId.Equals(ClassifyId)).OrderBy(u => u.PublishDate, SqlSugar.OrderByType.Desc).ToList();
        }

        /// <summary>
        /// 获取分页新闻数据
        /// </summary>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">分页序列</param>
        /// <param name="total">总数</param>
        /// <param name="wheres">where查询</param>
        /// <returns>分页新闻列表</returns>
        public List<News> NewsPageQuery(int pageSize, int pageIndex, ref int total, List<Expression<Func<News, bool>>> wheres)
        {
            //var list = dbContext.Db.Queryable<News, NewsClassify>
            //    ((n, cy) => new object[] {JoinType.Left, n.NewsClassifyId == cy.Id.ToString()}).OrderBy(n=>n.PublishDate,OrderByType.Desc);
            var list = dbContext.Db.Queryable<News>
                ().OrderBy(n => n.PublishDate, OrderByType.Desc);
            foreach (var item in wheres)
            {
                list.Where(item);
            }
            list.ToPageList(pageIndex, pageSize,ref total);
            return list.ToList();
        }
    }
}
