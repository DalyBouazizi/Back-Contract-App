using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Projet_Stage.Services.Interfaces;

public class EmailSenderService : BackgroundService
{
    private readonly ILogger<EmailSenderService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public EmailSenderService(ILogger<EmailSenderService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTimeOffset.Now;
            var nextRunTime = GetNextMondayAtSpecificTime(now);
            var delay = nextRunTime - now;

            _logger.LogInformation("EmailSenderService scheduled to run at: {nextRunTime}", nextRunTime);

            if (delay > TimeSpan.Zero)
            {
                await Task.Delay(delay, stoppingToken);  // Wait until the next Monday at 
            }

            using (var scope = _serviceProvider.CreateScope())
            {
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                await emailService.SendContractsEmailAndAddAlertAsync();
            }
            //await Task.Delay(20000, stoppingToken);
        }
    }

    private DateTimeOffset GetNextMondayAtSpecificTime(DateTimeOffset currentTime)
    {
        // Calculate the days until the next Monday
        int daysUntilMonday = ((int)DayOfWeek.Wednesday - (int)currentTime.DayOfWeek + 7) % 7;

        if (daysUntilMonday == 0 && currentTime.TimeOfDay >= new TimeSpan(14, 25, 0))
        {
            // If it's Monday and past 8:26 AM, schedule for next Monday
            daysUntilMonday = 7;
        }

        var nextMonday = currentTime.Date.AddDays(daysUntilMonday);
        var nextMondayAt826AM = new DateTimeOffset(nextMonday.AddHours(14).AddMinutes(25), currentTime.Offset);

        return nextMondayAt826AM;
    }
}
