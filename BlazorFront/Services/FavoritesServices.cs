using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using BuisnesLogicLayer.DTO;
using DataAccesLayer.Enteties;
using Microsoft.AspNetCore.Components;
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using BlazorFront.AuthServices;

namespace BlazorFront.Services
{
    public class FavoritesServices : IFavoritesServices // Authorized
    {
        private HttpClient httpClient { get; }
        public ILocalStorageService localStorageService { get; }
        public ITokenServices tokenServices;
        public FavoritesServices(HttpClient httpClient, ILocalStorageService localStorageService, ITokenServices tokenServices)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
            this.tokenServices = tokenServices;
        }

        public async Task<IEnumerable<AdShortInfoDTO>> GetAllFavoritesByUserId(string userId)
        {
            string serializedAd = JsonConvert.SerializeObject("");
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"{userId}");
            requestMessage.Content = new StringContent(serializedAd);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string accessToken = await localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage);
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<AdShortInfoDTO>>(responseBody);


        }

        public async Task RemoveFavoriteByUserIdAndAdId(string userId, int adId)
        {
            string serializedAd = JsonConvert.SerializeObject("");
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"{userId}/{adId}");
            requestMessage.Content = new StringContent(serializedAd);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string accessToken = await localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage);

            var responseBody = await response.Content.ReadAsStringAsync();
        }

        public async Task SetFavorite(string userId, int adId)
        {
            string serializedAd = JsonConvert.SerializeObject("");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{userId}/{adId}");
            requestMessage.Content = new StringContent(serializedAd);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string accessToken = await localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage);

            var errorMessage = new Regex("(?<=error_description=\").*(?=;)").Match(response.Headers.WwwAuthenticate.ToString());

            var responseBody = await response.Content.ReadAsStringAsync();
        }
    }
}
