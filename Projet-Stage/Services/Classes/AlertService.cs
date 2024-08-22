using Projet_Data.ModelsEF;
using Projet_Data.Repo.Interfaces;
using Projet_Stage.Models;
using Projet_Stage.Services.Interfaces;

namespace Projet_Stage.Services.Classes
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IContractRepository _contractRepository;

        public AlertService(IAlertRepository alertRepository,IContractRepository contractRepository)
        {
            _alertRepository = alertRepository;
            _contractRepository = contractRepository;
        }

        public async Task<bool> CreateAlertAsync(AlertModel alertModel)
        {
            var test = await _contractRepository.GetContractByIdAsync(alertModel.ContractId);
            if (test == null) {
                
                return false;
            }
            else
            {
                var alert = new Alert
                {
                    AlertDate = alertModel.AlertDate,
                    ContractId = alertModel.ContractId
                };
                return await _alertRepository.CreateAlertAsync(alert);
            }
           
        }

        public async Task<bool> DeleteAlertAsync(int alertId)
        {
            return await _alertRepository.DeleteAlertAsync(alertId);
        }

        public async Task<AlertGetModel> GetAlertByIdAsync(int alertId)
        {
            var alert = await _alertRepository.GetAlertByIdAsync(alertId);
            if (alert == null) return null;
            return new AlertGetModel
            {
                AlertId = alert.AlertId,
                AlertDate = alert.AlertDate,
                ContractId = alert.ContractId
            };
        }

        public async Task<IEnumerable<AlertGetModel>> GetAlertsByContractIdAsync(int contractId)
        {
            var alerts = await _alertRepository.GetAlertsByContractIdAsync(contractId);
            return alerts.Select(a => new AlertGetModel
            {
                AlertId = a.AlertId,
                AlertDate = a.AlertDate,
                ContractId = a.ContractId
            });
        }

        public async Task<IEnumerable<AlertGetModel>> GetAllAlertsAsync()
        {
            var alerts = await _alertRepository.GetAllAlertsAsync();
            return alerts.Select(a => new AlertGetModel
            {
                AlertId = a.AlertId,
                AlertDate = a.AlertDate,
                ContractId = a.ContractId
            });
        }
    }
}
