using System;
using System.Collections.Generic;
using System.Text;
using BuisnesLogicLayer.DTO;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Enteties;
using System.Threading.Tasks;

namespace BuisnesLogicLayer.Interfaces
{
    public interface IUserServices
    {
        public Task<IEnumerable<UserProfileDTO>> GetAllUsersProfiles();
        public Task<UserProfileDTO> GetUserProfileById(string id);
        public Task<bool> RegisterUser(UserRegisterDTO userRegisterDTO); // Add New User
        public Task<bool> DeleteUserById(string id);
        public Task<bool> UpdateUser(UserEditDTO userEditDTO);
        public Task<UserProfileDTO> GetUserProfileByEmail(string email);
        public Task<UserProfileDTO> GetUserByAccessToken(UserTokenDTO token);
        public Task<UserTokenDTO> LogIn(UserLogInDTO user);
        public void LogOut();
    }
}
