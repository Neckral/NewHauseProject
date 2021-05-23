using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnesLogicLayer.Interfaces;
using BuisnesLogicLayer.Services;
using BuisnesLogicLayer.DTO;

namespace HauseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ForCompareController : Controller
    {
        private readonly IForCompareServices forCompareServices;

        public ForCompareController(IForCompareServices forCompareServices) => this.forCompareServices = forCompareServices;
        
        // set(add) new comparision for user
        [HttpPost("{userId}/{adId}")]
        public async Task SetNewForCompare(string userId, int adId)
        {
            await forCompareServices.SetForCompare(userId, adId);
        }

        // get all user compares by user and ad id
        [HttpGet("{userId}")]
        public async Task<IEnumerable<ForCompareDTO>> GetUserFavoritesAds(string userId)
        {
            return await forCompareServices.GetAllComparesByUserId(userId);
        }

        // Delete Compare by useId and adId
        [HttpDelete("{userId}/{adId}")]
        public async Task DeleteCompare(string userId, int adId)
        {
            await forCompareServices.RemoveCopareByUserIdAndAdId(userId, adId);
        }
    }
}
