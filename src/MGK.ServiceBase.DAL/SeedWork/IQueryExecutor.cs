using MGK.ServiceBase.CQRS.SeedWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MGK.ServiceBase.DAL.SeedWork
{
    public interface IQueryExecutor
    {
		/// <summary>
		/// Runs a sql query that returns multiple result sets.
		/// </summary>
		/// <param name="resultSetTypes">A dictionary with the list of the name of each result set and the type of the records to return</param>
		/// <param name="sqlQuery">The sql query to run.</param>
		/// <param name="queryParameters">Optional: the parameters for the query.</param>
		/// <param name="transaction">Optional: a transaction where the query must be executed.</param>
		/// <param name="timeout">Optional: the timeout for the query. By default takes what is setup in the configuration.</param>
		/// <returns>A dictionay with the name of each result set and their records.</returns>
		Task<IDictionary<string, IEnumerable>> GetMultipleResultSetsAsync(
			IDictionary<string, Type> resultSetTypes,
			string sqlQuery,
			object queryParameters = null,
			IDbTransaction transaction = null,
			int? timeout = null);

		/// <summary>
		/// Runs a sql query that returns the records from a single result set.
		/// </summary>
		/// <typeparam name="TRecord">The type of the class that represents the structure of the record.</typeparam>
		/// <param name="sqlQuery">The sql query to run.</param>
		/// <param name="queryParameters">Optional: the parameters for the query.</param>
		/// <param name="transaction">Optional: a transaction where the query must be executed.</param>
		/// <param name="timeout">Optional: the timeout for the query. By default takes what is setup in the configuration.</param>
		/// <returns>The records of the result set.</returns>
		Task<IEnumerable<TRecord>> GetResultSetAsync<TRecord>(
			string sqlQuery,
			object queryParameters = null,
			IDbTransaction transaction = null,
			int? timeout = null)
			where TRecord : class, IResponse;

		/// <summary>
		/// Runs a sql query that returns a single record from a single result set.
		/// </summary>
		/// <typeparam name="TRecord">The type of the class that represents the structure of the record.</typeparam>
		/// <param name="sqlQuery">The sql query to run.</param>
		/// <param name="queryParameters">Optional: the parameters for the query.</param>
		/// <param name="transaction">Optional: a transaction where the query must be executed.</param>
		/// <param name="timeout">Optional: the timeout for the query. By default takes what is setup in the configuration.</param>
		/// <returns>The single record of the result set.</returns>
		Task<TRecord> GetSingleResultSetAsync<TRecord>(
			string sqlQuery,
			object queryParameters = null,
			IDbTransaction transaction = null,
			int? timeout = null)
			where TRecord : class, IResponse;

		/// <summary>
		/// Runs a sql query that return one single value.
		/// </summary>
		/// <typeparam name="TValue">The type of the value to return.</typeparam>
		/// <param name="sqlQuery">The sql query to run.</param>
		/// <param name="queryParameters">Optional: the parameters for the query.</param>
		/// <param name="transaction">Optional: a transaction where the query must be executed.</param>
		/// <param name="timeout">Optional: the timeout for the query. By default takes what is setup in the configuration.</param>
		/// <returns>A value.</returns>
		Task<TValue> GetValueAsync<TValue>(
			string sqlQuery,
			object queryParameters = null,
			IDbTransaction transaction = null,
			int? timeout = null);
	}
}
