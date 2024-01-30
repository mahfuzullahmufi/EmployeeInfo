using EmployeeInfo.Web.Factories;

namespace EmployeeInfo.Web.Extentions
{
    public static class ServiceRegistryExtensions
    {
        public static IServiceCollection RegisterFactories(this IServiceCollection service)
        {
            return service
                .AddScoped<IEmployeeFactory, EmployeeFactory>()
                .AddScoped<IProjectFactory, ProjectFactory>();
        }
    }
}
