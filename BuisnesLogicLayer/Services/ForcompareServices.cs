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
    public class ForcompareServices : IForCompareServices
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper mapper;
        public ForcompareServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Database = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<ForCompareDTO>> GetAllComparesByUserId(string userId)
        {
            var forCompares = await Database.ForCompareRepository.GetAllComparesByUserId(userId);
            List<ForCompareDTO> forCompareDTOs = new();
            foreach (var coparasion in forCompares)
            {
                var ad = await Database.AdRepository.GetById(coparasion.AdID);
                var mappedAd = mapper.Map<Ad, ForCompareDTO>(ad);
                mappedAd.images = mapper.Map<IEnumerable<Image>, List<ImageEditInfoDTO>>(ad.images);
                forCompareDTOs.Add(mappedAd);
            }
            return forCompareDTOs;
        }

        public async Task RemoveCopareByUserIdAndAdId(string userId, int AdId)
        {
           await Database.ForCompareRepository.RemoveCopareByUserIdAndAdId(userId, AdId);
        }

        public async Task SetForCompare(string userId, int adId)
        {
            await Database.ForCompareRepository.Add(new ForCompare(userId, adId));
        }
    }
}
