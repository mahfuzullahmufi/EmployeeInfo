using EmployeeInfo.Entities.Models;
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

        public async Task<PagedViewModel<Employee>> GetAllAsync(int page, int pageSize)
        {
            try
            {
                PagedViewModel<Employee> model = new PagedViewModel<Employee>();
                var employeeList = await _employeeRepository.GetAllAsync();

                model.CurrentPage = page;
                model.PageSize = pageSize;
                model.TotalCount = employeeList.Count();
                model.TotalPages = (int)Math.Ceiling((double)model.TotalCount / pageSize);
                model.PagedData = employeeList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddAsync(Employee entity)
        {
            var employee = await _employeeRepository.AddAsync(entity);
            await _employeeRepository.SaveChangesAsync();

            if(employee.Id>0)
                return true;
            return false;
        }
        
        public async Task<bool> EditAsync(Employee entity)
        {
            var employee = await _employeeRepository.EditAsync(entity);
            await _employeeRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            if(employee != null)
            {
                await _employeeRepository.DeleteAsync(employee);
                await _employeeRepository.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
