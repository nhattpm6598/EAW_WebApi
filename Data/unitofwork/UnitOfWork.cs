using EAW_WebApi.Data.Repository;
using EAW_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EAW_WebApi.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AEW_DEVContext _context;

        public UnitOfWork(AEW_DEVContext context)
        {
            _context = context;
        }

        public UnitOfWork()
        {
        }

      

        Dictionary<Type, object> reposotories = new Dictionary<Type, object>();
        public IGenericRepository<T> Repository<T>()
            where T : class
        {
            Type type = typeof(T);
            if (!reposotories.TryGetValue(type, out object value))
            {
                var genericRepos = new GenericRepository<T>(_context);
                reposotories.Add(type, genericRepos);
                return genericRepos;
            }
            return value as GenericRepository<T>;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
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

        public int Commit()
        {
            return _context.SaveChanges();
        }

       
        public Task<int> CommitAsync() => _context.SaveChangesAsync();

       
    }
}
