using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreMyIstence.IRepository
{
    public interface IBaseRepository<T> where T:class,new()
    {
        /// <summary>
        /// 获取实体模型
        /// </summary>
        /// <param name="whereLamda">Lamda表达式</param>
        /// <returns></returns>
        List<T> LoadEntity(System.Linq.Expressions.Expression<Func<T, bool>> whereLamda);

        /// <summary>
        /// 获取所有数据集合
        /// </summary>
        /// <returns>List集合</returns>
        List<T> GetAll();

        /// <summary>
        /// 根据ID主键来查找数据
        /// </summary>
        /// <param name="id">ID主键</param>
        /// <returns>实体</returns>
        T GetbyId(int id);

        /// <summary>
        /// 添加实体模型
        /// </summary>
        /// <param name="Entity">实体模型</param>
        /// <returns>是否添加成功</returns>
        bool AddEntity(T Entity);

        /// <summary>
        ///删除实体模型 
        /// </summary>
        /// <param name="Entity">实体模型</param>
        /// <returns>是否删除成功</returns>
        bool DelEntity(T Entity);

        /// <summary>
        /// 更新实体模型
        /// </summary>
        /// <param name="Entity">实体模型</param>
        /// <returns>是否更新成功</returns>
        bool ModifyEntity(T Entity);

    }
}
