using EAW_WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAW_WebApi.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private static AEW_DEVContext _Context;
        public static DbSet<T> _table { get; set; }
        public GenericRepository(AEW_DEVContext context)
        {
            _Context = context;
            _table = _Context.Set<T>();
        }

        public T Find(Func<T, bool> predicate)
        {
            return _table.FirstOrDefault(predicate);
        }

        public IQueryable<T> FindAll(Func<T, bool> predicate)
        {
            return _table.Where(predicate).AsQueryable();

        }

        public DbSet<T> GetAll()
        {
            return _table;
        }

        public T GetById(Guid Id)
        {
            return _table.Find(Id);
        }

        public T GetById(int Id)
        {
            return _table.Find(Id);
        }
        public void HardDelete(Guid key)
        {
            _table.Remove(GetById(key));
        }
        public void HardDelete(int key)
        {
            _table.Remove(GetById(key));
        }
        public void Insert(T entity)
        {
            _table.Add(entity);
        }

        public void Update(T entity,int Id)
        {
            var existEntity = GetById(Id);
            _Context.Entry(existEntity).CurrentValues.SetValues(entity);
            _table.Update(existEntity);
        }

        public void UpdateRange(IQueryable<T> entity)
        {
            _table.UpdateRange(entity);
        }
        
    }
}
