using EmployeeInfo.Repository.Data;
using EmployeeInfo.Repository.IRepository;
using LinqToDB;
using LinqToDB.DataProvider;
using System.Linq.Expressions;

namespace EmployeeInfo.Repository.Repository
{
    public class Linq2DbRepository<T> : ILinq2DbRepository<T> where T : class
    {
        private Linq2DbDataConnection _connection;

        public Linq2DbRepository(Linq2DbDataConnection connection)
        {
            _connection = connection;
        }

        public virtual IQueryable<T> Table => _connection.GetTable<T>();

        public async Task<List<T>> GetAllAsync()
        {
            return await _connection.GetTable<T>().ToListAsync();
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _connection.GetTable<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task InsertAsync(T entity)
        {
            await _connection.InsertAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            await _connection.UpdateAsync(entity);
        }

        public async Task DeleteAsync(T entity)
        {
            await _connection.DeleteAsync(entity);
        }
    }
}

