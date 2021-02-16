using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGK.ServiceBase.DAL.SeedWork
{
	public interface IQueryBuilder
	{
        Task<TOutput> GetRecordAsync<TOutput>(bool withTracking = false);
        Task<TOutput[]> QueryAsArrayAsync<TOutput>(bool withTracking = false);
        Task<IEnumerable<TOutput>> QueryAsEnumerableAsync<TOutput>(bool withTracking = false);
        Task<List<TOutput>> QueryAsListAsync<TOutput>(bool withTracking = false);
    }

    public interface IQueryBuilder<TEntity, out TQueryBuilder> : IQueryBuilder
        where TEntity : IDataUnit
        where TQueryBuilder : IQueryBuilder
    {
        Task<TEntity> GetFirstRecordAsync(bool withTracking = false);
        Task<TOutput> GetFirstRecordAsync<TOutput>(bool withTracking = false);
        Task<TEntity> GetRecordAsync(bool withTracking = false);
        IQueryable<TEntity> Query(bool withTracking = false);
        Task<TEntity[]> QueryAsArrayAsync(bool withTracking = false);
        Task<IEnumerable<TEntity>> QueryAsEnumerableAsync(bool withTracking = false);
        Task<List<TEntity>> QueryAsListAsync(bool withTracking = false);
        TQueryBuilder Start();
    }
}
