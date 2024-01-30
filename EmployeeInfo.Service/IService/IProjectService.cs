using EmployeeInfo.Entities.Domain;

namespace EmployeeInfo.Service.IService
{
    public interface IProjectService
    {
        Task<Project> GetByIdAsync(int id);
        Task<Project> GetProjectByIdWithEmployees(int id);
        Task<List<Project>> GetAllAsync();
        Task<List<Project>> GetProjectsWithEmployees();
        Task<bool> AddAsync(Project entity);
        Task<bool> EditAsync(Project entity);
        Task<bool> DeleteAsync(int id);
    }
}
