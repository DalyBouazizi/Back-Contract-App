using Projet_Data.Abstract;
using Projet_Data.Repo.Interfaces;
using Projet_Data.ModelsEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Data.Repo.Classes
{
    public class ContractRepository : IContractRepository
    {

        private readonly DataContext _context;
        private readonly IRepository<Contract> _repository;

        //constructor
        public ContractRepository(DataContext context , IRepository<Contract> repository) 
        {
            this._context = context;
            this._repository = repository;

        }

        //methods implementation from IContractRepository


    }
}
