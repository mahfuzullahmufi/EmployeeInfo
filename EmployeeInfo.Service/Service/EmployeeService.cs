using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Repository.IRepository;
using EmployeeInfo.Service.IService;

namespace EmployeeInfo.Service.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {

            _employeeRepository = employeeRepository;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();

        }

        public async Task<Employee> GetEmployeeByIdWithProjects(int id)
        {
            return await _employeeRepository.GetEmployeeByIdWithProjects(id);
        }

        public async Task<List<Employee>> GetEmployeesWithProjects()
        {
            return await _employeeRepository.GetEmployeesWithProjects();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<Employee> AddAsync(Employee entity)
        {
            var resutl = await _employeeRepository.Insert(entity);
            await _employeeRepository.SaveChangesAsync();
            return resutl;
        }

        public async Task EditAsync(Employee entity)
        {
            await _employeeRepository.Update(entity);
            await _employeeRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                await _employeeRepository.Delete(entity);
                await _employeeRepository.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
