using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnesLogicLayer.Interfaces;
using BuisnesLogicLayer.Services;
using BuisnesLogicLayer.DTO;
using DataAccesLayer.Enteties;
using Microsoft.AspNetCore.Authorization;
using DataAccesLayer.Helpers;
using DataAccesLayer.Models;
using Newtonsoft.Json;

namespace HauseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdController : Controller
    {
        private readonly IAdServices adServices;

        public AdController(IAdServices adServices) => this.adServices = adServices;

        // get all ads
        [HttpGet("GetAll")]
        public async Task<PagedList<AdShortInfoDTO>> GetAllAds([FromQuery] QueryStringParameters parameters)
        {
            var ads = await adServices.GetAllAds(parameters);
            var metadata = new
            {
                ads.TotalCount,
                ads.PageSize,
                ads.CurrentPage,
                ads.TotalPages,
                ads.HasNext,
                ads.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return ads;
        }

        // get by option ads
        [HttpPost("GetByOptions")]
        public async Task<PagedList<AdShortInfoDTO>> GetAdsByOptions([FromBody] AdToCompare adToCompare,[FromQuery]QueryStringParameters parameters)
        {
            var ads = await adServices.GetAdsByOptions(adToCompare, parameters);
            var metadata = new
            {
                ads.TotalCount,
                ads.PageSize,
                ads.CurrentPage,
                ads.TotalPages,
                ads.HasNext,
                ads.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return ads;
        }

        // get all ads by user id
        [Authorize]
        [HttpGet("UserId/{userId}")]
        public async Task<IEnumerable<AdShortInfoDTO>> GetAllAdsByUserId(string userId)
        {
            return await adServices.GetAdsByUserId(userId);
        }

        // get ad by id
        [HttpGet("GetById/{id}")]
        public async Task<AdInfoDTO> GetAddDTOByID(int id)
        {
            return await adServices.GetAdById(id);
        }

        // add new ad
        [Authorize]
        [HttpPost]
        public async Task AddNewAdd([FromBody] AdCreateDTO createAdDTO)
        {
            await adServices.AddNewAd(createAdDTO);
        }

        // delete ad by id
        [Authorize]
        [HttpDelete("{id}")]
        public async Task DeleteAdById(int id)
        {
            await adServices.DeleteAdById(id);
        }

        // edit ad by id
        [Authorize]
        [HttpPut]
        public async Task EditAd([FromBody] AdEditDTO editAdDTO)
        {
            await adServices.UpdateAd(editAdDTO);
        }

        // get add to edit dto
        [Authorize]
        [HttpGet("GetByIdToEdit/{id}")]
        public async Task<AdEditDTO> GetAdToEdit(int id)
        {
            return await adServices.GetAdToEdit(id);
        }
    }
}
