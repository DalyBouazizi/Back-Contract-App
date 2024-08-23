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

            foreach (var contract in contractsEndingThisMonth)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var scopedAlertService = scope.ServiceProvider.GetRequiredService<IAlertService>();

                    var alert = new AlertModel
                    {
                        ContractId = contract.id,
                        AlertDate = DateTime.Now
                        // Add other necessary fields
                    };
                    await scopedAlertService.CreateAlertAsync(alert);
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
