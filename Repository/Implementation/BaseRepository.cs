using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System.Linq.Expressions;

namespace Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().AsNoTracking().Where(expression);
        }
        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }
        public T? GetById(int id) => _context.Set<T>().Find(id);

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
