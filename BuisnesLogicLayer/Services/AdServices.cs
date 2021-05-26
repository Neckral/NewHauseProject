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
using DataAccesLayer.Helpers;
using DataAccesLayer.Models;

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
        public async Task<PagedList<AdShortInfoDTO>> GetAllAds(QueryStringParameters parameters)
        {
            var allAds = await Database.AdRepository.GetAll();
            List<AdShortInfoDTO> adShortInfoDTOs = new();
            foreach (var ad in allAds)
            {
                var mappedAd = mapper.Map<Ad, AdShortInfoDTO>(ad);
                mappedAd.images = mapper.Map<IEnumerable<Image>, List<ImageEditInfoDTO>>(ad.images);
                adShortInfoDTOs.Add(mappedAd);
            }
            var result = PagedList<AdShortInfoDTO>.ToPagedList(adShortInfoDTOs, parameters.PageNumber, parameters.PageSize);
            return result;
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

        public async Task<IEnumerable<AdShortInfoDTO>> GetAdsByUserId(string userId)
        {
            var allAds = await Database.AdRepository.GetAdsByUserId(userId);
            List<AdShortInfoDTO> adShortInfoDTOs = new();
            foreach (var ad in allAds)
            {
                var mappedAd = mapper.Map<Ad, AdShortInfoDTO>(ad);
                mappedAd.images = mapper.Map<IEnumerable<Image>, List<ImageEditInfoDTO>>(ad.images);
                adShortInfoDTOs.Add(mappedAd);
            }
            return adShortInfoDTOs;
        }

        public async Task<PagedList<AdShortInfoDTO>> GetAdsByOptions(AdToCompare adToCompare,QueryStringParameters parameters)
        {
            var allAds = await Database.AdRepository.GetAdsByOptions(adToCompare);
            List<AdShortInfoDTO> adShortInfoDTOs = new();
            foreach (var ad in allAds)
            {
                var mappedAd = mapper.Map<Ad, AdShortInfoDTO>(ad);
                mappedAd.images = mapper.Map<IEnumerable<Image>, List<ImageEditInfoDTO>>(ad.images);
                adShortInfoDTOs.Add(mappedAd);
            }
            var result = PagedList<AdShortInfoDTO>.ToPagedList(adShortInfoDTOs, parameters.PageNumber, parameters.PageSize);
            return result;
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
