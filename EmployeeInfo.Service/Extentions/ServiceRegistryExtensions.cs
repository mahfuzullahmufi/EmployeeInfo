using EmployeeInfo.Service.IService;
using EmployeeInfo.Service.Service;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeInfo.Service.Extentions
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection service)
        {
            return service
                .AddScoped<IEmployeeService, EmployeeService>()
                .AddScoped<IProjectService, ProjectService>();
        }
    }
}
