using EAW_WebApi.Data.Repository;
using System;
using System.Threading.Tasks;

namespace EAW_WebApi.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<T> Repository<T>()
          where T : class;

        
        int Commit();
        new void Dispose();

        Task<int> CommitAsync();


    }
}
