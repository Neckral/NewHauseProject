using System;
using System.Collections.Generic;
using System.Text;
using BuisnesLogicLayer.DTO;
using BuisnesLogicLayer.Interfaces;
using DataAccesLayer;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Enteties;
using BuisnesLogicLayer.Converters;
using DataAccesLayer.Repositories;
using System.Linq;
using DataAccesLayer.EF;
using System.Threading.Tasks;
using AutoMapper;
using BuisnesLogicLayer.JWTs;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace BuisnesLogicLayer.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper mapper;
        public UserServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Database = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<bool> DeleteUserById(string id)
        {
           return await Database.UserRepository.DeleteById(id);
        }

        public async Task<IEnumerable<UserProfileDTO>> GetAllUsersProfiles()
        {
            var users = await Database.UserRepository.GetAll();
            List<UserProfileDTO> uesrsProfiles = new();
            foreach (var user in users)
            {
                var userDTO = await GetUserDTO(user);
                uesrsProfiles.Add(userDTO);
            }
            return uesrsProfiles.ToList();
        }

        public async Task<UserProfileDTO> GetUserProfileById(string id)
        {
            var user = await Database.UserRepository.GetById(id);
            return await GetUserDTO(user);
        }

        public async Task<UserProfileDTO> GetUserProfileByEmail(string email)
        {
            var user = await Database.UserRepository.GetByEmail(email);
            return await GetUserDTO(user);
        }

        public async Task<UserTokenDTO> LogIn (UserLogInDTO userLogin)
        {
            var user = await Database.UserRepository.LogIn(userLogin.Email, userLogin.Password);
            if(user != null)
            {
                var mappedUser = mapper.Map<User, UserProfileDTO>(user);
                return TokenManager.BuildToken(mappedUser);
            }
            return null;
        }

        public void LogOut()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RegisterUser(UserRegisterDTO userRegisterDTO)
        {
           return await Database.UserRepository.Add(ConvertToUser.FromRgisterDTO(userRegisterDTO));
        }

        public async Task<bool> UpdateUser(UserEditDTO userEditDTO)
        {
            return await Database.UserRepository.Update(ConvertToUser.FromUserEditDTO(userEditDTO));
        }

        private async Task<UserProfileDTO> GetUserDTO(User user)
        {
            var comments = await Database.CommentRepository.GetAll();
            var commentsCount = comments.Where(comment => comment.UserID == user.Id).Count();
            var ads = await Database.AdRepository.GetAll();
            var adsCount = ads.Where(ad => ad.OwnerId == user.Id).Count();

            var mappedUser = mapper.Map<User, UserProfileDTO>(user);
            mappedUser.AdsAmount = adsCount;
            mappedUser.ComentsAmount = commentsCount;
            return mappedUser;
        }

        public async Task<UserProfileDTO> GetUserByAccessToken(UserTokenDTO token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(JwtOptions.KEY);

                var tokenVakidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                var principle = tokenHandler.ValidateToken(token.Token, tokenVakidationParameters, out SecurityToken securityToken);

                if(securityToken is JwtSecurityToken jwtSecurityToken && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,StringComparison.InvariantCultureIgnoreCase))
                {
                    var email = principle.FindFirst(ClaimTypes.Email)?.Value;
                    var user = await Database.UserRepository.GetByEmail(email);
                    return await GetUserDTO(user);
                }
            }
            catch(Exception)
            {
                return new UserProfileDTO();
            }

            return new UserProfileDTO();
        }
    }
}
