using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesLayer.Enteties;

namespace DataAccesLayer.Interfaces
{
    public interface IForCompareRepository : IGenericRepository<ForCompare>
    {
        public Task<IEnumerable<ForCompare>> GetAllComparesByUserId(string userId);
        public Task RemoveCopareByUserIdAndAdId(string userId, int AdId);
    }
}
