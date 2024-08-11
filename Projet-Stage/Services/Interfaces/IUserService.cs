using Projet_Data.ModelsEF;
using Projet_Stage.Models;

namespace Projet_Stage.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserModel>> GetAllUsersAsync();
        Task<bool> AddUserAsync(UserModel user);
        Task<UserModel> GetUserByIdAsync(int IdUser);
        Task<bool> DeleteUserAsync(int IdUser);
        Task<bool> UpdateUserAsync(UserModel User);
        Task<(bool isSuccess, List<int> failedUserIds)> AddListUsersAsync(List<UserModel> users);
        Task<(bool isSuccess, List<int> failedUserIds)> UpdateListUsersAsync(List<UserModel> users);
        Task<String> LoginUser(int Matricule, String Password);

    }
}
