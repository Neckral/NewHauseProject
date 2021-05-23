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
    public class FavoritesController : Controller
    {
        private readonly IFavoritesServices favoritesServices;

        public FavoritesController(IFavoritesServices favoritesServices) => this.favoritesServices = favoritesServices;

        // set(add) new favorite for user
        [HttpPost("{userId}/{adId}")]
        public async Task SetNewFavoite(string userId, int adId)
        {
            await favoritesServices.SetFavorite(userId, adId);
        }

        // get all user favoritres by user id
        [HttpGet("{userId}")]
        public async Task<IEnumerable<AdShortInfoDTO>> GetUserFavoritesAds(string userId)
        {
           return await favoritesServices.GetAllFavoritesByUserId(userId);
        }

        // Delete Favorite by useId and adId
        [HttpDelete("{userId}/{adId}")]
        public async Task DeleteFavorite(string userId, int adId)
        {
            await favoritesServices.RemoveFavoriteByUserIdAndAdId(userId, adId);
        }
    }
}
