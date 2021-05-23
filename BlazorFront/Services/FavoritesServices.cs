using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using BuisnesLogicLayer.DTO;
using DataAccesLayer.Enteties;
using Microsoft.AspNetCore.Components;

namespace BlazorFront.Services
{
    public class FavoritesServices : IFavoritesServices
    {
        private HttpClient httpClient { get; }

        public FavoritesServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<AdShortInfoDTO>> GetAllFavoritesByUserId(string userId)
        {
            return await httpClient.GetJsonAsync<IEnumerable<AdShortInfoDTO>>(userId);
        }

        public async Task RemoveFavoriteByUserIdAndAdId(string userId, int adId)
        {
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"{userId}/{adId}"));
        }

        public async Task SetFavorite(string userId, int adId)
        {
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"{userId}/{adId}"));
        }
    }
}
