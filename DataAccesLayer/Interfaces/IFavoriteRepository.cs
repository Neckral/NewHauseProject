using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccesLayer.Enteties;

namespace DataAccesLayer.Interfaces
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        public Task<IEnumerable<Favorite>> GetAllFavoritesByUserId(string userId);

        public Task RemoveFavoriteByUserIdAndAdId(string userId, int adId);
    }
}
