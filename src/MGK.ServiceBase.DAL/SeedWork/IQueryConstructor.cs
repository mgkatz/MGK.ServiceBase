using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MGK.ServiceBase.DAL.SeedWork
{
    /// <summary>
    /// A contract to work with queries.
    /// </summary>
	public interface IQueryConstructor
	{
        /// <summary>
        /// Get the first record.
        /// </summary>
        /// <typeparam name="TOutput">The type of the object to return.</typeparam>
        /// <param name="withTracking">Allows to set the tracking of the record.</param>
        /// <returns>The first record.</returns>
        Task<TOutput> GetFirstRecordAsync<TOutput>(bool withTracking = false);

        /// <summary>
        /// Get the only record that matches with the filters applied.
        /// </summary>
        /// <typeparam name="TOutput">The type of the object to return.</typeparam>
        /// <param name="withTracking">Allows to set the tracking of the record.</param>
        /// <returns>The record searched.</returns>
        Task<TOutput> GetRecordAsync<TOutput>(bool withTracking = false);

        /// <summary>
        /// Get a group of records as an array.
        /// </summary>
        /// <typeparam name="TOutput">The type of the object to return.</typeparam>
        /// <param name="withTracking">Allows to set the tracking of the records.</param>
        /// <returns>An array of records.</returns>
        Task<TOutput[]> QueryAsArrayAsync<TOutput>(bool withTracking = false);

        /// <summary>
        /// Get a group of records as an IEnumerable.
        /// </summary>
        /// <typeparam name="TOutput">The type of the object to return.</typeparam>
        /// <param name="withTracking">Allows to set the tracking of the records.</param>
        /// <returns>An IEnumerable with the records.</returns>
        Task<IEnumerable<TOutput>> QueryAsEnumerableAsync<TOutput>(bool withTracking = false);

        /// <summary>
        /// Get a group of records as a list.
        /// </summary>
        /// <typeparam name="TOutput">The type of the object to return.</typeparam>
        /// <param name="withTracking">Allows to set the tracking of the records.</param>
        /// <returns>A list of records.</returns>
        Task<List<TOutput>> QueryAsListAsync<TOutput>(bool withTracking = false);
    }

    /// <summary>
    /// A contract to work with queries.
    /// </summary>
    /// <typeparam name="TEntity">The entity to work with.</typeparam>
    /// <typeparam name="TQueryConstructor">The IQueryConstructor to work with.</typeparam>
    public interface IQueryConstructor<TEntity, out TQueryConstructor> : IQueryConstructor
        where TEntity : IDataUnit
        where TQueryConstructor : IQueryConstructor
    {
        /// <summary>
        /// Get the first record.
        /// </summary>
        /// <param name="withTracking">Allows to set the tracking of the record.</param>
        /// <returns>The first record.</returns>
        Task<TEntity> GetFirstRecordAsync(bool withTracking = false);

        /// <summary>
        /// Get the only record that matches with the filters applied.
        /// </summary>
        /// <param name="withTracking">Allows to set the tracking of the record.</param>
        /// <returns>The record searched.</returns>
        Task<TEntity> GetRecordAsync(bool withTracking = false);

        /// <summary>
        /// Get a group of records as an IQueryable.
        /// </summary>
        /// <param name="withTracking">Allows to set the tracking of the records.</param>
        /// <returns>An IQueryable of records.</returns>
        IQueryable<TEntity> Query(bool withTracking = false);

        /// <summary>
        /// Get a group of records as an array.
        /// </summary>
        /// <param name="withTracking">Allows to set the tracking of the records.</param>
        /// <returns>An array of records.</returns>
        Task<TEntity[]> QueryAsArrayAsync(bool withTracking = false);

        /// <summary>
        /// Get a group of records as an IEnumerable.
        /// </summary>
        /// <param name="withTracking">Allows to set the tracking of the records.</param>
        /// <returns>An IEnumerable with the records.</returns>
        Task<IEnumerable<TEntity>> QueryAsEnumerableAsync(bool withTracking = false);

        /// <summary>
        /// Get a group of records as a list.
        /// </summary>
        /// <param name="withTracking">Allows to set the tracking of the records.</param>
        /// <returns>A list of records.</returns>
        Task<List<TEntity>> QueryAsListAsync(bool withTracking = false);

        /// <summary>
        /// Allows to set entity that will be queried.
        /// </summary>
        /// <returns>An IQueryConstructor.</returns>
        TQueryConstructor Start();
    }
}
