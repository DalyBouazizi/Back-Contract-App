using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Data.Abstract
{
    public interface IRepository<T> where T : class

    {

        Task<T> GetEntity(object id);
        Task<bool> AddEntity(T entity);
        Task<bool> AddListEntity(List<T> list_entity);
        Task<bool> UpdateEntity(T entity);
        Task<bool> UpdateListEntity(List<T> list_entity);
        Task<bool> DeleteEntity(T entity);
        Task<ICollection<T>> GetAllEntity();



    }

}

