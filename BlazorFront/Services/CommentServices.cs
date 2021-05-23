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

namespace BlazorFront.Services
{
    public class CommentServices : ICommentServices
    {
        private HttpClient httpClient { get; }
        public ILocalStorageService localStorageService { get; }
        public ITokenServices tokenServices;
        public CommentServices(HttpClient httpClient, ILocalStorageService localStorageService, ITokenServices tokenServices)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
            this.tokenServices = tokenServices;
        }

        public async Task CreateComment(CommentCreateDTO commentCreate) // Authorized
        {
            string serializedUser = JsonConvert.SerializeObject(commentCreate);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string accessToken = await localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage);
            var responseBody = await response.Content.ReadAsStringAsync();

            var errorMessage = new Regex("(?<=error_description=\").*(?=;)").Match(response.Headers.WwwAuthenticate.ToString());

        }

        public async Task DeleteCommentById(int id) // Authorized
        {
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"{id}"));

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

        public async Task EditComment(CommentInfoAndEditIDTO commentInfoAndEdit) // Authorized
        {
            string serializedUser = JsonConvert.SerializeObject(commentInfoAndEdit);
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string accessToken = await localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage);
            var responseBody = await response.Content.ReadAsStringAsync();

            var errorMessage = new Regex("(?<=error_description=\").*(?=;)").Match(response.Headers.WwwAuthenticate.ToString());

        }

        public async Task<IEnumerable<CommentInfoAndEditIDTO>> GetCommentsByAdId(int adId)
        {
            string serializedAd = JsonConvert.SerializeObject("");
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, $"ad/{adId}");
            requestMessage.Content = new StringContent(serializedAd);
            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            //string accessToken = await localStorageService.GetItemAsync<string>("accessToken");
            //requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedComment = JsonConvert.DeserializeObject<IEnumerable<CommentInfoAndEditIDTO>>(responseBody);

            return returnedComment;
        }
    }
}
