using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Repository.Data;
using EmployeeInfo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInfo.Repository.Repository
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Project> GetProjectByIdWithEmployees(int id)
        {
            return await _db.Include(x => x.EmployeeProjects).ThenInclude(y => y.Employee).FirstOrDefaultAsync(x => x.ProjectId == id); ;
        }

        public async Task<List<Project>> GetProjectsWithEmployees()
        {
            return await _db.Include(x => x.EmployeeProjects).ThenInclude(y => y.Employee).OrderBy(x => x.ProjectName).ToListAsync();
        }
    }
}
