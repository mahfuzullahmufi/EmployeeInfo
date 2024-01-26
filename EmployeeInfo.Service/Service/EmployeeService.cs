using EmployeeInfo.Entities.Models;
using EmployeeInfo.Repository.IRepository;
using EmployeeInfo.Service.IService;
using EmployeeInfo.Service.VM;

namespace EmployeeInfo.Service.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {

            _employeeRepository = employeeRepository;

        }

        public async Task<PagedViewModel<EmployeeTableVm>> GetAllAsync(int page, int pageSize)
        {
            PagedViewModel<EmployeeTableVm> model = new PagedViewModel<EmployeeTableVm>();
            var employeeList = await _employeeRepository.GetAllAsync();

            List<EmployeeTableVm> employeeTableVmList = employeeList
            .Select(e => new EmployeeTableVm
            {
                Id = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                EmployeeDesignation = e.EmployeeDesignation,
                Salary = e.Salary,
                Street = e.Address.Street,
                District = e.Address.District,
                Division = e.Address.Division,
                Hobbies = string.Join(", ", e.Hobbies.Select(obj => obj.HobbyName)),
                Projects = string.Join(", ", e.Projects.Select(obj => obj.ProjectName)),
            }).ToList();

            model.CurrentPage = page;
            model.PageSize = pageSize;
            model.TotalCount = employeeList.Count();
            model.TotalPages = (int)Math.Ceiling((double)model.TotalCount / pageSize);
            model.PagedData = employeeTableVmList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return model;

        }

        public async Task<EmployeeVm?> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);

            EmployeeVm employeeVm = new EmployeeVm();
            employeeVm.EmployeeId = employee.EmployeeId;
            employeeVm.EmployeeName = employee.EmployeeName;
            employeeVm.EmployeeDesignation = employee.EmployeeDesignation;
            employeeVm.Salary = employee.Salary;
            employeeVm.Address = employee.Address;
            employeeVm.Hobbies = employee.Hobbies
            .Select(name => name.HobbyName)
            .ToList();
            employeeVm.Projects = employee.Projects
            .Select(name => name.ProjectName)
            .ToList();

            return employeeVm;
        }

        public async Task<bool> AddAsync(EmployeeVm model)
        {
            Employee entity = new Employee();
            entity.EmployeeName = model.EmployeeName;
            entity.EmployeeDesignation = model.EmployeeDesignation;
            entity.Salary = model.Salary;
            entity.Address = model.Address;
            entity.Hobbies = model.Hobbies
            .Select(name => new Hobby { HobbyName = name })
            .ToList();
            entity.Projects = model.Projects
            .Select(name => new Project { ProjectName = name })
            .ToList();

            var employee = await _employeeRepository.AddAsync(entity);
            await _employeeRepository.SaveChangesAsync();

            if (employee.EmployeeId > 0)
                return true;

            return false;
        }

        public async Task<bool> EditAsync(EmployeeVm model)
        {
            Employee entity = await _employeeRepository.GetByIdAsync(model.EmployeeId);
            if (entity != null)
            {
                entity.EmployeeName = model.EmployeeName;
                entity.EmployeeDesignation = model.EmployeeDesignation;
                entity.Salary = model.Salary;
                entity.Address = model.Address;
                entity.Hobbies = model.Hobbies
                .Select(name => new Hobby { HobbyName = name })
                .ToList();
                entity.Projects = model.Projects
                .Select(name => new Project { ProjectName = name })
                .ToList();

                var employee = await _employeeRepository.EditAsync(entity);
                await _employeeRepository.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _employeeRepository.DeleteAsync(id);


        }
    }
}
