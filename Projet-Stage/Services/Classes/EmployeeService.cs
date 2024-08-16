using Projet_Data.Features;
using Projet_Data.ModelsEF;
using Projet_Data.Repo.Classes;
using Projet_Data.Repo.Interfaces;
using Projet_Stage.Models;
using Projet_Stage.Services.Interfaces;
using System.Data;

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

        public async Task<(bool isSuccess, List<int> failedEmployeeIds)> AddListEmployeesAsync(List<EmployeeModel> Employees)
        {
            var failedIds = new List<int>();
            var employeeEntities = new List<Employee>();
            foreach (var emp in Employees)
            {
                var existingemp = await _employeeRepository.GetEmployeeByIdAsync(emp.Matricule);
                if (existingemp == null)
                {
                    employeeEntities.Add(new Employee
                    {
                        Matricule = emp.Matricule,
                        Nom = emp.Nom,
                        Prenom = emp.Prenom,
                        Poste = emp.Poste,
                        Adresse = emp.Adresse,
                        DateNaissance = emp.DateNaissance,
                        LieuNaissance = emp.LieuNaissance,
                        Cin = emp.Cin,
                        DateCin = emp.DateCin,
                        CategoriePro = emp.CategoriePro,
                        Salaireb = emp.Salaireb,
                        Salairen = emp.Salairen

                    });
                }
                else
                {
                    failedIds.Add(emp.Matricule);
                }
            }
            if (employeeEntities.Count > 0)
            {
                var result = await _employeeRepository.AddListEmployeesAsync(employeeEntities);
                return (result, failedIds);
            }
            return (false, failedIds);
        }

        public async Task<bool> DeleteEmployeeAsync(int IdEmployee)
        {
            try
            {
                var res = await _employeeRepository.DeleteEmployeeAsync(IdEmployee);
               return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EmployeeModel>> GetAllEmployeesAsync()
        {
            List<EmployeeModel> employees = new List<EmployeeModel>();
            try
            {
                var res = await _employeeRepository.GetAllEmployeesAsync();
                if (res == null)
                {
                    return null;
                }
                else
                {
                    foreach (var item in res)
                    {
                        EmployeeModel employee = new EmployeeModel();
                        employee.Matricule = item.Matricule;
                        employee.Nom = item.Nom;
                        employee.Prenom = item.Prenom;
                        employee.Poste = item.Poste;
                        employee.Adresse = item.Adresse;
                        employee.DateNaissance = item.DateNaissance;
                        employee.Cin = item.Cin;
                        employee.LieuNaissance = item.LieuNaissance;
                        employee.DateCin = item.DateCin;
                        employee.CategoriePro = item.CategoriePro;
                        employee.Salaireb = item.Salaireb;
                        employee.Salairen = item.Salairen;
                        employees.Add(employee);
                    }
                    return await Task.FromResult(employees);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeModel> GetEmployeeByCinAsync(string Cin)
        {
            try
            {

                var res = await _employeeRepository.GetEmployeeByCinAsync(Cin);
                if (res == null)
                {

                    return null;
                }
                else
                {
                    EmployeeModel employee = new EmployeeModel();
                    employee.Matricule = res.Matricule;
                    employee.Nom = res.Nom;
                    employee.Prenom = res.Prenom;
                    employee.Poste = res.Poste;
                    employee.Adresse = res.Adresse;
                    employee.DateNaissance = res.DateNaissance;
                    employee.Cin = res.Cin;
                    employee.LieuNaissance = res.LieuNaissance;
                    employee.DateCin = res.DateCin;
                    employee.CategoriePro = res.CategoriePro;
                    employee.Salaireb = res.Salaireb;
                    employee.Salairen = res.Salairen;

                    return await Task.FromResult(employee);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeModel> GetEmployeeByIdAsync(int IdEmployee)
        {

            try
            {

                var res = await _employeeRepository.GetEmployeeByIdAsync(IdEmployee);
                if (res == null)
                {

                    return null;
                }
                else
                {
                    EmployeeModel employee = new EmployeeModel();
                    employee.Matricule = res.Matricule;
                    employee.Nom = res.Nom;
                    employee.Prenom = res.Prenom;
                    employee.Poste = res.Poste;
                    employee.Adresse = res.Adresse;
                    employee.DateNaissance = res.DateNaissance;
                    employee.Cin = res.Cin;
                    employee.LieuNaissance = res.LieuNaissance;
                    employee.DateCin = res.DateCin;
                    employee.CategoriePro = res.CategoriePro;
                    employee.Salaireb = res.Salaireb;
                    employee.Salairen = res.Salairen;

                    return await Task.FromResult(employee);

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Employee>> GetEmployeesByFiltersAsync(FilterCriteria criteria)
        {
            try
            {
                return await _employeeRepository.GetEmployeesByFilterAsync(criteria);

            }catch(Exception ex)
            {
                throw ex;
            }   
        }

        public async Task<List<Employee>> GetEmployeesByPosteAsync(string poste)
        {
            return await _employeeRepository.GetEmployeesByPosteAsync(poste);
        }

        public async Task<List<Employee>> SortEmployeesByFirstNameAsync(bool ascending =true)
        {
            return await _employeeRepository.SortEmployeesByFirstNameAsync(ascending=true );
        }

        public async Task<List<Employee>> SortEmployeesByIdAsync(bool ascending = true)
        {
            return await _employeeRepository.SortEmployeesByIdAsync(ascending);
        }

        public async Task<List<Employee>> SortEmployeesByLastNameAsync(bool ascending = true)
        {
            return await _employeeRepository.SortEmployeesByLastNameAsync(ascending =true );
        }

        public async Task<List<Employee>> SortEmployeesByPosteAsync(bool ascending = true)
        {
            return await _employeeRepository.SortEmployeesByPosteAsync(ascending);
        }

        public async Task<List<Employee>> SortEmployeesBySalaryAsync(decimal value, bool over = true)
        {
            return await _employeeRepository.SortEmployeesBySalaryAsync(value, over);
        }

        public async Task<bool> UpdateEmployeeAsync(EmployeeModel employee)
        {
            try
            {
                // Find the employee by Matricule
                var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(employee.Matricule);
                if (existingEmployee != null)
                {
                    // Update the properties of the employee entity
                    existingEmployee.Nom = employee.Nom;
                    existingEmployee.Prenom = employee.Prenom;
                    existingEmployee.Poste = employee.Poste;
                    existingEmployee.Adresse = employee.Adresse;
                    existingEmployee.DateNaissance = employee.DateNaissance;
                    existingEmployee.LieuNaissance = employee.LieuNaissance;
                    existingEmployee.Cin = employee.Cin;
                    existingEmployee.DateCin = employee.DateCin;
                    existingEmployee.CategoriePro = employee.CategoriePro;
                    existingEmployee.Salaireb = employee.Salaireb;
                    existingEmployee.Salairen = employee.Salairen;

                    // Call repository to update the employee
                    return await _employeeRepository.UpdateEmployeeAsync(existingEmployee);
                }
                else
                {
                    return false; // Employee not found
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
