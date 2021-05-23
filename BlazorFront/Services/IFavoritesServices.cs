using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnesLogicLayer.DTO;
using DataAccesLayer.Enteties;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace BlazorFront.Services
{
    public interface IFavoritesServices
    {
        public Task SetFavorite(string userId, int adId);

        public Task<IEnumerable<AdShortInfoDTO>> GetAllFavoritesByUserId(string userId);

        public Task RemoveFavoriteByUserIdAndAdId(string userId, int adId);
    }
}
