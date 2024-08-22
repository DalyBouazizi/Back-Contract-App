using Projet_Stage.Models;

namespace Projet_Stage.Services.Interfaces
{
    public interface IAlertService
    {

        Task<bool> CreateAlertAsync(AlertModel alertModel);
        Task<bool> DeleteAlertAsync(int alertId);
        Task<AlertGetModel> GetAlertByIdAsync(int alertId);
        Task<IEnumerable<AlertGetModel>> GetAlertsByContractIdAsync(int contractId);
        Task<IEnumerable<AlertGetModel>> GetAllAlertsAsync();
    }
}
