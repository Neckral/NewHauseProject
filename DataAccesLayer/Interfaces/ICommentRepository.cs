using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesLayer.Enteties;

namespace DataAccesLayer.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        public Task<IEnumerable<Comment>> GetCommentsByAdId(int adId);
        public Task RemoveCommentByUserIdAndAdId(string userId,int adId);
    }
}
