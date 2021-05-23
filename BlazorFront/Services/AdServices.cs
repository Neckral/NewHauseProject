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
    public class AdServices : IAdServices
    {
        private HttpClient httpClient { get; }

        public AdServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task AddNewAd(AdCreateDTO createAdDTO)
        {
            string serializedUser = JsonConvert.SerializeObject(createAdDTO);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            //var returnedUser = JsonConvert.DeserializeObject<bool>(responseBody);
        }

        public async Task DeleteAdById(int id)
        {
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"{id}"));
        }

        public async Task<AdInfoDTO> GetAdById(int id)
        {
            return await httpClient.GetJsonAsync<AdInfoDTO>("GetById/" + id);
        }

        public async Task<IEnumerable<AdInfoDTO>> GetAdsByUserId(string userId)
        {
            return await httpClient.GetJsonAsync<IEnumerable<AdInfoDTO>>("UserId/" + userId);
        }

        public async Task<IEnumerable<AdInfoDTO>> GetAllAds()
        {
            return await httpClient.GetJsonAsync<IEnumerable<AdInfoDTO>>("GetAll");
        }

        public async Task UpdateAd(AdEditDTO editAdDTO)
        {
            string serializedUser = JsonConvert.SerializeObject(editAdDTO);
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<UserRegisterDTO>(responseBody);
        }

        public async Task<IEnumerable<AdInfoDTO>> GetAdsByOptions(AdToCompare adToCompare)
        {
            string serializedUser = JsonConvert.SerializeObject(adToCompare);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "GetByOptions");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<IEnumerable<AdInfoDTO>>(responseBody);

            return returnedUser;
        }

        public async Task<AdEditDTO> GetAdForEdit(int id)
        {
            return await httpClient.GetJsonAsync<AdEditDTO>($"GetByIdToEdit/{id}");
        }
    }
}
