using System.Linq.Expressions;

namespace API.Mappers.Injections
{
    public interface IRepo<T>
    {
        T Get(string id);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        T Insert(T o);
        void Save();
        void Delete(T o);
    }
}