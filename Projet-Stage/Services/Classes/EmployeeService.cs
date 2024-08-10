using Projet_Data.ModelsEF;
using Projet_Data.Repo.Classes;
using Projet_Data.Repo.Interfaces;
using Projet_Stage.Models;
using Projet_Stage.Services.Interfaces;

namespace Projet_Stage.Services.Classes
{
    public class EmployeeService : IEmployeeService
    {   
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<bool> AddEmployeeAsync(EmployeeModel Employee)
        {
            bool res = false;
            try
            {


                Employee NewEmployee = new Employee();
                NewEmployee.Matricule = Employee.Matricule;
                NewEmployee.Nom = Employee.Nom;
                NewEmployee.Prenom = Employee.Prenom;
                NewEmployee.Poste = Employee.Poste;
                NewEmployee.Adresse = Employee.Adresse;
                NewEmployee.DateNaissance = Employee.DateNaissance;
                NewEmployee.LieuNaissance   = Employee.LieuNaissance;
                NewEmployee.Cin = Employee.Cin; 
                NewEmployee.DateCin = Employee.DateCin;
                NewEmployee.CategoriePro = Employee.CategoriePro;
                NewEmployee.Salaireb = Employee.Salaireb;
                NewEmployee.Salairen = Employee.Salairen;


                

                res = await _employeeRepository.AddEmployeeAsync(NewEmployee);


                if (res)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<(bool isSuccess, List<int> failedEmployeeIds)> AddListEmployeesAsync(List<EmployeeModel> Employees)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEmployeeAsync(int IdEmployee)
        {
            throw new NotImplementedException();
        }

        public Task<List<EmployeeModel>> GetAllEmployeesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeModel> GetEmployeeByIdAsync(int IdEmployee)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEmployeeAsync(EmployeeModel Employee)
        {
            throw new NotImplementedException();
        }
    }
}
