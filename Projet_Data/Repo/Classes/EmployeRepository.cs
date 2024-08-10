using Projet_Data.Abstract;
using Projet_Data.Repo.Interfaces;
using Projet_Data.ModelsEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Projet_Data.Repo.Classes
{
    public class EmployeRepository : IEmployeeRepository
    {
        private readonly DataContext _context;
        private readonly IRepository<Employee> _repository;

        //constructor
        public EmployeRepository(DataContext context, IRepository<Employee> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<bool> AddEmployeeAsync(Employee Employee)
        {
            var test = await GetEmployeeByIdAsync(Employee.Matricule);
            if (test == null)
            {
                try
                {
                    await _repository.AddEntity(Employee);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> AddListEmployeesAsync(List<Employee> Employees)
        {
            try
            {
                return await _repository.AddListEntity(Employees);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteEmployeeAsync(int IdEmployee)
        {
            try
            {
                var employee = await GetEmployeeByIdAsync(IdEmployee);
                if (employee != null)
                {
                    await _repository.DeleteEntity(employee);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<Employee>> GetAllEmployeesAsync()
        {
            try
            {
                var ListEmployee = await _repository.GetAllEntity();
                return ListEmployee;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Employee> GetEmployeeByIdAsync(int IdEmployee)
        {
            Employee Employee;
            Employee = await _context.Employees
                .Where(u => u.Matricule.Equals(IdEmployee)).FirstOrDefaultAsync();
            return Employee;
        }

        public async Task<bool> UpdateEmployeeAsync(Employee Employee)
        {
            throw new NotImplementedException();
        }
    }
}
