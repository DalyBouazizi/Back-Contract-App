using Projet_Data.ModelsEF;
using Projet_Data.ModelsEF2;
using Projet_Stage.Models;

namespace Projet_Stage.Services.Interfaces
{
    public interface IContractService
    {
        Task<bool> AddContractAsync(ContractModel Contract);
        Task<List<ContractGetModel>> GetAllContractsAsync();
    }
}
