using Projet_Data.Abstract;
using Projet_Data.Repo.Interfaces;
using Projet_Data.ModelsEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projet_Data.ModelsEF2;

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

        public async Task<bool> AddContractAsync(Contract Contract)
        {
            try
            {
                return await _repository.AddEntity(Contract);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<Contract>> GetAllContractsAsync()
        {
            try
            {
                var ListContracts =  await _repository.GetAllEntity();
                return ListContracts;

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }   
        }

       

        //methods implementation from IContractRepository


    }
}
