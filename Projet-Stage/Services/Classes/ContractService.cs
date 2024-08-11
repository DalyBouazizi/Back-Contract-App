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
                
            }catch(Exception ex)
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
    }
    
}
