using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Repository.IRepository;
using EmployeeInfo.Service.IService;

namespace EmployeeInfo.Service.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IGenericRepository<Employee> repository, IEmployeeRepository employeeRepository)
        {

            _repository = repository;
            _employeeRepository = employeeRepository;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();

        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<Employee> AddAsync(Employee entity)
        {
            var resutl = await _repository.Insert(entity);
            await _repository.SaveChangesAsync();
            return resutl;
        }

        public async Task EditAsync(Employee entity)
        {
            await _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                await _repository.Delete(entity);
                await _repository.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
