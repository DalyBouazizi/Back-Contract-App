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

        ScheduleNextRun();

        return Task.CompletedTask;
    }

    private void ScheduleNextRun()
    {
        var now = DateTimeOffset.Now;
        var nextRunTime = GetNextMondayAtSpecificTime(now);
        var delay = nextRunTime - now;

        _timer = new Timer(DoWork, null, delay, TimeSpan.FromMilliseconds(-1)); // Only run once
        _logger.LogInformation("Alert Refresher Service scheduled to run at: {nextRunTime}", nextRunTime);
    }

    private DateTimeOffset GetNextMondayAtSpecificTime(DateTimeOffset currentTime)
    {
        int daysUntilMonday = ((int)DayOfWeek.Monday - (int)currentTime.DayOfWeek + 7) % 7;

        if (daysUntilMonday == 0 && currentTime.TimeOfDay >= new TimeSpan(9, 20, 0))
        {
            daysUntilMonday = 7;
        }

        var nextMonday = currentTime.Date.AddDays(daysUntilMonday);
        var nextMondayAt826AM = new DateTimeOffset(nextMonday.AddHours(9).AddMinutes(20), currentTime.Offset);

        return nextMondayAt826AM;
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

        ScheduleNextRun(); // Reschedule the next run
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























//using Projet_Stage.Models;
//using Projet_Stage.Services.Interfaces;

//public class AlertRefresherService : IHostedService, IDisposable
//{
//    private readonly IServiceScopeFactory _scopeFactory;
//    private readonly ILogger<AlertRefresherService> _logger;
//    private Timer _timer;

//    public AlertRefresherService(
//        IServiceScopeFactory scopeFactory,
//        ILogger<AlertRefresherService> logger)
//    {
//        _scopeFactory = scopeFactory;
//        _logger = logger;
//    }

//    public Task StartAsync(CancellationToken cancellationToken)
//    {
//        _logger.LogInformation("Alert Refresher Service is starting.");
//        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(1));
//        return Task.CompletedTask;
//    }

//    private async void DoWork(object state)
//    {
//        _logger.LogInformation("Alert Refresher Service is working.");

//        using (var scope = _scopeFactory.CreateScope())
//        {
//            var contractService = scope.ServiceProvider.GetRequiredService<IContractService>();
//            var alertService = scope.ServiceProvider.GetRequiredService<IAlertService>();

//            await AddAlerts(contractService, alertService);
//        }
//    }

//    private async Task AddAlerts(IContractService contractService, IAlertService alertService)
//    {
//        try
//        {
//            var contractsEndingThisMonth = await contractService.GetContractsEndingInOneMonthAsync();

//            foreach (var contract in contractsEndingThisMonth)
//            {
//                using (var scope = _scopeFactory.CreateScope())
//                {
//                    var scopedAlertService = scope.ServiceProvider.GetRequiredService<IAlertService>();

//                    var alert = new AlertModel
//                    {
//                        ContractId = contract.id,
//                        AlertDate = DateTime.Now
//                        // Add other necessary fields
//                    };
//                    await scopedAlertService.CreateAlertAsync(alert);
//                }
//            }

//            _logger.LogInformation("Alerts have been refreshed.");
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Error adding alerts.");
//        }
//    }

//    public Task StopAsync(CancellationToken cancellationToken)
//    {
//        _logger.LogInformation("Alert Refresher Service is stopping.");
//        _timer?.Change(Timeout.Infinite, 0);
//        return Task.CompletedTask;
//    }

//    public void Dispose()
//    {
//        _timer?.Dispose();
//    }
//}
