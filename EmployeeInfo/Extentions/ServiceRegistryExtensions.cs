using EmployeeInfo.Repository.IRepository;
using EmployeeInfo.Repository.Repository;
using EmployeeInfo.Service.IService;
using EmployeeInfo.Service.Service;
using EmployeeInfo.Web.Factories.Implementions;
using EmployeeInfo.Web.Factories.Interfaces;
using EmployeeInfo.Web.Setup;

namespace EmployeeInfo.Web.Extentions
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection service)
        {
            service
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddScoped<IEmployeeService, EmployeeService>()
                .AddScoped<IProjectService, ProjectService>();

            service.AddScoped<IEmployeeFactory, EmployeeFactory>();

            service.MapEntities();

            return service;
        }
    }
}
