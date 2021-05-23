using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using BuisnesLogicLayer.DTO;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using System.Text.RegularExpressions;

namespace BlazorFront.Services
{
    public class UserServices : IUserServices
    {
        private HttpClient httpClient { get; }

        public ILocalStorageService localStorageService { get; }
        //public ITokenServices tokenServices;
        //public AuthenticationStateProvider authenticationStateProvider;
        public UserServices(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            // this.authenticationStateProvider = new CustomAuthStateProvider(localStorageService, this);
             this.localStorageService = localStorageService;
        }
        /*====================== Get By Email ======================*/
        public async Task<UserProfileDTO> GetUserByEmail(string email)
        {
            return await httpClient.GetJsonAsync<UserProfileDTO>("Email/" + email);
        }

        /*====================== Register ======================*/
        public async Task<bool> RegisterUser(UserRegisterDTO user)
        {
            string serializedUser = JsonConvert.SerializeObject(user);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "RegisterUser");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<bool>(responseBody);

            return returnedUser;
        }

        /*====================== Edit ======================*/
        public async Task<bool> UpdateUser(UserEditDTO user)
        {
            string serializedUser = JsonConvert.SerializeObject(user);
            var requestMessage = new HttpRequestMessage(HttpMethod.Put, "UpdateUser");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<UserRegisterDTO>(responseBody);

            return returnedUser.Email != null ? true : false;
        }

        public async Task<UserTokenDTO> LogIn(UserLogInDTO user)
        {
            string serializedUser = JsonConvert.SerializeObject(user);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Login");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var response = await httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<UserTokenDTO>(responseBody);

            return token != null ? token : null;

        }

        public async Task<UserProfileDTO> GetUserById(string Id)
        {
            string serializedUser = JsonConvert.SerializeObject("");
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "GetById/" + Id);
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string accessToken = await localStorageService.GetItemAsync<string>("accessToken");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage);

            UserProfileDTO savedResult = new UserProfileDTO();
            var errorMessage = new Regex("(?<=error_description=\").*(?=;)").Match(response.Headers.WwwAuthenticate.ToString());
            if (errorMessage.Value == "The token lifetime is invalid" && response.StatusCode.ToString() == "Unauthorized")
            {
                string refreshToken = await localStorageService.GetItemAsync<string>("refreshToken");
                //await tokenServices.RefreshToken(new UserTokenDTO() { AccessToken = accessToken, RefreshToken = refreshToken });
                savedResult = await GetUserByAccessToken(accessToken); // call method again after refresh token
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserProfileDTO>(responseBody);
            }
            return savedResult;
        }

        public async Task<UserProfileDTO> GetUserByAccessToken(string accessToken)
        {
            string serializedUser = JsonConvert.SerializeObject(accessToken);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "GetByToken");
            requestMessage.Content = new StringContent(serializedUser);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<UserProfileDTO>(responseBody);

            return returnedUser != null ? returnedUser : null;
        }

        public async Task<UserTokenDTO> RefreshToken(UserTokenDTO token)
        {
            string serializedToken = JsonConvert.SerializeObject(token);
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "RefreshToken");
            requestMessage.Content = new StringContent(serializedToken);
            requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await httpClient.SendAsync(requestMessage);
            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedToken = JsonConvert.DeserializeObject<UserTokenDTO>(responseBody);

            return returnedToken != null ? returnedToken : null;
        }
    }
}