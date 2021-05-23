using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccesLayer.Enteties;

namespace DataAccesLayer.Interfaces
{
    public interface IImageRepository : IGenericRepository<Image>
    {
        public Task<IEnumerable<Image>> GetAllAdsImagesByAdId(int adId);
        public Task RemoveImageById(int imageId);
    }
}
