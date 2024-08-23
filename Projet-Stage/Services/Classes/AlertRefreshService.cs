using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Projet_Stage.Models;
using Projet_Stage.Services.Interfaces;

public class AlertRefresherService : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<AlertRefresherService> _logger;
    private Timer _timer;

    public AlertRefresherService(
        IServiceScopeFactory scopeFactory,
        ILogger<AlertRefresherService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Alert Refresher Service is starting.");
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        _logger.LogInformation("Alert Refresher Service is working.");

        using (var scope = _scopeFactory.CreateScope())
        {
            var contractService = scope.ServiceProvider.GetRequiredService<IContractService>();
            var alertService = scope.ServiceProvider.GetRequiredService<IAlertService>();

            await AddAlerts(contractService, alertService);
        }
    }

    private async Task AddAlerts(IContractService contractService, IAlertService alertService)
    {
        try
        {
            var contractsEndingThisMonth = await contractService.GetContractsEndingInOneMonthAsync();
            var alertTasks = contractsEndingThisMonth.Select(contract =>
            {
                var alert = new AlertModel
                {
                    ContractId = contract.id,
                    AlertDate = DateTime.Now
                    // Add other necessary fields
                };
                return alertService.CreateAlertAsync(alert);
            });

            await Task.WhenAll(alertTasks);
            _logger.LogInformation("Alerts have been refreshed.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding alerts.");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Alert Refresher Service is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
