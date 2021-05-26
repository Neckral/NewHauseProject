using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnesLogicLayer.DTO;
using DataAccesLayer.Enteties;
using DataAccesLayer.Models;
using BlazorFront.Models;

namespace BlazorFront.Services
{
    public interface IAdServices
    {
        public Task<PagedList<AdShortInfoDTO>> GetAllAds(QueryStringParameters parameters);

        public Task<AdInfoDTO> GetAdById(int id);

        public Task AddNewAd(AdCreateDTO createAdDTO);

        public Task<AdEditDTO> GetAdForEdit(int id);

        public Task DeleteAdById(int id);

        public Task UpdateAd(AdEditDTO editAdDTO);

        public Task<IEnumerable<AdShortInfoDTO>> GetAdsByUserId(string userId);

        public Task<PagedList<AdShortInfoDTO>> GetAdsByOptions(AdToCompare adToCompare, QueryStringParameters parameters);
    }
}
