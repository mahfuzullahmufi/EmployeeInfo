using EmployeeInfo.Repository.Data;
using EmployeeInfo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeInfo.Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public readonly DbSet<T> _db;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            IQueryable<T> query = _db;

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetsAsync(params Expression<Func<T, object>>[] includes)
        {
            return await includes
                .Aggregate(
                   _db.AsQueryable(),
                    (current, include) => current.Include(include)
                ).ToListAsync();
        }

        public async Task<List<T>> GetsAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return await includes
               .Aggregate(
                   _db.AsQueryable(),
                   (current, include) => current.Include(include),
                  c => c.Where(predicate)
               ).ToListAsync().ConfigureAwait(false);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _db.FindAsync(id);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return await includes
               .Aggregate(_db.AsQueryable(),
               (current, include) => current.Include(include),
               c => c.FirstOrDefaultAsync(predicate)).ConfigureAwait(false);
        }

        public async Task<T> Insert(T entity)
        {
            await _db.AddAsync(entity);
            return entity;
        }

        public async Task Update(T entity)
        {
            _db.Update(entity);
        }

        public async Task Delete(T entity)
        {
            _db.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

