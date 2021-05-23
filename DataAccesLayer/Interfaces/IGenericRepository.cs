using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccesLayer.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(int id);

        Task Add(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task DeleteById(int id);
    }
}
