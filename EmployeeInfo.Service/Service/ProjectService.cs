using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Repository.IRepository;
using EmployeeInfo.Service.IService;

namespace EmployeeInfo.Service.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IGenericRepository<Project> _repository;
        public ProjectService(IGenericRepository<Project> repository)
        {

            _repository = repository;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
