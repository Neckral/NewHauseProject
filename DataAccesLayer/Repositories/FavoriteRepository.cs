using System;
using System.Collections.Generic;
using System.Text;
using DataAccesLayer.Enteties;
using DataAccesLayer.Interfaces;
using System.Linq;
using DataAccesLayer.EF;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataAccesLayer.Repositories
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        public readonly string connectionString = "Server = (localdb)\\mssqllocaldb; Database = UAHP1; Trusted_Connection = True; MultipleActiveResultSets=true";

        public AppDBContext context;
        public FavoriteRepository(AppDBContext context) : base(context) => this.context = context;

        public async Task<IEnumerable<Favorite>> GetAllFavoritesByUserId(string userId)
        {
            var favorites = new List<Favorite>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand($"select * from Favorites where UserID = '{userId}'", connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        favorites.Add(new Favorite(reader.GetString(0), reader.GetInt32(1)));
                    }
                }
            }
            return favorites;

            //return await context.Favorites.Where(fav => fav.UserID == userId).ToListAsync();
        }

        public async Task RemoveFavoriteByUserIdAndAdId(string userId, int adId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand($"exec RemoveFavoriteByUserIdAndAdId '{userId}', {adId}", connection);
                await command.ExecuteNonQueryAsync();
            }
            //var favTemp = context.Favorites.ToList().Where(fav => fav.UserID == userId && fav.AdID == adId).FirstOrDefault();
            //context.Favorites.Remove(favTemp);
            //await context.SaveChangesAsync();
        }
    }
}
