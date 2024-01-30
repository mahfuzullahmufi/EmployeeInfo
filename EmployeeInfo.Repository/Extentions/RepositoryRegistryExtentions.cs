using EmployeeInfo.Repository.IRepository;
using EmployeeInfo.Repository.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeInfo.Repository.Extentions
{
    public static class RepositoryRegistryExtentions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection service)
        {
            return service
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddScoped<IEmployeeRepository, EmployeeRepository>()
                .AddScoped<IProjectRepository, ProjectRepository>();
        }
    }
}
