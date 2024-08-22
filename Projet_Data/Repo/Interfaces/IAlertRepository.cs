using Projet_Data.ModelsEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Data.Repo.Interfaces
{
    public interface IAlertRepository
    {
        Task<bool> CreateAlertAsync(Alert alert);
        Task<bool> DeleteAlertAsync(int alertId);
        Task<Alert> GetAlertByIdAsync(int alertId);
        Task<IEnumerable<Alert>> GetAlertsByContractIdAsync(int contractId);
        Task<IEnumerable<Alert>> GetAllAlertsAsync();

        Task<bool> DeleteAlertsByContractId(int contractId);
    }
}
