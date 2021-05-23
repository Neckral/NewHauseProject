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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        [HttpGet("GetById/{id}")]
        public async Task<UserProfileDTO> GetUserProfileDTOById(string id)
        {
            return await userServices.GetUserProfileById(id);
        }

        // Get User Profile by id
        [Authorize]
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
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<bool> DeleteUserById(string id)
        {
           return await userServices.DeleteUserById(id);
        }

        // Edit user
        [Authorize]
        [HttpPut("UpdateUser")]
        public async Task<bool> EditUser([FromBody] UserEditDTO userEditDTO)
        {
            return await userServices.UpdateUser(userEditDTO);
        }

        [HttpPost("LogIn")]
        public async Task<UserTokenDTO> LogInUser([FromBody] UserLogInDTO login)
        {
            return await userServices.LogIn(new UserLogInDTO() { Email = login.Email, Password = login.Password });
        }
        
        //[Authorize]
        [HttpPost("GetByToken")]
        public async Task<UserProfileDTO> GetUserByAccessToken([FromBody] string token)
        {
            return await userServices.GetUserByAccessToken(new UserTokenDTO() { AccessToken = token });
        }
    }
}
