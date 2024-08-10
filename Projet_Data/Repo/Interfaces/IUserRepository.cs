using Projet_Data.Abstract;
using Projet_Data.ModelsEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Data.Repo.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetAllUsersAsync();

        Task<bool> AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int IdUser);
        Task<bool> DeleteUserAsync(int IdUser);
        Task<bool> UpdateUserAsync(User User);
        Task<bool> AddListUsersAsync(List<User> users);
        Task<bool> UpdateListUsersAsync(List<User> users);

    }
}
