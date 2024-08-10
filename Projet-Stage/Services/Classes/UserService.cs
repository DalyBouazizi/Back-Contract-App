using Projet_Data.ModelsEF;
using Projet_Data.Repo.Interfaces;
using Projet_Stage.Models;
using Projet_Stage.Services.Interfaces;

namespace Projet_Stage.Services.Classes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository )
        {
            this._userRepository = userRepository;
        }

        public async Task<(bool isSuccess, List<int> failedUserIds)> AddListUsersAsync(List<UserModel> users)
        {
            var failedUserIds = new List<int>();
            var userEntities = new List<User>();
            foreach (var user in users)
            {
                var existingUser = await _userRepository.GetUserByIdAsync(user.Matricule);
                if (existingUser == null)
                {
                    userEntities.Add(new User
                    {
                        Matricule = user.Matricule,
                        Nom = user.Nom,
                        Prenom = user.Prenom,
                        Password = user.Password
                    });
                }
                else
                {
                    failedUserIds.Add(user.Matricule);
                }
            }
            if (userEntities.Count > 0)
            {
                var result = await _userRepository.AddListUsersAsync(userEntities);
                return (result, failedUserIds);
            }
            return (false, failedUserIds);
        }

        public async Task<bool> AddUserAsync(UserModel user)
        {
            bool res = false;
            try
            {
                
                
                    User Newuser = new User();
                    Newuser.Matricule = user.Matricule;
                    Newuser.Nom = user.Nom;
                    Newuser.Prenom = user.Prenom;
                    Newuser.Password = user.Password;
                    res = await _userRepository.AddUserAsync(Newuser);
                
                
                if (res)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public Task<bool> DeleteUserAsync(int IdUser)
        {
           var res = _userRepository.DeleteUserAsync(IdUser);
            return res;
        }

        public async Task<List<UserModel>> GetAllUsersAsync()
        {
            List<UserModel> users = new List<UserModel>();
            
            try
            {
                var res = await _userRepository.GetAllUsersAsync();
                foreach (var item in res)
                {
                    UserModel user = new UserModel();
                    user.Matricule = item.Matricule;
                    user.Nom = item.Nom;
                    user.Prenom = item.Prenom;
                    user.Password = item.Password;
                    users.Add(user);
                }
                return await Task.FromResult(users);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<UserModel> GetUserByIdAsync(int IdUser)
        {
            try
            {
                
                var res = await _userRepository.GetUserByIdAsync(IdUser);
                if (res == null)
                {
                    
                    return null;
                }
                else
                {
                    UserModel user = new UserModel();
                    user.Matricule = res.Matricule;
                    user.Nom = res.Nom;
                    user.Prenom = res.Prenom;
                    user.Password = res.Password;

                    return await Task.FromResult(user);

                }
               
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(bool isSuccess, List<int> failedUserIds)> UpdateListUsersAsync(List<UserModel> users)
        {
            var failedUserIds = new List<int>();
            var Updatedusers = new List<User>();
            foreach(var user in users)
            {
                var test = await _userRepository.GetUserByIdAsync(user.Matricule);
                if(test != null)
                {
                    Updatedusers.Add(new User
                    {
                        Matricule = user.Matricule,
                        Nom = user.Nom,
                        Prenom = user.Prenom,
                        Password = user.Password
                    });
                }else
                {
                    failedUserIds.Add(user.Matricule);
                }
            }
            if (Updatedusers.Count > 0)
            {
                var result = await _userRepository.UpdateListUsersAsync(Updatedusers);
                return (result, failedUserIds);
            }
            return (false, failedUserIds);
        }

        public async Task<bool> UpdateUserAsync(UserModel User)
        {
            try
            {
                // Find the user by Matricule (or IdUser if using Id)
                var Updateduser = await _userRepository.GetUserByIdAsync(User.Matricule);
                if (Updateduser != null)
                {
                    // Update the properties of the user entity
                    Updateduser.Nom = User.Nom;
                    Updateduser.Prenom = User.Prenom;
                    //Updateduser.Password = User.Password;

                    // Use the UpdateEntity method from the abstract repository
                    return await _userRepository.UpdateUserAsync(Updateduser);
                }
                else
                {
                    return false; // User not found
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
