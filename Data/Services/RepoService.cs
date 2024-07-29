
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.Interfaces;
using Library.Common;
using Data.DBContext;

namespace Data.Services
{
    public class RepoService : IRepoService
    {
        protected readonly Db _dbContext;
        public RepoService(Db dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Query<T>(bool forUpdate = true) where T : class
        {
            var query = _dbContext.Set<T>().AsQueryable();
            if (!forUpdate)
            {
                query.AsNoTracking();
            }
            foreach (var property in _dbContext.Model.FindEntityType(typeof(T)).GetNavigations())
            {
                query = query.Include(property.Name);
            }
            return query;
        }

        //public T GetById<T>(string id) where T : BaseEntity
        //{
        //    return Query<T>().SingleOrDefault(m => m.Id == id);
        //}

        //public async Task<T> GetByIdAsync<T>(string id) where T : BaseEntity
        //{
        //    return await Query<T>().SingleOrDefaultAsync(m => m.Id == id);
        //}

        public T GetById<T>(params object[] id) where T : class
        {
            return _dbContext.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync<T>(params object[] id) where T : class
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return Query<T>().AsEnumerable();
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            return await Query<T>().ToListAsync();
        }

        public IEnumerable<T> Where<T>(Expression<Func<T, bool>> predicate, bool forUpdate = false) where T : class
        {
            return Query<T>().Where(predicate).AsEnumerable();
        }

        public async Task<List<T>> WhereAsync<T>(Expression<Func<T, bool>> predicate, bool forUpdate = false) where T : class
        {
            return await Query<T>(forUpdate).Where(predicate).ToListAsync();
        }

        public T Insert<T>(T entity) where T : class, new()
        {
            _dbContext.Set<T>().Add(entity);
            return entity;
        }

        public T Update<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Update(entity);
            return entity;
        }

        public T Modify<T>(T entity) where T : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            Save();
            return entity;
        }

        public void Delete<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Remove(entity);
            Save();
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Remove(entity);
            await SaveAsync();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public T InsertUpdate<T>(T entity, bool isNew) where T : class, new()
        {
            if (isNew)
                return Insert(entity);
            else
                return Update(entity);
        }
    }
}
