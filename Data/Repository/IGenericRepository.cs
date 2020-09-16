using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAW_WebApi.Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        DbSet<TEntity> GetAll();
        IQueryable<TEntity> FindAll(Func<TEntity, bool> predicate);
        TEntity Find(Func<TEntity, bool> predicate);
        TEntity GetById(Guid Id);
        TEntity GetById(int Id);
        void Insert(TEntity entity);
        void Update(TEntity entity, int Id);
        void UpdateRange(IQueryable<TEntity> entity);
        void HardDelete(Guid key);
        void HardDelete(int key);
    }
}
