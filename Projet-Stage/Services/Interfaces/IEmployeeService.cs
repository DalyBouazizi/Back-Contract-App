using Projet_Data.Features;
using Projet_Data.ModelsEF;
using Projet_Stage.Models;

namespace Projet_Stage.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetAllEmployeesAsync();
        Task<bool> AddEmployeeAsync(EmployeeModel Employee);
        Task<EmployeeModel> GetEmployeeByIdAsync(int IdEmployee);
        Task<EmployeeModel> GetEmployeeByCinAsync(string Cin);
        Task<bool> DeleteEmployeeAsync(int IdEmployee);
        Task<bool> UpdateEmployeeAsync(EmployeeModel Employee);
        Task<(bool isSuccess, List<int> failedEmployeeIds)> AddListEmployeesAsync(List<EmployeeModel> Employees);
        //Task<(bool isSuccess, List<int> failedEmployeeIds)> UpdateListEmployeesAsync(List<EmployeeModel> Employees);
        Task<List<Employee>> GetEmployeesByPosteAsync(string poste);

        Task<List<Employee>> SortEmployeesByIdAsync(bool ascending);
        Task<List<Employee>> SortEmployeesByPosteAsync(bool ascending);
        Task<List<Employee>> SortEmployeesByFirstNameAsync(bool ascending);
        Task<List<Employee>> SortEmployeesByLastNameAsync(bool ascending);
        Task<List<Employee>> SortEmployeesBySalaryAsync(decimal value, bool over);
        Task<List<Employee>> GetEmployeesByFiltersAsync(FilterCriteria criteria);
    }
}
