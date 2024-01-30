using EmployeeInfo.Web.Models;

namespace EmployeeInfo.Web.Factories
{
    public interface IProjectFactory
    {
        Task<PagedViewModel<ProjectTableModel>> GetProjectsWithEmployees(int page, int pageSize);
        Task<ProjectModel> GetProjectByIdWithEmployees(int id);
        Task AddProjectAsync(ProjectModel model);
        Task EditProjectAsync(ProjectModel model);
    }
}
