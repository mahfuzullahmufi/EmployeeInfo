using EmployeeInfo.Entities.Domain;

namespace EmployeeInfo.Service.IService
{
    public interface IProjectService
    {
        Task<Project> GetByIdAsync(int id);
        Task<List<Project>> GetAllAsync();
        //Task<Employee> AddAsync(Employee entity);
        //Task EditAsync(Employee entity);
        //Task<bool> DeleteAsync(int id);
    }
}
