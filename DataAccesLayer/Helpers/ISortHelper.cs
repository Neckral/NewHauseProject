using System.Linq;
using DataAccesLayer.Models;

namespace DataAccesLayer.Helpers
{
    /// <summary>
    /// Sorting interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ISortHelper<TEntity> where TEntity : class
    {
        IQueryable<TEntity> ApplySort(IQueryable<TEntity> entities, QueryStringParameters queryString);
    }
}
