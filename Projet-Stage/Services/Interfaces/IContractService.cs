using Projet_Data.ModelsEF;

using Projet_Stage.Models;

namespace Projet_Stage.Services.Interfaces
{
    public interface IContractService
    {
        Task<bool> AddContractAsync(ContractModel Contract);
        Task<List<ContractGetModel>> GetAllContractsAsync();

        Task<List<ContractGetModel>> GetLatestContractsAsync();
        Task<bool> DeleteContractAsync(int IdContract);
        Task<bool> UpdateContractAsync(ContractGetModel contract);
        Task<List<Contract>> GetContractByType(string Type);
        Task<List<ContractGetModel>> GetContractsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<ContractModel>> GetContractByEmployeeIdAsync(int EmployeeId);
        Task RenewContractAsync(int employeeId, ContractModel newContract);
    }
}
