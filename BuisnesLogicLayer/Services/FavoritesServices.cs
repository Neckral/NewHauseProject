using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesLayer.EF;
using BuisnesLogicLayer.DTO;
using BuisnesLogicLayer.Interfaces;
using DataAccesLayer;
using DataAccesLayer.Interfaces;
using DataAccesLayer.Enteties;
using BuisnesLogicLayer.Converters;
using DataAccesLayer.Repositories;
using AutoMapper;

namespace BuisnesLogicLayer.Services
{
    public class FavoritesServices : IFavoritesServices
    {
        private IUnitOfWork Database;
        private readonly IMapper mapper;
        public FavoritesServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Database = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<AdShortInfoDTO>> GetAllFavoritesByUserId(string userId)
        {
            var favorites = await Database.FavoriteRepository.GetAllFavoritesByUserId(userId);
            List<AdShortInfoDTO> adShortInfoDTOs = new();
            foreach (var favorite in favorites)
            {
                var ad = await Database.AdRepository.GetById(favorite.AdID);
                var mappedAd = mapper.Map<Ad, AdShortInfoDTO>(ad);
                mappedAd.images = mapper.Map<IEnumerable<Image>, List<ImageEditInfoDTO>>(ad.images);
                adShortInfoDTOs.Add(mappedAd);
            }
            return adShortInfoDTOs;
        }

        public async Task RemoveFavoriteByUserIdAndAdId(string userId, int adId)
        {
            await Database.FavoriteRepository.RemoveFavoriteByUserIdAndAdId(userId, adId);
        }

        public async Task SetFavorite(string userId, int adId)
        {
            await Database.FavoriteRepository.Add(new Favorite(userId, adId));
        }
    }
}

