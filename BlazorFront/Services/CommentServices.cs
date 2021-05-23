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
    public class CommentServices : ICommentServices
    {
        private HttpClient httpClient { get; }

        public CommentServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task CreateComment(CommentCreateDTO commentCreate)
        {
            string serializedUser = JsonConvert.SerializeObject(commentCreate);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();
        }

        public async Task DeleteCommentById(int id)
        {
            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Delete, $"{id}"));
        }

        public async Task EditComment(CommentInfoAndEditIDTO commentInfoAndEdit)
        {
            string serializedUser = JsonConvert.SerializeObject(commentInfoAndEdit);
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returned = JsonConvert.DeserializeObject<UserRegisterDTO>(responseBody);
        }

        public async Task<IEnumerable<CommentInfoAndEditIDTO>> GetCommentsByAdId(int adId)
        {
            return await httpClient.GetJsonAsync<IEnumerable<CommentInfoAndEditIDTO>>($"ad/{adId}");
        }
    }
}
