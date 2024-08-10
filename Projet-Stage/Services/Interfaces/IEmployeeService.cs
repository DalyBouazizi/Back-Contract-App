using Projet_Stage.Models;

namespace Projet_Stage.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeModel>> GetAllEmployeesAsync();
        Task<bool> AddEmployeeAsync(EmployeeModel Employee);
        Task<EmployeeModel> GetEmployeeByIdAsync(int IdEmployee);
        Task<bool> DeleteEmployeeAsync(int IdEmployee);
        Task<bool> UpdateEmployeeAsync(EmployeeModel Employee);
        Task<(bool isSuccess, List<int> failedEmployeeIds)> AddListEmployeesAsync(List<EmployeeModel> Employees);
        //Task<(bool isSuccess, List<int> failedEmployeeIds)> UpdateListEmployeesAsync(List<EmployeeModel> Employees);
    }
}
