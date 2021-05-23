using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnesLogicLayer.DTO;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace BlazorFront.Services
{
    public interface IForCompareServices
    {
        public Task SetForCompare(string userId, int adId);

        public Task<IEnumerable<ForCompareDTO>> GetAllComparesByUserId(string userId);

        public Task RemoveCopareByUserIdAndAdId(string userId, int AdId);
    }
}
