using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnesLogicLayer.DTO;
namespace BlazorFront.Services
{
    public interface IImageServices
    {
        public Task<IEnumerable<ImageEditInfoDTO>> GetImagesByAdId(int adId);
    }
}
