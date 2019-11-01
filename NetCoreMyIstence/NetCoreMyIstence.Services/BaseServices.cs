using NetCoreMyIstence.IRepository;
using NetCoreMyIstence.IServices;
using NetCoreMyIstence.Repository.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreMyIstence.Services
{
    public class BaseServices<T> : IBaseServices<T> where T : class, new()
    {
        private readonly IBaseRepository<T> baseRepository;

        public BaseServices(IBaseRepository<T> baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        /// <summary>
        /// 添加实体模型
        /// </summary>
        /// <param name="Entity">实体模型</param>
        /// <returns>是否添加成功</returns>
        public bool AddEntity(T Entity)
        {
            return baseRepository.AddEntity(Entity);
        }

        //public async Task<bool> AddEntityAsync(T Entity)
        //{
        //    bool add = await baseRepository.AddEntity(Entity);
        //    return add;
        //}

        /// <summary>
        ///删除实体模型 
        /// </summary>
        /// <param name="Entity">实体模型</param>
        /// <returns>是否删除成功</returns>
        public bool DelEntity(T Entity)
        {
            return baseRepository.DelEntity(Entity);
        }

        /// <summary>
        /// 获取所有数据集合
        /// </summary>
        /// <returns>List集合</returns>
        public List<T> GetAll()
        {
            return baseRepository.GetAll();
        }

        /// <summary>
        /// 根据ID主键来查找数据
        /// </summary>
        /// <param name="id">ID主键</param>
        /// <returns>实体</returns>
        public T GetbyId(int id)
        {
            return baseRepository.GetbyId(id);
        }
        
        /// <summary>
        /// 获取实体模型
        /// </summary>
        /// <param name="whereLamda">Lamda表达式</param>
        /// <returns></returns>
        public List<T> LoadEntity(Expression<Func<T, bool>> whereLamda)
        {
            return baseRepository.LoadEntity(whereLamda);
        }

        /// <summary>
        /// 更新实体模型
        /// </summary>
        /// <param name="Entity">实体模型</param>
        /// <returns>是否更新成功</returns>
        public bool ModifyEntity(T Entity)
        {
            return baseRepository.ModifyEntity(Entity);
        }
    }
}
