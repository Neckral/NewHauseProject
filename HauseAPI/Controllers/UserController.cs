using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnesLogicLayer.Services;
using BuisnesLogicLayer.Interfaces;
using DataAccesLayer.Enteties;
using BuisnesLogicLayer.DTO;
using DataAccesLayer.EF;

namespace HauseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IUserServices userServices;
        
        public UserController(IUserServices userServices) => this.userServices = userServices;

        // Get All Users Profiles
        [HttpGet]
        public async Task<IEnumerable<UserProfileDTO>> GetAllUsersProfiles()
        {
            return await userServices.GetAllUsersProfiles();
        }

        // Get User Profile by id
        [HttpGet("GetById/{id}")]
        public async Task<UserProfileDTO> GetUserProfileDTOById(string id)
        {
            return await userServices.GetUserProfileById(id);
        }

        // Get User Profile by id
        [HttpGet("Email/{email}")]
        public async Task<UserProfileDTO> GetUserProfileDTOByEmail(string email)
        {
            return await userServices.GetUserProfileByEmail(email);
        }

        // Register new user
        [HttpPost("RegisterUser")]
        public async Task<bool> RegisterUser([FromBody]UserRegisterDTO userRegisterDTO)
        {
           return await userServices.RegisterUser(userRegisterDTO);
        }

        // Delete user by id
        [HttpDelete("{id}")]
        public async Task<bool> DeleteUserById(string id)
        {
           return await userServices.DeleteUserById(id);
        }

        // Edit user
        [HttpPut("UpdateUser")]
        public async Task<bool> EditUser([FromBody] UserEditDTO userEditDTO)
        {
            return await userServices.UpdateUser(userEditDTO);
        }

        //[HttpGet("LogIn/{email}/{password}")]
        //public async Task<UserProfileDTO> LogInUser (string email, string password)
        //{
        //    return await userServices.LogIn(new UserLogInDTO() { Email = email,Password = password});
        //}
        [HttpPost("LogIn")]
        public async Task<UserTokenDTO> LogInUser([FromBody] UserLogInDTO login)
        {
            return await userServices.LogIn(new UserLogInDTO() { Email = login.Email, Password = login.Password });
        }

        [HttpPost("BetByToken")]
        public async Task<UserProfileDTO> GetUserByAccessToken([FromBody] UserTokenDTO token)
        {
            return await userServices.GetUserByAccessToken(token);
        }
    }
}
