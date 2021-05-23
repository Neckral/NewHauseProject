using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Enteties;
using DataAccesLayer.Interfaces;
using DataAccesLayer.EF;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;

namespace DataAccesLayer.Repositories
{
    public class AdRepository : IAdRepository
    {
        public readonly string connectionString = "Server = (localdb)\\mssqllocaldb; Database = UAHP1; Trusted_Connection = True; MultipleActiveResultSets=true";

        private AppDBContext context;
        public AdRepository(AppDBContext context) => this.context = context;

        public async Task Add(Ad entity)
        {
            List<Tag> tags = entity.tags.GroupBy(t => t.Tag_).Select(t => t.First()).ToList();
            entity.tags = null;
            await context.Ads.AddAsync(entity);
            await context.SaveChangesAsync();
            int adId = entity.ID;

            foreach (var tag in tags)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    var allTags = await context.Tags.ToListAsync();
                    if (allTags.All(localtag => localtag.Tag_ != tag.Tag_))
                    {
                        connection.Query($"insert into Tags (Tag_) values ('{tag.Tag_}')");
                    }
                }
            }

            foreach (var tag in tags)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                        connection.Query($"insert into AdTag(adsID,tagsTag_) values ({adId},'{tag.Tag_}')");
                }
            }
        }

        public async Task Delete(Ad entityToDelete)
        {
                context.Ads.Remove(entityToDelete);
                await context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
                var entityToDelete = await context.Ads.FindAsync(id);
                await this.Delete(entityToDelete);
        }

        public async Task<IEnumerable<Ad>> GetAdsByOptions(AdToCompare adToCompare)
        {
            List<Ad> allAds = await context.Ads.Include(c => c.comments)
                            .Include(i => i.images)
                            .Include(t => t.tags)
                            .ToListAsync();

            List<Ad> resutlAds = new List<Ad>();
            foreach (var ad in allAds)
            {
                if (adToCompare.MaxPrice != 0)
                {
                    if (adToCompare.MaxPrice < ad.Price) continue;
                }

                //if(adToCompare.Region != "")
                //{
                //    if (adToCompare.Region != ad.Region) continue;
                //}

                //if (adToCompare.District != "")
                //{
                //    if (adToCompare.District != ad.District) continue;
                //}

                //if (adToCompare.City != "")
                //{
                //    if (adToCompare.City != ad.City) continue;
                //}

                if (adToCompare.HouseType != "" && !adToCompare.HouseTypeNoMatter)
                {
                    if (adToCompare.HouseType != ad.HouseType) continue;
                }

                if (adToCompare.MaxAreaOfHouse != 0)
                {
                    if (adToCompare.MaxAreaOfHouse < ad.AreaOfHouse) continue;
                }

                if (adToCompare.MaxFloorAmount != 0)
                {
                    if (adToCompare.MaxFloorAmount < ad.FloorAmount) continue;
                }

                if (adToCompare.MaxRoomNumber != 0)
                {
                    if (adToCompare.MaxRoomNumber < ad.RoomNumber) continue;
                }

                if (!adToCompare.PoolNoMatter)
                {
                    if (adToCompare.Pool != ad.Pool) continue;
                }

                if (!adToCompare.BalkonNoMatter)
                {
                    if (adToCompare.Balkon != ad.Balkon) continue;
                }

                if (!adToCompare.PurchaseOportunityNoMatter)
                {
                    if (adToCompare.PurchaseOportunity != ad.PurchaseOportunity) continue;
                }
                resutlAds.Add(ad);
            }

            return resutlAds;
        }

        public async Task<IEnumerable<Ad>> GetAdsByUserId(string userId)
        {

                return await context.Ads.Include(c => c.comments)
                            .Include(i => i.images)
                            .Include(t => t.tags)
                            .Where(ad => ad.OwnerId == userId)
                            .ToListAsync();
            
        }

        public async Task<IEnumerable<Ad>> GetAll()
        {
                return await context.Ads.Include(c => c.comments)
                            .Include(i => i.images)
                            .Include(t => t.tags)
                            .ToListAsync();
        }

        public async Task<Ad> GetById(int id)
        {
                return await context.Ads.Include(c => c.comments)
                            .Include(i => i.images)
                            .Include(t => t.tags)
                            .Where(ad => ad.ID == id)
                            .FirstOrDefaultAsync();
        }

        public async Task Update(Ad ad)
        {
                context.Ads.Update(ad);
                await context.SaveChangesAsync();
        }
    }
}
