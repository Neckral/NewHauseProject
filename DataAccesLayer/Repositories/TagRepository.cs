using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Enteties;
using DataAccesLayer.Interfaces;
using DataAccesLayer.EF;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Data.SqlClient;

namespace DataAccesLayer.Repositories
{
    public class TagRepository :GenericRepository<Tag>, ITagRepository
    {
        public readonly string connectionString = "Server = (localdb)\\mssqllocaldb; Database = UAHP1; Trusted_Connection = True; MultipleActiveResultSets=true";

        private AppDBContext context;
        public TagRepository(AppDBContext context) : base(context) => this.context = context;

        public async Task<IEnumerable<Tag>> GetTagsFromAdByAdsId(int adId)
        {
            return await context.Tags.ToListAsync();
        }

        public async Task AddTagDapper(Tag tag)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var allTags = await context.Tags.ToListAsync();
                if (allTags.All(localtag => localtag.Tag_ != tag.Tag_))
                {
                    connection.Query($"insert into Tags (Tag_) values ('${tag}')");
                }
            }
        }

        public async Task AddTagToAdDapper(int adId, Tag tag)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var allTagsAd = connection.Query<AdTag>("Select * from AdTag"); 
                if (allTagsAd.All(localtag => localtag.tagsTag_ != tag.Tag_))
                {
                    connection.Query($"insert into AdTag(adsID,tagsTag_) values ({adId},'${tag.Tag_}')");
                }
            }
        }
    }
}
