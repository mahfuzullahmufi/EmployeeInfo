﻿using EmployeeInfo.Entities.Domain;
using EmployeeInfo.Repository.IRepository;

namespace EmployeeInfo.Service.IService
{
    public interface IEmployeeService 
    {
        Task<Employee> GetEmployeeByIdWithProjects(int id);
        Task<List<Employee>> GetAllAsync();
        Task<List<Employee>> GetEmployeesWithProjects();
        Task<bool> AddAsync(Employee entity);
        Task EditAsync(Employee entity);
        Task<bool> DeleteAsync(int id);
    }
}
