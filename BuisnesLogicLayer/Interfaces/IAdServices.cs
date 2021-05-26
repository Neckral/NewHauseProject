using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnesLogicLayer.DTO;
using DataAccesLayer.Enteties;
using DataAccesLayer.Helpers;
using DataAccesLayer.Models;

namespace BuisnesLogicLayer.Interfaces
{
    public interface IAdServices
    {
        /*--------------------Common Methods from Generic repository--------------------*/
        public Task<PagedList<AdShortInfoDTO>> GetAllAds(QueryStringParameters parameters);

        public Task<AdInfoDTO> GetAdById(int id);

        public Task<AdEditDTO> GetAdToEdit(int id);

        public Task AddNewAd(AdCreateDTO createAdDTO);

        public Task DeleteAdById(int id);

        public Task UpdateAd(AdEditDTO editAdDTO);

        public Task<IEnumerable<AdShortInfoDTO>> GetAdsByUserId(string userId);

        public Task<PagedList<AdShortInfoDTO>> GetAdsByOptions(AdToCompare adToCompare,QueryStringParameters parameters);

        /*------------------------------Individual methods------------------------------*/
       

    }
}
