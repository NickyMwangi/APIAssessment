using Library.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces;

public interface IRepoService
{
    IQueryable<T> Query<T>(bool forUpdate = true) where T : class;
    //T GetById<T>(string id) where T : BaseEntity;
    //Task<T> GetByIdAsync<T>(string id) where T : BaseEntity;
    T GetById<T>(params object[] id) where T : class;
    Task<T> GetByIdAsync<T>(params object[] id) where T : class;
    IEnumerable<T> GetAll<T>() where T : class;
    Task<List<T>> GetAllAsync<T>() where T : class;
    IEnumerable<T> Where<T>(Expression<Func<T, bool>> predicate, bool forUpdate = true) where T : class;
    Task<List<T>> WhereAsync<T>(Expression<Func<T, bool>> predicate, bool forUpdate = true) where T : class;
    T Insert<T>(T entity) where T : class, new();
    T Update<T>(T entity) where T : class;
    T Modify<T>(T entity) where T : class;
    T InsertUpdate<T>(T entity, bool isNew) where T : class, new();
    void Delete<T>(T entity) where T : class;
    Task DeleteAsync<T>(T entity) where T : class;
    void Save();
    Task SaveAsync();
}
