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
    public class ForCompareServices : IForCompareServices
    {
        private HttpClient httpClient { get; }

        public ForCompareServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<ForCompareDTO>> GetAllComparesByUserId(string userId)
        {
            return await httpClient.GetJsonAsync<IEnumerable<ForCompareDTO>>(userId);
        }

        public async Task RemoveCopareByUserIdAndAdId(string userId, int AdId)
        {
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"{userId}/{AdId}"));
        }

        public async Task SetForCompare(string userId, int adId)
        {
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, $"{userId}/{adId}"));
        }
    }
}
