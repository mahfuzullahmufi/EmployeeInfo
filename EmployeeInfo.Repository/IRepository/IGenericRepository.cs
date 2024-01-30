using System.Linq.Expressions;

namespace EmployeeInfo.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetsAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<List<T>> GetsAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        Task<T> Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task SaveChangesAsync();
    }
}
