using NetCoreMyIstence.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreMyIstence.IServices
{
    public interface IBaseServices<T>:IBaseRepository<T> where T:class,new()
    {

    }
}
