using Microsoft.EntityFrameworkCore;
using Projet_Data.Abstract;
using Projet_Data.ModelsEF;
using Projet_Data.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Data.Repo.Classes
{
    public class AlertRepository : IAlertRepository
    {
        private readonly DataContext _context;
        private readonly IRepository<Alert> _repository;

        public AlertRepository(DataContext context, IRepository<Alert> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<bool> CreateAlertAsync(Alert alert)
        {
            try
            {
               await _repository.AddEntity(alert);
                return true;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<bool> DeleteAlertAsync(int alertId)
        {
            var alert = await GetAlertByIdAsync(alertId);
            if (alert != null)
            {
                await _repository.DeleteEntity(alert);
                return true;
            }
            else
            {
               return false;
            }
        }

        public async Task<Alert> GetAlertByIdAsync(int alertId)
        {
            return await _context.Set<Alert>().FindAsync(alertId);
        }

        public async Task<IEnumerable<Alert>> GetAlertsByContractIdAsync(int contractId)
        {
            return await _context.Set<Alert>()
                                 .Where(a => a.ContractId == contractId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Alert>> GetAllAlertsAsync()
        {
            return await _context.Set<Alert>()
                                 .OrderByDescending(a => a.AlertDate)
                                 .ToListAsync();
        }

        public async Task<bool> DeleteAlertsByContractId(int contractId)
        {
            try
            {
                var alerts = await _context.Alerts
                                              .Where(a => a.ContractId == contractId)
                                              .ToListAsync();

                if (alerts.Any())
                {
                    _context.Alerts.RemoveRange(alerts);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting alerts for contract ID {contractId}: {ex.Message}");
            }
        }
    }
}
