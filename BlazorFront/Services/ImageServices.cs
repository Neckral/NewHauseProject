using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using BuisnesLogicLayer.DTO;
using Microsoft.AspNetCore.Components;
namespace BlazorFront.Services
{
    public class ImageServices : IImageServices
    {
        private HttpClient httpClient { get; }

        public ImageServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }


        public async Task<IEnumerable<ImageEditInfoDTO>> GetImagesByAdId(int adId)
        {
            return await httpClient.GetJsonAsync<IEnumerable<ImageEditInfoDTO>>("GetByAdId/" + adId);
        }
    }
}
