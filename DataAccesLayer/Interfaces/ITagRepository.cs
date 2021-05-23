using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataAccesLayer.Enteties;

namespace DataAccesLayer.Interfaces
{
    public interface ITagRepository : IGenericRepository<Tag>
    {
        public Task<IEnumerable<Tag>> GetTagsFromAdByAdsId(int adId);
        public Task AddTagDapper(Tag tag);
        public Task AddTagToAdDapper(int adId, Tag tag);

    }
}
