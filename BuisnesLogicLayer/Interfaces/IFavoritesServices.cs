using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnesLogicLayer.Interfaces;
using BuisnesLogicLayer.Services;
using BuisnesLogicLayer.DTO;
namespace BuisnesLogicLayer.Interfaces
{
    public interface IFavoritesServices
    {
        public Task SetFavorite(string userId, int adId);

        public Task<IEnumerable<AdShortInfoDTO>> GetAllFavoritesByUserId(string userId);

        public Task RemoveFavoriteByUserIdAndAdId(string userId, int adId);
    }
}
