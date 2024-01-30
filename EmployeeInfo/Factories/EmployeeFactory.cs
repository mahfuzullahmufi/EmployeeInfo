using AutoMapper;
using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Service.IService;
using EmployeeInfo.Web.Models;

namespace EmployeeInfo.Web.Factories
{
    public class EmployeeFactory : IEmployeeFactory
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        public EmployeeFactory(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        public async Task<PagedViewModel<EmployeeTableModel>> GetEmployeesWithProjects(int page, int pageSize)
        {
            PagedViewModel<EmployeeTableModel> model = new PagedViewModel<EmployeeTableModel>();
            var employeeList = await _employeeService.GetEmployeesWithProjects();

            List<EmployeeTableModel> employeeTableModelList = employeeList
            .Select(e => new EmployeeTableModel
            {
                Id = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                EmployeeDesignation = e.EmployeeDesignation,
                Salary = e.Salary,
                Street = e.Address.Street,
                District = e.Address.District,
                Division = e.Address.Division,
                Hobbies = string.Join(", ", e.Hobbies.Select(obj => obj.HobbyName)),
                Projects = string.Join(", ", e.EmployeeProjects.Select(obj => obj.Project.ProjectName)),
            }).ToList();

            model.CurrentPage = page;
            model.PageSize = pageSize;
            model.TotalCount = employeeList.Count();
            model.TotalPages = (int)Math.Ceiling((double)model.TotalCount / pageSize);
            model.PagedData = employeeTableModelList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return model;

        }

        public async Task<EmployeeModel> GetEmployeeByIdWithProjects(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdWithProjects(id);

            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeId = employee.EmployeeId;
            employeeModel.EmployeeName = employee.EmployeeName;
            employeeModel.EmployeeDesignation = employee.EmployeeDesignation;
            employeeModel.Salary = employee.Salary;
            employeeModel.Street = employee.Address.Street;
            employeeModel.District = employee.Address.District;
            employeeModel.Division = employee.Address.Division;
            employeeModel.Hobbies = employee.Hobbies.Select(name => name.HobbyName).ToList();
            employeeModel.Projects = employee.EmployeeProjects.Select(name => name.Project.ProjectId).ToList();

            return employeeModel;
        }

        public async Task AddEmployeeAsync(EmployeeModel model)
        {
            Employee entity = new Employee
            {
                EmployeeName = model.EmployeeName,
                EmployeeDesignation = model.EmployeeDesignation,
                Salary = model.Salary,
                Address = new Address { Street = model.Street, District = model.District, Division = model.Division },
                Hobbies = model.Hobbies.Select(name => new Hobby { HobbyName = name }).ToList(),
                EmployeeProjects = model.Projects.Select(id => new EmployeeProject { ProjectId = id }).ToList()
            };

            var result = await _employeeService.AddAsync(entity);
        }

        public async Task EditAsync(EmployeeModel model)
        {
            var entity = await _employeeService.GetEmployeeByIdWithProjects(model.EmployeeId);
            if (entity != null)
            {
                entity.EmployeeName = model.EmployeeName;
                entity.EmployeeDesignation = model.EmployeeDesignation;
                entity.Salary = model.Salary;
                entity.Address.Street = model.Street;
                entity.Address.District = model.District;
                entity.Address.Division = model.Division;
                entity.Hobbies = model.Hobbies.Select(name => new Hobby { HobbyName = name }).ToList();
                entity.EmployeeProjects = model.Projects.Select(id => new EmployeeProject { ProjectId = id }).ToList();

                await _employeeService.EditAsync(entity);

            }
        }
    }
}
