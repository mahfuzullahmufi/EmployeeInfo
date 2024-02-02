using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Repository.Data;
using EmployeeInfo.Repository.IRepository;
using EmployeeInfo.Service.IService;
using LinqToDB;

namespace EmployeeInfo.Service.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ILinq2DbRepository<Employee> _employeeRepository;
        private readonly ILinq2DbRepository<Address> _addressRepository;
        private readonly ILinq2DbRepository<Hobby> _hobbyRepository;
        private readonly ILinq2DbRepository<EmployeeProject> _employeeProjectRepository;
        private readonly ILinq2DbRepository<Project> _projectRepository;
        private readonly Linq2DbDataConnection _connection;
        public EmployeeService(ILinq2DbRepository<Employee> employeeRepository, ILinq2DbRepository<Address> addressRepository, ILinq2DbRepository<Hobby> hobbyRepository, ILinq2DbRepository<EmployeeProject> employeeProjectRepository, ILinq2DbRepository<Project> projectRepository, Linq2DbDataConnection connection)
        {
            _employeeRepository = employeeRepository;
            _addressRepository = addressRepository;
            _hobbyRepository = hobbyRepository;
            _employeeProjectRepository = employeeProjectRepository;
            _projectRepository = projectRepository;
            _connection = connection;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();

        }

        public async Task<Employee> GetEmployeeByIdWithProjects(int id)
        {
            var employeeProjects = _employeeProjectRepository.Table.Join(_projectRepository.Table, ep => ep.ProjectId, p => p.ProjectId, (ep, p) => new { EmployeeProject = ep, Project = p })
                .Select(x => new EmployeeProject
                {
                    EmployeeId = x.EmployeeProject.EmployeeId,
                    ProjectId = x.EmployeeProject.ProjectId,
                    Project = x.Project
                });

            var query = _employeeRepository.Table.Where(x => x.EmployeeId == id)
                .Join(_addressRepository.Table, e => e.EmployeeId, a => a.EmployeeId, (e, a) => new { Employee = e, Address = a })
                .GroupJoin(_hobbyRepository.Table, e => e.Employee.EmployeeId, h => h.EmployeeId, (e, h) => new { Employee = e.Employee, Address = e.Address, Hobbies = h })
                .GroupJoin(employeeProjects, e => e.Employee.EmployeeId, ep => ep.EmployeeId, (e, ep) => new { Employee = e.Employee, Address = e.Address, Hobbies = e.Hobbies, EmployeeProjects = ep })
                .Select(x => new Employee
                {
                    EmployeeId = x.Employee.EmployeeId,
                    EmployeeName = x.Employee.EmployeeName,
                    EmployeeDesignation = x.Employee.EmployeeDesignation,
                    Salary = x.Employee.Salary,
                    Address = x.Address,
                    Hobbies = x.Hobbies.ToList(),
                    EmployeeProjects = x.EmployeeProjects.ToList()
                });

            var result = await query.FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<Employee>> GetEmployeesWithProjects()
        {
            var employeeProjects = _employeeProjectRepository.Table.Join(_projectRepository.Table, ep => ep.ProjectId, p => p.ProjectId, (ep, p) => new { EmployeeProject = ep, Project = p })
                .Select(x => new EmployeeProject
                {
                    EmployeeId = x.EmployeeProject.EmployeeId,
                    ProjectId = x.EmployeeProject.ProjectId,
                    Project = x.Project
                });

            var query = _employeeRepository.Table
                .Join(_addressRepository.Table, e => e.EmployeeId, a => a.EmployeeId, (e, a) => new { Employee = e, Address = a })
                .GroupJoin(_hobbyRepository.Table, e => e.Employee.EmployeeId, h => h.EmployeeId, (e, h) => new { Employee = e.Employee, Address = e.Address, Hobbies = h })
                .GroupJoin(employeeProjects, e => e.Employee.EmployeeId, ep => ep.EmployeeId, (e, ep) => new { Employee = e.Employee, Address = e.Address, Hobbies = e.Hobbies, EmployeeProjects = ep })
                .Select(x => new Employee
                {
                    EmployeeId = x.Employee.EmployeeId,
                    EmployeeName = x.Employee.EmployeeName,
                    EmployeeDesignation = x.Employee.EmployeeDesignation,
                    Salary = x.Employee.Salary,
                    Address = x.Address,
                    Hobbies = x.Hobbies.ToList(),
                    EmployeeProjects = x.EmployeeProjects.ToList()
                });

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _employeeRepository.FirstOrDefaultAsync(x => x.EmployeeId == id);
        }

        public async Task<bool> AddAsync(Employee entity)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    await _employeeRepository.InsertAsync(entity);
                    var employeeId = _employeeRepository.Table.Max(x => x.EmployeeId);

                    entity.Address.EmployeeId = employeeId;
                    await _addressRepository.InsertAsync(entity.Address);

                    foreach (var hobby in entity.Hobbies)
                    {
                        hobby.EmployeeId = employeeId;
                        await _hobbyRepository.InsertAsync(hobby);
                    }

                    foreach (var employeeProject in entity.EmployeeProjects)
                    {
                        employeeProject.EmployeeId = employeeId;
                        await _employeeProjectRepository.InsertAsync(employeeProject);
                    }
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task EditAsync(Employee entity)
        {

            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    await _employeeRepository.UpdateAsync(entity);

                    await _addressRepository.UpdateAsync(entity.Address);

                    _hobbyRepository.Table.Where(x => x.EmployeeId == entity.EmployeeId).Delete();

                    foreach (var hobby in entity.Hobbies)
                    {
                        hobby.EmployeeId = entity.EmployeeId;
                        await _hobbyRepository.InsertAsync(hobby);
                    }

                    _employeeProjectRepository.Table.Where(x => x.EmployeeId == entity.EmployeeId).Delete();

                    foreach (var employeeProject in entity.EmployeeProjects)
                    {
                        employeeProject.EmployeeId = entity.EmployeeId;
                        await _employeeProjectRepository.InsertAsync(employeeProject);
                    }
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    _employeeRepository.Table.Where(x => x.EmployeeId == id).Delete();

                    _addressRepository.Table.Where(x => x.EmployeeId == id).Delete();

                    _hobbyRepository.Table.Where(x => x.EmployeeId == id).Delete();

                    _employeeProjectRepository.Table.Where(x => x.EmployeeId == id).Delete();

                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
