using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BuisnesLogicLayer.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace BuisnesLogicLayer.JWTs
{
    public static class TokenManager
    {
        public static UserTokenDTO BuildToken(UserProfileDTO userProfile)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, userProfile.Id),
                new Claim(ClaimTypes.Name, userProfile.Name),
                new Claim(ClaimTypes.Surname, userProfile.Surname),
                new Claim(ClaimTypes.Email, userProfile.Email),
                new Claim(ClaimTypes.MobilePhone, userProfile.PhoneNumber),
                new Claim("AdsAmount",userProfile.AdsAmount.ToString()),
                new Claim("ComentsAmount", userProfile.ComentsAmount.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.KEY));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(JwtOptions.LIFETIME);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: JwtOptions.ISSUER,
                audience: JwtOptions.AUDIENCE,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);

            return new UserTokenDTO() { Token = new JwtSecurityTokenHandler().WriteToken(token) };
        }
    }
}
