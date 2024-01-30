using AutoMapper;
using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Service.IService;
using EmployeeInfo.Service.Service;
using EmployeeInfo.Web.Models;

namespace EmployeeInfo.Web.Factories
{
    public class ProjectFactory : IProjectFactory
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        public ProjectFactory(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;
        }

        public async Task<ProjectModel> GetProjectByIdWithEmployees(int id)
        {
            var project = await _projectService.GetProjectByIdWithEmployees(id);

            ProjectModel ProjectModel = new ProjectModel();
            ProjectModel.ProjectId = project.ProjectId;
            ProjectModel.ProjectName = project.ProjectName;
            ProjectModel.Employees = project.EmployeeProjects.Select(name => name.Employee.EmployeeId).ToList();

            return ProjectModel;
        }

        public async Task<PagedViewModel<ProjectTableModel>> GetProjectsWithEmployees(int page, int pageSize)
        {
            PagedViewModel<ProjectTableModel> model = new PagedViewModel<ProjectTableModel>();
            var projectList = await _projectService.GetProjectsWithEmployees();

            List<ProjectTableModel> projectTableModelList = projectList
            .Select(e => new ProjectTableModel
            {
                ProjectId = e.ProjectId,
                ProjectName = e.ProjectName,
                Employees = string.Join(", ", e.EmployeeProjects.Select(obj => obj.Employee.EmployeeName)),
            }).ToList();

            model.CurrentPage = page;
            model.PageSize = pageSize;
            model.TotalCount = projectList.Count();
            model.TotalPages = (int)Math.Ceiling((double)model.TotalCount / pageSize);
            model.PagedData = projectTableModelList.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return model;
        }

        public async Task AddProjectAsync(ProjectModel model)
        {
            Project entity = new Project
            {
                ProjectName = model.ProjectName,
                EmployeeProjects = model.Employees.Select(id => new EmployeeProject { EmployeeId = id }).ToList()
            };

            var result = await _projectService.AddAsync(entity);
        }

        public async Task EditProjectAsync(ProjectModel model)
        {
            var entity = await _projectService.GetProjectByIdWithEmployees(model.ProjectId);
            if (entity != null)
            {
                entity.ProjectName = model.ProjectName;
                entity.EmployeeProjects = model.Employees.Select(id => new EmployeeProject { EmployeeId = id }).ToList();

                await _projectService.EditAsync(entity);
            }
        }
    }
}
