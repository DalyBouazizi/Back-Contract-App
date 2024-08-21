using Projet_Data.ModelsEF;

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
            var employee = await _employeeRepository.GetEmployeeByRealIdAsync(Contract.EmployeeId);
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
                NewContract.Salaireb = Contract.Salaireb;   
                NewContract.Salairen = Contract.Salairen;
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
                        Contract.Salaireb = item.Salaireb;  
                        Contract.Salairen = item.Salairen;

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
                var employee = await _employeeRepository.GetEmployeeByRealIdAsync(contract.EmployeeId);
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
                        existingContract.Salairen = contract.Salairen;
                        existingContract.Salaireb = contract.Salaireb;
                        
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
                    EmployeeId = c.EmployeeId,
                    Salairen = c.Salairen,
                    Salaireb = c.Salaireb
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving contracts: {ex.Message}");
            }
        }

        public async Task<List<ContractModel>> GetContractByEmployeeIdAsync(int EmployeeId)
        {
            var test = await _employeeRepository.GetEmployeeByRealIdAsync(EmployeeId);
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
                    DisplayContract.Salaireb = contract.Salaireb;
                    DisplayContract.Salairen = contract.Salairen;   

                    DisplayContracts.Add(DisplayContract);

                }
                return DisplayContracts;
            }
        }

        public async Task RenewContractAsync(int employeeId, ContractModel newContract)
        {
            // Get the latest contract for the employee
            var latestContract = await _contractRepository.GetLatestContractByEmployeeIdAsync(employeeId);

            if (latestContract == null)
            {
                throw new Exception("No existing contracts found for the employee.");
            }

            // Optionally update the end date of the old contract (if required)
            latestContract.DateFin = DateTime.UtcNow; // or any other date that makes sense
            await _contractRepository.UpdateContractAsync(latestContract);


            //convert from ContractModel to Contract from library : 
            Contract FinalContract = new Contract();

            FinalContract.EmployeeId = employeeId;
            FinalContract.Datedeb = newContract.Datedeb;
            FinalContract.DateFin = newContract.DateFin;
            FinalContract.Type = newContract.Type;
            FinalContract.Salaireb = newContract.Salaireb;
            FinalContract.Salairen = newContract.Salairen;


            // Add the new contract without altering its start date
            await _contractRepository.AddContractAsync(FinalContract);
        }

        public async Task<List<ContractGetModel>> GetLatestContractsAsync()
        {
            List<ContractGetModel> Contracts = new List<ContractGetModel>();
            try
            {
                var res = await _contractRepository.GetLatestContractsAsync();
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
                        Contract.Salaireb = item.Salaireb;
                        Contract.Salairen = item.Salairen;


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

        public async Task<ContractGetModel> GetLatestContractByEmployeeIdAsync(int employeeId)
        {
            try
            {
                var res = await _contractRepository.GetLatestContractByEmployeeIdAsync(employeeId);
                if (res == null)
{
                    return null;
                }
                else { 
                ContractGetModel Contract = new ContractGetModel();
                Contract.id = res.Idcontrat;
                Contract.Type = res.Type;
                Contract.DateFin = res.DateFin;
                Contract.Datedeb = res.Datedeb;
                Contract.EmployeeId = res.EmployeeId;
                Contract.Salaireb = res.Salaireb;
                Contract.Salairen = res.Salairen;

                return Contract;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
          
            
            
            
        }

        public async Task<List<ContractGetModel>> GetContractsEndingInOneMonthAsync()
        {
            try
            {

                List<ContractGetModel> Contracts = new List<ContractGetModel>();
                try
                {
                    var res = await _contractRepository.GetContractsEndingInOneMonthAsync();
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
                            Contract.Salaireb = item.Salaireb;
                            Contract.Salairen = item.Salairen;

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
    

