using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Enteties;
using DataAccesLayer.Interfaces;
using DataAccesLayer.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccesLayer.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private AppDBContext context;
        public CommentRepository(AppDBContext context) : base(context)=> this.context = context;

        public async Task<IEnumerable<Comment>> GetCommentsByAdId(int adId)
        {
            return await context.Comments.Where(coment => coment.AdId == adId).ToListAsync();
        }
    }
}
