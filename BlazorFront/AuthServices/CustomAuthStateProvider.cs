using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using BlazorFront.Services;
using BuisnesLogicLayer.DTO;
using System.Security.Claims;

namespace BlazorFront.AuthServices
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        public ILocalStorageService localStorageService { get; }
        public IUserServices userServices { get; }

        public CustomAuthStateProvider(ILocalStorageService localStorageService, IUserServices userServices)
        {
            this.localStorageService = localStorageService;
            this.userServices = userServices;
        }

        // Get Authentication State of a user by acces token in local storage
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accesToken = await localStorageService.GetItemAsync<string>("accessToken");
            ClaimsIdentity identity;

            if(accesToken != null)
            {
                UserProfileDTO user = await userServices.GetUserByAccessToken(accesToken);
                identity = GetClaimsIdentity(user);
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            var claimsPrincipal = new ClaimsPrincipal(identity);

            var authState = await Task.FromResult(new AuthenticationState(claimsPrincipal));
            return await Task.FromResult(authState);
        }

        // Mark a user as Authenticated for program
        public async Task MarkUserAsAuthenticated(UserTokenDTO token)
        {
            await localStorageService.SetItemAsync("accessToken", token.AccessToken);

            var userProfile = await userServices.GetUserByAccessToken(token.AccessToken);
            var identity = GetClaimsIdentity(userProfile);

            var claimsPrincipal = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        // Get ClaimsIdentity
        public ClaimsIdentity GetClaimsIdentity(UserProfileDTO userProfile)
        {
            var claimsIdentity = new ClaimsIdentity();

            if (userProfile != null)
            {
                var claims = new List<Claim>
                                {
                                    new Claim(ClaimTypes.Authentication, userProfile.Id),
                                    new Claim(ClaimTypes.Name, userProfile.Name),
                                    new Claim(ClaimTypes.Surname, userProfile.Surname),
                                    new Claim(ClaimTypes.Email, userProfile.Email),
                                    new Claim(ClaimTypes.MobilePhone, userProfile.PhoneNumber),
                                    new Claim("AdsAmount",userProfile.AdsAmount.ToString()),
                                    new Claim("ComentsAmount", userProfile.ComentsAmount.ToString())
                                 };

                claimsIdentity = new ClaimsIdentity(claims, "my_api_aputh_type");
            }
            return claimsIdentity;
        }

        // Mark a user as Logout
        public async Task MarkUserAsLoggedOut()
        {
            await localStorageService.RemoveItemAsync("accessToken");

            var identity = new ClaimsIdentity();

            var user = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
