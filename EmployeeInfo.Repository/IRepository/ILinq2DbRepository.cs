using System.Linq.Expressions;

namespace EmployeeInfo.Repository.IRepository
{
    public interface ILinq2DbRepository<T> where T : class
    {
        IQueryable<T> Table { get; }

        Task<List<T>> GetAllAsync();
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
