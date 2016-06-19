
using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using System.Linq.Expressions;
using System.Data.Entity;    

namespace Business
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected CarrierRateContext _context;

        #region ctor
        public Repository()
        {
            _context = new CarrierRateContext();
            _context.Configuration.AutoDetectChangesEnabled = false;
            _context.Configuration.EnsureTransactionsForFunctionsAndCommands = false;
            _context.Configuration.LazyLoadingEnabled = false;
            _context.Configuration.ProxyCreationEnabled = false;
            _context.Configuration.UseDatabaseNullSemantics = false;
            _context.Configuration.ValidateOnSaveEnabled = false;
        }
        #endregion

        #region Methods
        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate = null)
        {
            if (predicate != null)
            {
                return _context.Set<T>().Where(predicate);
            }
            else
            {
                return _context.Set<T>();
            }
        }

        public T Find(int id)
        {
            var obj = _context.Set<T>().Find(id);
            _context.Entry(obj).State = EntityState.Detached;
            return obj;
        }

        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Any(predicate);
        }

        public virtual T Insert(T obj)
        {
            if (obj != null)
            {
                _context.Configuration.ValidateOnSaveEnabled = false;
                _context.Configuration.AutoDetectChangesEnabled = true;
                _context.Set<T>().Add(obj);
            }
            return obj;
        }

        public virtual void Update(T obj)
        {
            if (obj != null)
            {
                _context.Configuration.ValidateOnSaveEnabled = false;
                _context.Configuration.AutoDetectChangesEnabled = true;
                _context.Entry<T>(obj).State = EntityState.Modified;
            }
        }

        public virtual void Delete(T obj)
        {
            if (obj != null)
            {
                _context.Configuration.ValidateOnSaveEnabled = false;
                _context.Set<T>().Attach(obj);
                _context.Set<T>().Remove(obj);
            }
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }
        #endregion

        #region dispose
        private bool disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
