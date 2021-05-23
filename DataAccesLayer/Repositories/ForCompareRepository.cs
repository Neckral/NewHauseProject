using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Enteties;
using DataAccesLayer.Interfaces;
using DataAccesLayer.EF;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataAccesLayer.Repositories
{
    public class ForCompareRepository : GenericRepository<ForCompare>, IForCompareRepository
    {
        private AppDBContext context;
        public ForCompareRepository(AppDBContext context) : base(context) => this.context = context;


        public async Task<IEnumerable<ForCompare>> GetAllComparesByUserId(string userId)
        {
            return await context.ForCompares.Where(fc => fc.UserID == userId).ToListAsync();
        }

        public async Task RemoveCopareByUserIdAndAdId(string userId, int AdId)
        {
            var compareToRemove = context.ForCompares.ToList().Where(fc => fc.UserID == userId && fc.AdID == AdId).FirstOrDefault();
            context.ForCompares.Remove(compareToRemove);
            await context.SaveChangesAsync();
        }
    }
}
