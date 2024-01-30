using EmployeeInfo.Entities.Domain;

namespace EmployeeInfo.Repository.IRepository
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<List<Project>> GetProjectsWithEmployees();
        Task<Project> GetProjectByIdWithEmployees(int id);
    }
}
