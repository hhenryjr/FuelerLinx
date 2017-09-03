using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VFM.API.EntityServices
{
    interface IEntityService<T>
    {
        T GetByKey(object key);
        T Insert(T entity);
        T Update(T entity);
        void Delete(T entity);
    }
}
