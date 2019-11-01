using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NetCoreMyIstence.IRepository;
using NetCoreMyIstence.Model;
using NetCoreMyIstence.ViewModel;

namespace NetCoreMyIstence.Repository.SqlServer
{
    public class NewsClassifyRepository : BaseRepository<NewsClassify>, INewsClassifyRepository
    {
        /// <summary>
        /// 获取所有新闻类别信息根据顺序排序
        /// </summary>
        /// <returns>新闻类别信息列表</returns>
        public List<NewsClassify> GetAllOrderBySort()
        {
            return dbContext.Db.Queryable<NewsClassify>().OrderBy(u => u.Sort).ToList();
        }

        public async Task<List<NewsClassify>> GetAllOrderBySortAsync()
        {
            return await dbContext.Db.Queryable<NewsClassify>().OrderBy(u => u.Sort).ToListAsync();
        }

        /// <summary>
        /// 获取分类名称
        /// </summary>
        /// <param name="ClassifyId">类别Id</param>
        /// <returns>分类名称</returns>
        public string GetClassifyName(int ClassifyId)
        {
            return dbContext.Db.Queryable<NewsClassify>().Where(u => u.Id == ClassifyId).First().Name;
        }

        public async Task<string> GetClassifyNameAsync(int ClassifyId)
        {
            var model = await dbContext.Db.Queryable<NewsClassify>().Where(u => u.Id == ClassifyId).FirstAsync();
            return model.Name;
        }

        /// <summary>
        /// 获取最大的类别Id
        /// </summary>
        /// <returns>最大的类别Id</returns>
        public int GetMaxClassifyId()
        {
            return dbContext.Db.Queryable<NewsClassify>().Max(u => u.Id);
        }

        public async Task<int> GetMaxClassifyIdAsync()
        {
            return await dbContext.Db.Queryable<NewsClassify>().MaxAsync(u => u.Id);
        }
    }
}
