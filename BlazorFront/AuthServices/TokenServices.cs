using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorFront.Services;
using Microsoft.AspNetCore.Components.Authorization;
using BuisnesLogicLayer.DTO;
using Blazored.LocalStorage;

namespace BlazorFront.AuthServices
{
    public class TokenServices : ITokenServices
    {
        public AuthenticationStateProvider authenticationStateProvider;
        public IUserServices userServices;
        public ILocalStorageService localStorageService;
        public TokenServices(IUserServices userServices, ILocalStorageService localStorageService)
        {
            this.userServices = userServices;
            this.localStorageService = localStorageService;
            authenticationStateProvider = new CustomAuthStateProvider(localStorageService, userServices);
        }
    }
}
