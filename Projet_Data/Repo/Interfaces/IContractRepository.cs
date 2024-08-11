using Projet_Data.Abstract;
using Projet_Data.ModelsEF;
using Projet_Data.ModelsEF2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Data.Repo.Interfaces
{
    public interface IContractRepository 
    {

        Task<bool> AddContractAsync(Contract Contract);
        Task<ICollection<Contract>> GetAllContractsAsync();
        Task<bool> DeleteContractAsync(int IdContract);
        Task<Contract> GetContractByIdAsync(int IdContract);
        Task<bool> UpdateContractAsync(Contract Contract);
        Task<List<Contract>> GetContractByTypeAsync(string Type);
        Task<ICollection<Contract>> GetContractsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
