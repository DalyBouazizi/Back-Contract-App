using Projet_Stage.Models;
using Projet_Stage.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

public class AlertRefresherService : IHostedService, IDisposable
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<AlertRefresherService> _logger;
    private Timer _timer;
    private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(2); // Run every 1 second

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

        // Start the timer to run immediately and then repeatedly every second
        _timer = new Timer(DoWork, null, TimeSpan.Zero, _checkInterval);

        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        _logger.LogInformation("Alert Refresher Service is checking for new alerts.");

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

            foreach (var contract in contractsEndingThisMonth)
            {
                // Check if an alert already exists for this contract
                var existingAlert = await alertService.GetAlertsByContractIdAsync(contract.id);

                if (existingAlert == null || !existingAlert.Any())
                {
                    var alert = new AlertModel
                    {
                        ContractId = contract.id,
                        AlertDate = DateTime.Now
                    };

                    await alertService.CreateAlertAsync(alert);
                    _logger.LogInformation("Alert created for contract ID: {ContractId}", contract.id);
                }
                else
                {
                    _logger.LogInformation("Alert already exists for contract ID: {ContractId}", contract.id);
                }
            }

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
