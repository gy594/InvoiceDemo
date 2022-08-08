using System.Linq.Expressions;

namespace Repository.Interface
{
    public interface IBaseRepository<T>
        where T : class
    {
        T? GetById(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Update(T entity);
        Task Commit();
    }
}
