using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnesLogicLayer.DTO;
using BuisnesLogicLayer.Interfaces;
using DataAccesLayer.Interfaces;
using AutoMapper;

namespace BuisnesLogicLayer.Services
{
    public class ImageServices : IImageServices
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper mapper;
        public ImageServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Database = unitOfWork;
            this.mapper = mapper;
        }

        public async Task DeleteImageById(int id)
        {
            await Database.ImageRepository.RemoveImageById(id);
            //await Database.ImageRepository.DeleteById(id);
        }

        public async Task<IEnumerable<ImageEditInfoDTO>> GetImagesByAdId(int adId)
        {
            List<ImageEditInfoDTO> imageDTOs = new List<ImageEditInfoDTO>();
            var images = await Database.ImageRepository.GetAllAdsImagesByAdId(adId);

            foreach (var item in images)
            {
                imageDTOs.Add(new ImageEditInfoDTO() { ImageFile = item.ImageFile, ID = item.ID, AdID = item.AdID });
            }

            return imageDTOs;
        }
    }
}
