using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnesLogicLayer.DTO;

namespace BuisnesLogicLayer.Interfaces
{
    public interface IImageServices
    {
        Task<IEnumerable<ImageEditInfoDTO>> GetImagesByAdId(int adId);
        Task DeleteImageById(int id);
    }
}
