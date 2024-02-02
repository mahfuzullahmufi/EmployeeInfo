using EmployeeInfo.Repository.Data;
using EmployeeInfo.Repository.IRepository;
using EmployeeInfo.Repository.Repository;
using LinqToDB;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeInfo.Repository.Extentions
{
    public static class RepositoryRegistryExtentions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddLinqToDBContext<Linq2DbDataConnection>((provider, options) => options
            .UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            .UseDefaultLogging(provider));

            return service
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddScoped(typeof(ILinq2DbRepository<>), typeof(Linq2DbRepository<>))
                .AddScoped<IEmployeeRepository, EmployeeRepository>()
                .AddScoped<ILinq2DbEmployeeRepo, Linq2DbEmployeeRepo>()
                .AddScoped<IProjectRepository, ProjectRepository>();
        }
    }
}
