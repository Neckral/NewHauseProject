using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Enteties;
using BuisnesLogicLayer.DTO;

namespace BuisnesLogicLayer.Converters
{
    public static class ConvertToUser
    {
        public static User FromRgisterDTO(UserRegisterDTO DTO)
        {
            return new User()
            {
                Name = DTO.Name,
                Surname = DTO.Surname,
                PhoneNumber = DTO.PhoneNumber,
                Email = DTO.Email,
                PasswordHash = DTO.PasswordHash,
                PhoneNumberConfirmed = false,
                EmailConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                LockoutEnd = DateTime.Now,
                AccessFailedCount = 0,
                UserName = String.Concat(DTO.Name, DTO.Surname)
            };
        }

        public static User FromUserEditDTO(UserEditDTO DTO)
        {
            return new User()
            {
                Id = DTO.Id,
                Name = DTO.Name,
                Surname = DTO.Surname,
                PhoneNumber = DTO.PhoneNumber,
                Email = DTO.Email,
                PasswordHash = DTO.PasswordHash
            };
        }
    }
}