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
using BlazorFront.AuthServices;
using System.Text.RegularExpressions;
using DataAccesLayer.Models;
using BlazorFront.Models;

namespace BlazorFront.Services
{
    public class AdServices : IAdServices
    {
        private HttpClient httpClient { get; }
        public ILocalStorageService localStorageService { get; }
        public ITokenServices tokenServices;

        public AdServices(HttpClient httpClient, ILocalStorageService localStorageService,ITokenServices tokenServices)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
            this.tokenServices = tokenServices;
        }

        public async Task AddNewAd(AdCreateDTO createAdDTO) // Authorized
        {
            string serializedUser = JsonConvert.SerializeObject(createAdDTO);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string accessToken = await localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage);

            var responseBody = await response.Content.ReadAsStringAsync();
        }

        public async Task DeleteAdById(int id) // Authorized
        {
            string serializedUser = JsonConvert.SerializeObject("");
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, $"{id}");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string accessToken = await localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage);
            var responseBody = await response.Content.ReadAsStringAsync();

            var errorMessage = new Regex("(?<=error_description=\").*(?=;)").Match(response.Headers.WwwAuthenticate.ToString());

        }

        public async Task<AdInfoDTO> GetAdById(int id)
        {
            return await httpClient.GetJsonAsync<AdInfoDTO>("GetById/" + id);
        }

        public async Task<IEnumerable<AdShortInfoDTO>> GetAdsByUserId(string userId) // Authorized
        {
            string serializedAd = JsonConvert.SerializeObject("");
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "UserId/" + userId);
            requestMessage.Content = new StringContent(serializedAd);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string accessToken = await localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage);

                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<AdShortInfoDTO>>(responseBody);

        }

        public async Task<PagedList<AdShortInfoDTO>> GetAllAds(QueryStringParameters parameters)
        {
            var requestMessage = await httpClient.GetAsync($"GetAll?PageNumber={parameters.PageNumber}&PageSize={parameters.PageSize}");
            using var responseContent = await requestMessage.Content.ReadAsStreamAsync();

            var resp = requestMessage.Headers.TryGetValues("X-Pagination", out var pag);
            var pagg = JsonConvert.DeserializeObject<PagedList>(pag.First());

            List<AdShortInfoDTO> ads = await System.Text.Json.JsonSerializer.DeserializeAsync<List<AdShortInfoDTO>>(responseContent);
            var result = new PagedList<AdShortInfoDTO>(ads, pagg.TotalCount, pagg.CurrentPage, pagg.PageSize, pagg.HasNext, pagg.HasPrevious);
            return result;
        }

        public async Task UpdateAd(AdEditDTO editAdDTO) // Authorized
        {
            string serializedUser = JsonConvert.SerializeObject(editAdDTO);
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string accessToken = await localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage);
            var responseBody = await response.Content.ReadAsStringAsync();

        }

        public async Task<IEnumerable<AdShortInfoDTO>> GetAdsByOptions(AdToCompare adToCompare)
        {
            string serializedUser = JsonConvert.SerializeObject(adToCompare);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "GetByOptions");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;

                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<AdShortInfoDTO>>(responseBody);

        }

        public async Task<AdEditDTO> GetAdForEdit(int id)
        {
            string serializedUser = JsonConvert.SerializeObject("");
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"GetByIdToEdit/{id}");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string accessToken = await localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;

                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AdEditDTO>(responseBody);
        }
    }
}
