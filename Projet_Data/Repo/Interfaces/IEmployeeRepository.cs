using Projet_Data.Abstract;
using Projet_Data.ModelsEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Data.Repo.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<ICollection<Employee>> GetAllEmployeesAsync();

        Task<bool> AddEmployeeAsync(Employee Employee);
        Task<Employee> GetEmployeeByIdAsync(int IdEmployee);
        Task<bool> DeleteEmployeeAsync(int IdEmployee);
        Task<bool> UpdateEmployeeAsync(Employee Employee);
        Task<bool> AddListEmployeesAsync(List<Employee> Employee);



    }
}
