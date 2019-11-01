using NetCoreMyIstence.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NetCoreMyIstence.IRepository
{
    public interface INewsRepository : IBaseRepository<News>
    {
        /// <summary>
        /// 获取新闻数量
        /// </summary>
        /// <returns>新闻总数</returns>
        int GetNewsCount();

        /// <summary>
        /// 获取所有新闻(按照发布时间倒序)
        /// </summary>
        /// <returns>新闻列表</returns>
        List<News> GetAllDesc();

        /// <summary>
        /// 获取类别新闻列表
        /// </summary>
        /// <param name="ClassifyId">新闻类别Id</param>
        /// <returns>新闻列表</returns>
        List<News> GetNewsByClassifyId(int ClassifyId);

        /// <summary>
        /// 获取分页新闻数据
        /// </summary>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">分页序列</param>
        /// <param name="total">总数</param>
        /// <param name="wheres">where查询</param>
        /// <returns>分页新闻列表</returns>
        List<News> NewsPageQuery(int pageSize,int pageIndex,ref int total, List<Expression<Func<News, bool>>> wheres);
    }
}
