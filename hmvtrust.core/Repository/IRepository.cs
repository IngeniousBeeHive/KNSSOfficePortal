using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hmvtrust.core
{
    public interface IRepository<T> where T : AppBase
    {
        T Add(T t);
        T Update(T t);
        void Delete(long id);

        T Get(long id);
        List<T> Get();

        T Get(System.Linq.Expressions.Expression<Func<T, bool>> expression);
        List<T> GetList(System.Linq.Expressions.Expression<Func<T, bool>> expression);
    }
}
