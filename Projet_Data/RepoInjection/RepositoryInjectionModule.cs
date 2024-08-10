using Microsoft.Extensions.DependencyInjection;
using Projet_Data.Abstract;
using Projet_Data.Repo.Classes;
using Projet_Data.Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Data.RepoInjection
{
    public static class RepositoryInjectionModule
    {

        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IContractRepository, ContractRepository>();
            services.AddTransient<IEmployeeRepository, EmployeRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
