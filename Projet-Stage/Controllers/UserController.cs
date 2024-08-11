using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projet_Data.Repo.Interfaces;
using Projet_Stage.Models;
using Projet_Stage.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Projet_Stage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [Route("AddUser")]
        [HttpPost]
        public async Task<ActionResult<String>> AddUserAsync([Required] UserModel user)
        {
            bool res = false;

            res = await _userService.AddUserAsync(user);
            if (res)
            {
                return Ok("User added");
            }
            else
            {
                return BadRequest("Matricule exists already");
            }
        }
        [Route("GetAllUsers")]
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAllUsersAsync()
        {
            List<UserModel> users = new List<UserModel>();
            try
            {
                users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetUserById")]
        [HttpGet]
        public async Task<ActionResult<UserModel>> GetUserByIdAsync([Required] int IdUser)
        {
           
            UserModel user = new UserModel();
            try
            {
                user = await _userService.GetUserByIdAsync(IdUser);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
            if (user == null)
            {
                
                return BadRequest("User with id " + IdUser + " Not found");
            }
            else
            {
                return Ok(user);

            }
        }
        
        [Route("DeleteUser")]
        [HttpDelete]
        public async Task<ActionResult<String>> DeleteUserAsync([Required] int IdUser)
        {
            bool res = false;
            res = await _userService.DeleteUserAsync(IdUser);
            if (res)
            {
                return Ok("User deleted successfuly");
            }
            else
            {
                return BadRequest("User not with id "+IdUser+" not found");
            }
        }

        [Route("UpdateUser")]
        [HttpPut]
        public async Task<ActionResult<string>> UpdateUserAsync(int IdUser, [FromForm] string Nom, [FromForm] string Prenom)
        {
            UserModel updatedUser = new UserModel
            {
                Matricule = IdUser,  // Use the non-editable IdUser as the Matricule
                Nom = Nom,
                Prenom = Prenom,
                //Password = Password
            };
            bool res = await _userService.UpdateUserAsync(updatedUser);
            if (res)
            {
                return Ok("User updated successfully");
            }
            else
            {
                return BadRequest("User not found or update failed");
            }
        }
        [Route("AddListUsers")]
        [HttpPost]
        public async Task<ActionResult<string>> AddListUsersAsync([FromBody] List<UserModel> users)
        {
            if (users == null )
            {
                return BadRequest("No users were provided");
            }
            var (isSuccess, failedUserIds) = await _userService.AddListUsersAsync(users);

            if (isSuccess)
            {
                if (failedUserIds.Count > 0)
                {
                    return Ok($"Users added successfully, but users with the following IDs were not added because they already exist: {string.Join(", ", failedUserIds)}");
                }
                return Ok("All users added successfully.");
            }
            else
            {
                return BadRequest("Failed to add users.");
            }
        }
        [Route("UpdateListUser/")]
        [HttpPut]
        public async Task<ActionResult<string>> UpdateListUserAsync([FromBody] List<UserModel> updatedUsers)
        {
            if (updatedUsers == null || updatedUsers.Count == 0)
            {
                return BadRequest("No users were provided");
            }
            var (isSuccess, failedUserIds) = await _userService.UpdateListUsersAsync(updatedUsers);
            if (isSuccess)
            {
                if (failedUserIds.Count > 0)
                {
                    return Ok($"Users updated successfully, but users with the following IDs were not updated because they dont exist: {string.Join(", ", failedUserIds)}");
                }
                return Ok("All users updated successfully.");
            }
            else{
                return BadRequest($"Failed to update users, users with the following IDs were not updated because they dont exist: {string.Join(", ", failedUserIds)}");
            }
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserModel>> Login([Required] int Matricule, [Required] string Password)
        {
            String Result = await _userService.LoginUser(Matricule, Password);
            if (Result == null)
            {
                return BadRequest("Invalid matricule or password");
            }else if (Result == "Password incorrect")
            {
                return BadRequest("Password incorrect");
            }
            else if (Result == "User with id "+Matricule+" not found")
            {
                return BadRequest("User not found");
            }
            else if (Result == "Success")
            {
                return Ok("You're good!!");
            }
            else
            {
                return BadRequest();
            }
           
        }
    }
}
