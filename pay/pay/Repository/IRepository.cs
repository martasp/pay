using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pay.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll(int id);
        T Get(int id);
        void Update(T obj);
    }


}
