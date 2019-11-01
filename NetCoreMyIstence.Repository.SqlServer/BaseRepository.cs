using NetCoreMyIstence.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMyIstence.Repository.SqlServer
{
    public class BaseRepository<T> where T:class,new()
    {
        public DbContext dbContext = new DbContext();

        /// <summary>
        /// 获取实体模型
        /// </summary>
        /// <param name="whereLamda">Lamda表达式</param>
        /// <returns></returns>
        public List<T> LoadEntity(System.Linq.Expressions.Expression<Func<T, bool>> whereLamda)
        {
            return dbContext.Db.Queryable<T>().Where(whereLamda).ToList();
        }

        //获取实体模型的异步方法
        public async Task<List<T>> LoadEntityAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereLamda)
        {
            return await dbContext.Db.Queryable<T>().Where(whereLamda).ToListAsync();
        }

        /// <summary>
        /// 添加实体模型
        /// </summary>
        /// <param name="Entity">实体模型</param>
        /// <returns>是否添加成功</returns>
        public bool AddEntity(T Entity)
        {
            if (dbContext.Db.Insertable<T>(Entity).ExecuteCommand() > 0)
                return true;
            else
                return false;
        }

        //添加实体模型的异步方法
        public async Task<bool> AddEntityAsync(T Entity)
        {
            int addNum = await dbContext.Db.Insertable<T>(Entity).ExecuteCommandAsync();
            if (addNum > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        ///删除实体模型 
        /// </summary>
        /// <param name="Entity">实体模型</param>
        /// <returns>是否删除成功</returns>
        public bool DelEntity(T Entity)
        {
            if (dbContext.Db.Deleteable<T>(Entity).ExecuteCommand() > 0)
                return true;
            else
                return false;
        }

        //删除实体模型的异步方法
        public async Task<bool> DelEntityAsync(T Entity)
        {
            int delNum = await dbContext.Db.Deleteable<T>(Entity).ExecuteCommandAsync();
            if (delNum > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 更新实体模型
        /// </summary>
        /// <param name="Entity">实体模型</param>
        /// <returns>是否更新成功</returns>
        public bool ModifyEntity(T Entity)
        {
            if (dbContext.Db.Updateable<T>(Entity).ExecuteCommand() > 0)
                return true;
            else
                return false;
        }

        //更新实体模型的异步方法
        public async Task<bool> ModifyEntityAsync(T Entity)
        {
            int modNum = await dbContext.Db.Updateable<T>(Entity).ExecuteCommandAsync();
            if (modNum > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取所有数据集合
        /// </summary>
        /// <returns>List集合</returns>
        public List<T> GetAll()
        {
            return dbContext.Db.Queryable<T>().ToList();
        }

        //获取所有集合的异步方法
        public async Task<List<T>> GetAllAsync()
        {
            return await dbContext.Db.Queryable<T>().ToListAsync();
        }

        /// <summary>
        /// 根据ID主键来查找数据
        /// </summary>
        /// <param name="id">ID主键</param>
        /// <returns>实体</returns>
        public T GetbyId(int id)
        {
            return dbContext.Db.Queryable<T>().InSingle(id);
        }

        //根据id查找的异步方法
        public async Task<T> GetbyIdAsync(int id)
        {
            return await dbContext.Db.Queryable<T>().InSingleAsync(id);
        }
    }
}
