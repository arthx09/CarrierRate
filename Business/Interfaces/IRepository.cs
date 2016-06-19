
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> Find(Expression<Func<T, bool>> predicate = null);
        T Insert(T obj);
        void Update(T obj);
        void Delete(T obj);
        bool Exists(Expression<Func<T, bool>> predicate);
        void Save();
    }
}
