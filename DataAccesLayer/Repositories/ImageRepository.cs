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
using System.Data;

namespace DataAccesLayer.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        private AppDBContext context;
        public ImageRepository(AppDBContext context) : base(context) => this.context = context;
        public readonly string connectionString = "Server = (localdb)\\mssqllocaldb; Database = UAHP1; Trusted_Connection = True; MultipleActiveResultSets=true";

        public async Task<IEnumerable<Image>> GetAllAdsImagesByAdId(int adId)
        {
            List<Image> images = new();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var result = connection.Query<Image>("GetImagesByAdId", new { adid = adId }, commandType: CommandType.StoredProcedure);
                foreach (var item in result) images.Add(new Image() { ID = item.ID, AdID = item.AdID, ImageFile = item.ImageFile });
            }
            return images != null ? images : null;
        }

        public async Task RemoveImageById(int imageId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                var result = connection.Query<Image>($"delete from Images where ID = {imageId}");
            }
        }
    }
}
