using Microsoft.EntityFrameworkCore;
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
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IRepository<User> _repository;
        public UserRepository(DataContext context, IRepository<User> repository)
        {
            _context = context;
            _repository = repository;

        }
        //methods implementation from IUserRepository

        public async Task<ICollection<User>> GetAllUsersAsync()
        {
            try
            {
                var ListUser = await _repository.GetAllEntity();
                return ListUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        public async Task<bool> AddUserAsync(User user)
        {

            //await _repository.AddAsync(user);
            //return true;
            var test = await GetUserByIdAsync(user.Matricule);
            if (test == null) 
            { 
                try
                {
                    await _repository.AddEntity(user);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
               return false;
            }

        }
        public async Task<User> GetUserByIdAsync(int IdUser)
        {
            User user;
            user = await _context.Users
                .Where(u => u.Matricule.Equals(IdUser)).FirstOrDefaultAsync();
            return user;
        }
        public async Task<bool> DeleteUserAsync(int IdUser)
        {
            try
            {
                var user = await GetUserByIdAsync(IdUser);
                if (user != null)
                {
                    await _repository.DeleteEntity(user);
                    return true;
                }else
                {
                    return false;
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> UpdateUserAsync(User User)
        {
            try
            {
                await _repository.UpdateEntity(User);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> AddListUsersAsync(List<User> users)
        {
            try
            {
                return await _repository.AddListEntity(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateListUsersAsync(List<User> users)
        {
            try
            {
                return await _repository.UpdateListEntity(users);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

