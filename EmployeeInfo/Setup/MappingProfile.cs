using AutoMapper;
using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Web.Models;

namespace EmployeeInfo.Web.Setup
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<Address, AddressModel>().ReverseMap();
            CreateMap<Hobby, HobbyModel>().ReverseMap();
            CreateMap<Project, ProjectModel>().ReverseMap();

        }
    }
}
