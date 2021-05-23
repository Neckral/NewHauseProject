using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccesLayer.Enteties;
using System.Threading.Tasks;

namespace DataAccesLayer.Interfaces
{
    public interface IAdRepository //: IGenericRepository<Ad>
    {
        Task<IEnumerable<Ad>> GetAll();

        Task<Ad> GetById(int id);

        Task Add(Ad entity);

        Task Update(Ad entity);

        Task Delete(Ad entity);

        Task DeleteById(int id);

        Task<IEnumerable<Ad>> GetAdsByUserId(string userdId);

        Task<IEnumerable<Ad>> GetAdsByOptions(AdToCompare adToCompare);
    }
}
