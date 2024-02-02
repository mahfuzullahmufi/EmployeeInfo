using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Repository.Data;
using EmployeeInfo.Repository.IRepository;
using LinqToDB;

namespace EmployeeInfo.Repository.Repository
{
    public class Linq2DbEmployeeRepo : ILinq2DbEmployeeRepo
    {
        private Linq2DbDataConnection _connection;

        public Linq2DbEmployeeRepo(Linq2DbDataConnection connection)
        {
            _connection = connection;
        }

        public async Task<Employee> GetEmployeeByIdWithProjects(int id)
        {
            var query = from e in _connection.Employees
                        join a in _connection.Addresses on e.EmployeeId equals a.EmployeeId
                        join h in _connection.Hobbies on e.EmployeeId equals h.EmployeeId into hobbies
                        join ep in _connection.EmployeeProjects.LoadWith(x => x.Project) on e.EmployeeId equals ep.EmployeeId into employeeProjects
                        where e.EmployeeId == id
                        select new Employee
                        {
                            EmployeeId = e.EmployeeId,
                            EmployeeName = e.EmployeeName,
                            EmployeeDesignation = e.EmployeeDesignation,
                            Salary = e.Salary,
                            Address = new Address { Id = a.Id, Street = a.Street, District = a.District, Division = a.Division, EmployeeId = a.EmployeeId },
                            Hobbies = hobbies.ToList(),
                            EmployeeProjects = employeeProjects.ToList(),
                        };

            return query.FirstOrDefault();
        }

        public async Task<List<Employee>> GetEmployeesWithProjects()
        {
            var query = from e in _connection.Employees
                        join a in _connection.Addresses on e.EmployeeId equals a.EmployeeId
                        join h in _connection.Hobbies on e.EmployeeId equals h.EmployeeId into hobbies
                        join ep in _connection.EmployeeProjects.LoadWith(x => x.Project) on e.EmployeeId equals ep.EmployeeId into employeeProjects
                        select new Employee
                        {
                            EmployeeId = e.EmployeeId,
                            EmployeeName = e.EmployeeName,
                            EmployeeDesignation = e.EmployeeDesignation,
                            Salary = e.Salary,
                            Address = new Address { Id = a.Id, Street = a.Street, District = a.District, Division = a.Division, EmployeeId = a.EmployeeId },
                            Hobbies = hobbies.ToList(),
                            EmployeeProjects = employeeProjects.ToList(),
                        };

            return query.ToList();
        }

        public async Task<bool> AddAsync(Employee entity)
        {
            using (var transaction = _connection.BeginTransaction())
            {
                try
                {
                    _connection.Insert(entity);
                    var employeeId = _connection.Employees.Max(e => e.EmployeeId);

                    entity.Address.EmployeeId = employeeId;
                    _connection.Insert(entity.Address);

                    foreach (var hobby in entity.Hobbies)
                    {
                        hobby.EmployeeId = employeeId;
                        _connection.Insert(hobby);
                    }

                    foreach (var employeeProject in entity.EmployeeProjects)
                    {
                        employeeProject.EmployeeId = employeeId;
                        _connection.Insert(employeeProject);
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
                    _connection.Update(entity);

                    _connection.Update(entity.Address);

                    _connection.Hobbies.Where(x => x.EmployeeId == entity.EmployeeId).Delete();
                    foreach (var hobby in entity.Hobbies)
                    {
                        hobby.EmployeeId = entity.EmployeeId;
                        _connection.Insert(hobby);
                    }

                    _connection.EmployeeProjects.Where(x => x.EmployeeId == entity.EmployeeId).Delete();
                    foreach (var employeeProject in entity.EmployeeProjects)
                    {
                        employeeProject.EmployeeId = entity.EmployeeId;
                        _connection.Insert(employeeProject);
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
                    _connection.Employees.Where(x => x.EmployeeId == id).Delete();

                    _connection.Addresses.Where(x => x.EmployeeId == id).Delete();

                    _connection.Hobbies.Where(x => x.EmployeeId == id).Delete();

                    _connection.EmployeeProjects.Where(x => x.EmployeeId == id).Delete();

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

