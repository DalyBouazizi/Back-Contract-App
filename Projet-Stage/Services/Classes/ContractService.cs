using Projet_Data.ModelsEF;
using Projet_Data.ModelsEF2;
using Projet_Data.Repo.Classes;
using Projet_Data.Repo.Interfaces;
using Projet_Stage.Models;
using Projet_Stage.Services.Interfaces;


namespace Projet_Stage.Services.Classes
{
    public class ContractService : IContractService

    {
        private readonly IContractRepository _contractRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public ContractService(IContractRepository contractRepository, IEmployeeRepository employeeRepository)
        {
            _contractRepository = contractRepository;
            _employeeRepository = employeeRepository;
        }
        public async Task<bool> AddContractAsync(ContractModel Contract)
        {
            // Retrieve the EmployeeId based on Matricule
            var employee = await _employeeRepository.GetEmployeeByIdAsync(Contract.EmployeeId);
            if (employee == null)
            {
                return false;
            }
            try
            {
                Contract NewContract = new Contract();

                NewContract.EmployeeId = employee.Id;
                NewContract.Datedeb = Contract.Datedeb;
                NewContract.DateFin = Contract.DateFin;
                NewContract.Type = Contract.Type;

                var res = await _contractRepository.AddContractAsync(NewContract);
                return res;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteContractAsync(int IdContract)
        {
            try
            {
                return await _contractRepository.DeleteContractAsync(IdContract);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ContractGetModel>> GetAllContractsAsync()
        {
            List<ContractGetModel> Contracts = new List<ContractGetModel>();
            try
            {
                var res = await _contractRepository.GetAllContractsAsync();
                if (res == null)
                {
                    return null;
                }
                else
                {
                    foreach (var item in res)
                    {
                        ContractGetModel Contract = new ContractGetModel();
                        Contract.id = item.Idcontrat;
                        Contract.Type = item.Type;
                        Contract.DateFin = item.DateFin;
                        Contract.Datedeb = item.Datedeb;
                        Contract.EmployeeId = item.EmployeeId;

                        Contracts.Add(Contract);
                    }
                    return await Task.FromResult(Contracts);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Contract>> GetContractByType(string Type)
        {
            return await _contractRepository.GetContractByTypeAsync(Type);
        }

        public async Task<bool> UpdateContractAsync(ContractGetModel contract)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(contract.EmployeeId);
                if (employee == null)
                {
                    return false;
                }
                else
                {
                    var existingContract = await _contractRepository.GetContractByIdAsync(contract.id);
                    if (existingContract != null)
                    {
                        existingContract.Type = contract.Type;
                        existingContract.Datedeb = contract.Datedeb;
                        existingContract.DateFin = contract.DateFin;
                        existingContract.EmployeeId = contract.EmployeeId;
                        return await _contractRepository.UpdateContractAsync(existingContract);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ContractGetModel>> GetContractsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                var contracts = await _contractRepository.GetContractsByDateRangeAsync(startDate, endDate);

                return contracts.Select(c => new ContractGetModel
                {
                    id = c.Idcontrat,
                    Type = c.Type,
                    Datedeb = c.Datedeb,
                    DateFin = c.DateFin,
                    EmployeeId = c.EmployeeId
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving contracts: {ex.Message}");
            }
        }

        public async Task<List<ContractModel>> GetContractByEmployeeIdAsync(int EmployeeId)
        {
            var test = await _employeeRepository.GetEmployeeByIdAsync(EmployeeId);
            if(test == null)
            {
                return null;
            }
            else
            {
                var Contracts =  await _contractRepository.GetContractByEmployeeIdAsync(EmployeeId);
                List<ContractModel> DisplayContracts = new List<ContractModel>();
                foreach(var contract in Contracts) {
                    ContractModel DisplayContract = new ContractModel();
                    DisplayContract.EmployeeId = contract.EmployeeId;
                    DisplayContract.Datedeb = contract.Datedeb;
                    DisplayContract.DateFin = contract.DateFin;
                    DisplayContract.Type = contract.Type;
                    DisplayContracts.Add(DisplayContract);

                }
                return DisplayContracts;
            }
        }
    }
}
    

