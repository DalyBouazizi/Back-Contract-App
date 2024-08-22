using Projet_Stage.Services.Classes;
using Projet_Stage.Services.Interfaces;

namespace Projet_Stage.ServiceInjection
{
    public static class ServiceInjectionModel
    {

        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IContractService, ContractService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IAlertService, AlertService>();
            return services;
        }

    }
}
