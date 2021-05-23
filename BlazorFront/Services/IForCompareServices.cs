using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnesLogicLayer.DTO;
namespace BlazorFront.Services
{
    public interface IForCompareServices
    {
        public Task SetForCompare(string userId, int adId);

        public Task<IEnumerable<ForCompareDTO>> GetAllComparesByUserId(string userId);

        public Task RemoveCopareByUserIdAndAdId(string userId, int AdId);
    }
}
