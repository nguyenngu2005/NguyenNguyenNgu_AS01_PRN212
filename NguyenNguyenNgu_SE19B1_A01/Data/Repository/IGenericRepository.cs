using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(Func<T, bool> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(Func<T, bool> predicate);
        IEnumerable<T> Search(Expression<Func<T, bool>> predicate);
    }
}
