using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnesLogicLayer.DTO;
using DataAccesLayer.Enteties;

namespace BlazorFront.Services
{
    public interface IAdServices
    {
        public Task<IEnumerable<AdShortInfoDTO>> GetAllAds();

        public Task<AdInfoDTO> GetAdById(int id);

        public Task AddNewAd(AdCreateDTO createAdDTO);

        public Task<AdEditDTO> GetAdForEdit(int id);

        public Task DeleteAdById(int id);

        public Task UpdateAd(AdEditDTO editAdDTO);

        public Task<IEnumerable<AdShortInfoDTO>> GetAdsByUserId(string userId);

        public Task<IEnumerable<AdShortInfoDTO>> GetAdsByOptions(AdToCompare adToCompare);
    }
}
