using AutoMapper;

namespace EmployeeInfo.Web.Setup
{
    public static class AutoMapperConfig
    {
        public static void MapEntities(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
