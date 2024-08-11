using Projet_Data.ModelsEF;
using Projet_Data.ModelsEF2;
using Projet_Stage.Models;

namespace Projet_Stage.Services.Interfaces
{
    public interface IContractService
    {
        Task<bool> AddContractAsync(ContractModel Contract);
        Task<List<ContractGetModel>> GetAllContractsAsync();
        Task<bool> DeleteContractAsync(int IdContract);
        Task<bool> UpdateContractAsync(ContractGetModel contract);
        Task<List<Contract>> GetContractByType(string Type);
        Task<List<ContractGetModel>> GetContractsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
