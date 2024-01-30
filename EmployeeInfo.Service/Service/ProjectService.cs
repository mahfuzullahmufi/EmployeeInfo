using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Repository.IRepository;
using EmployeeInfo.Service.IService;

namespace EmployeeInfo.Service.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;
        public ProjectService(IProjectRepository repository)
        {

            _repository = repository;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
        
        public async Task<List<Project>> GetProjectsWithEmployees()
        {
            return await _repository.GetProjectsWithEmployees();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Project> GetProjectByIdWithEmployees(int id)
        {
            return await _repository.GetProjectByIdWithEmployees(id);
        }

        public async Task<bool> AddAsync(Project entity)
        {
            await _repository.Insert(entity);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditAsync(Project entity)
        {
            await _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result != null)
            {
                await _repository.Delete(result);
                await _repository.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
