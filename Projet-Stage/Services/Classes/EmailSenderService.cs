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
            _logger.LogInformation("EmailSenderService running at: {time}", DateTimeOffset.Now);

            using (var scope = _serviceProvider.CreateScope())
            {
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                await emailService.SendContractsEmailAndAddAlertAsync();
            }

            await Task.Delay(1000000, stoppingToken); // 10 seconds delay
        }
    }
}
