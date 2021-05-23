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
    public class AdServices : IAdServices
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper mapper;
        public AdServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Database = unitOfWork;
            this.mapper = mapper;
        }

        /*--------------------Common Methods from Generic repository--------------------*/
        public async Task<IEnumerable<AdInfoDTO>> GetAllAds()
        {
            var allAds = await Database.AdRepository.GetAll();
            return await GetAdDTOs(allAds);
        }

        public async Task<AdInfoDTO> GetAdById(int id)
        {
            var ad = await Database.AdRepository.GetById(id);
            var owenerOfAd = await Database.UserRepository.GetById(ad.OwnerId);
            List<TagDTO> tagsDTOs = new List<TagDTO>();
            List<ImageEditInfoDTO> imageDTOs = new List<ImageEditInfoDTO>();

            // Get Tags
            tagsDTOs = mapper.Map<IEnumerable<Tag>, List<TagDTO>>(ad.tags);
            // Get Images
            imageDTOs = mapper.Map<IEnumerable<Image>, List<ImageEditInfoDTO>>(ad.images);

            var mappedAd = mapper.Map<Ad, AdInfoDTO>(ad);
            mappedAd.OwnerEmail = owenerOfAd.Email;
            mappedAd.OwnerPhone = owenerOfAd.PhoneNumber;
            mappedAd.tags = tagsDTOs;
            mappedAd.images = imageDTOs;

            return mappedAd;
        }

        public async Task AddNewAd(AdCreateDTO createAdDTO)
        {
            var mappedAd = mapper.Map<AdCreateDTO, Ad>(createAdDTO);
            mappedAd.images = mapper.Map<List<ImageCreateDTO>, IEnumerable<Image>>(createAdDTO.images);
            mappedAd.tags = mapper.Map<List<TagDTO>, IEnumerable<Tag>>(createAdDTO.tags);
            await Database.AdRepository.Add(mappedAd);
        }

        public async Task DeleteAdById(int id)
        {
            await Database.AdRepository.DeleteById(id);
        }

        public async Task UpdateAd(AdEditDTO editAdDTO)
        {
            var mappedAd = mapper.Map<AdEditDTO, Ad>(editAdDTO);
            await Database.AdRepository.Update(mappedAd);
        }

        public async Task<IEnumerable<AdInfoDTO>> GetAdsByUserId(string userId)
        {
            var allAds = await Database.AdRepository.GetAdsByUserId(userId);
            return await GetAdDTOs(allAds);
        }

        public async Task<IEnumerable<AdInfoDTO>> GetAdsByOptions(AdToCompare adToCompare)
        {
            var allAds = await Database.AdRepository.GetAdsByOptions(adToCompare);
            return await GetAdDTOs(allAds);
        }

        /*------------------------------Individual methods------------------------------*/
        public async Task<List<AdInfoDTO>> GetAdDTOs(IEnumerable<Ad> allAds)
        {
            var adsDTO = new List<AdInfoDTO>();
            foreach (var ad in allAds)
            {
                List<TagDTO> tagsDTOs = new();
                List<ImageEditInfoDTO> imageDTOs = new();

                // Get Tags
                tagsDTOs = mapper.Map<IEnumerable<Tag>, List<TagDTO>>(ad.tags);
                // Get Images
                imageDTOs = mapper.Map<IEnumerable<Image>, List<ImageEditInfoDTO>>(ad.images);

                var owenerOfAd = await Database.UserRepository.GetById(ad.OwnerId);

                var mappedAd = mapper.Map<Ad, AdInfoDTO>(ad);
                mappedAd.OwnerEmail = owenerOfAd.Email;
                mappedAd.OwnerPhone = owenerOfAd.PhoneNumber;
                mappedAd.tags = tagsDTOs;
                mappedAd.images = imageDTOs;
                adsDTO.Add(mappedAd);

            }
            return adsDTO;
        }

        public async Task<AdEditDTO> GetAdToEdit(int id)
        {
            var ad = await Database.AdRepository.GetById(id);
            return mapper.Map<Ad, AdEditDTO>(ad);
        }
    }
}
