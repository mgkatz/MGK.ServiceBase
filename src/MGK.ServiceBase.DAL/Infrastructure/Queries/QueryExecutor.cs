using Dapper;
using MGK.Acceptance;
using MGK.ServiceBase.CQRS.SeedWork;
using MGK.ServiceBase.DAL.SeedWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MGK.ServiceBase.DAL.Infrastructure.Queries
{
	public abstract class QueryExecutor : IQueryExecutor
	{
		private readonly ISqlConnectionFactory _sqlConnectionFactory;
		private readonly string _database;

		protected QueryExecutor(ISqlConnectionFactory sqlConnectionFactory, string database)
		{
			Ensure.Parameter.IsNotNull(sqlConnectionFactory, nameof(sqlConnectionFactory));
			Ensure.Parameter.IsNotNull(database, nameof(database));

			_sqlConnectionFactory = sqlConnectionFactory;
			_database = database;
		}

		/// <summary>
		/// Runs a sql query that returns multiple result sets.
		/// </summary>
		/// <param name="resultSetTypes">A dictionary with the list of the name of each result set and the type of the records to return</param>
		/// <param name="sqlQuery">The sql query to run.</param>
		/// <param name="queryParameters">Optional: the parameters for the query.</param>
		/// <param name="transaction">Optional: a transaction where the query must be executed.</param>
		/// <param name="timeout">Optional: the timeout for the query. By default takes what is setup in the configuration.</param>
		/// <returns>A dictionay with the name of each result set and their records.</returns>
		public virtual async Task<IDictionary<string, IEnumerable>> GetMultipleResultSetsAsync(
			IDictionary<string, Type> resultSetTypes,
			string sqlQuery,
			object queryParameters = null,
			IDbTransaction transaction = null,
			int? timeout = null)
		{
			EvaluateParametersInternal(sqlQuery, ref timeout);

			var results = await GetConnectionInternal()
				.QueryMultipleAsync(
					sql: sqlQuery,
					param: queryParameters,
					transaction: transaction,
					commandTimeout: timeout,
					commandType: CommandType.Text);
			var resultSets = new Dictionary<string, IEnumerable>();

			foreach (var resultSetType in resultSetTypes)
			{
				var records = results.Read(resultSetType.Value).AsList();
				resultSets.Add(resultSetType.Key, records.ToList());
			}

			return resultSets;
		}

		/// <summary>
		/// Runs a sql query that returns the records from a single result set.
		/// </summary>
		/// <typeparam name="TRecord">The type of the class that represents the structure of the record.</typeparam>
		/// <param name="sqlQuery">The sql query to run.</param>
		/// <param name="queryParameters">Optional: the parameters for the query.</param>
		/// <param name="transaction">Optional: a transaction where the query must be executed.</param>
		/// <param name="timeout">Optional: the timeout for the query. By default takes what is setup in the configuration.</param>
		/// <returns>The records of the result set.</returns>
		public virtual async Task<IEnumerable<TRecord>> GetResultSetAsync<TRecord>(
			string sqlQuery,
			object queryParameters = null,
			IDbTransaction transaction = null,
			int? timeout = null)
			where TRecord : class, IResponse
		{
			EvaluateParametersInternal(sqlQuery, ref timeout);

			var results = await GetConnectionInternal()
				.QueryAsync<TRecord>(
					sql: sqlQuery,
					param: queryParameters,
					transaction: transaction,
					commandTimeout: timeout,
					commandType: CommandType.Text);

			return results.AsList();
		}

		/// <summary>
		/// Runs a sql query that returns a single record from a single result set.
		/// </summary>
		/// <typeparam name="TRecord">The type of the class that represents the structure of the record.</typeparam>
		/// <param name="sqlQuery">The sql query to run.</param>
		/// <param name="queryParameters">Optional: the parameters for the query.</param>
		/// <param name="transaction">Optional: a transaction where the query must be executed.</param>
		/// <param name="timeout">Optional: the timeout for the query. By default takes what is setup in the configuration.</param>
		/// <returns>The single record of the result set.</returns>
		public virtual async Task<TRecord> GetSingleResultSetAsync<TRecord>(
			string sqlQuery,
			object queryParameters = null,
			IDbTransaction transaction = null,
			int? timeout = null)
			where TRecord : class, IResponse
		{
			EvaluateParametersInternal(sqlQuery, ref timeout);

			return await GetConnectionInternal()
				.QuerySingleOrDefaultAsync<TRecord>(
					sql: sqlQuery,
					param: queryParameters,
					transaction: transaction,
					commandTimeout: timeout,
					commandType: CommandType.Text);
		}

		/// <summary>
		/// Runs a sql query that return one single value.
		/// </summary>
		/// <typeparam name="TValue">The type of the value to return.</typeparam>
		/// <param name="sqlQuery">The sql query to run.</param>
		/// <param name="queryParameters">Optional: the parameters for the query.</param>
		/// <param name="transaction">Optional: a transaction where the query must be executed.</param>
		/// <param name="timeout">Optional: the timeout for the query. By default takes what is setup in the configuration.</param>
		/// <returns>A value.</returns>
		public virtual async Task<TValue> GetValueAsync<TValue>(
			string sqlQuery,
			object queryParameters = null,
			IDbTransaction transaction = null,
			int? timeout = null)
		{
			EvaluateParametersInternal(sqlQuery, ref timeout);

			var result = await GetConnectionInternal()
				.QuerySingleOrDefaultAsync<TValue>(
					sql: sqlQuery,
					param: queryParameters,
					transaction: transaction,
					commandTimeout: timeout,
					commandType: CommandType.Text);

			return result;
		}

		private void EvaluateParametersInternal(string sqlQuery, ref int? timeout)
		{
			Ensure.Parameter.IsNotNullNorEmptyNorWhiteSpace(sqlQuery, nameof(sqlQuery));

			if (timeout == null)
			{
				timeout = _sqlConnectionFactory.GetTimeoutForQueries(_database);
			}
		}

		private IDbConnection GetConnectionInternal()
		{
			var connection = _sqlConnectionFactory.GetOpenConnection(_database);
			Ensure.Value.IsNotNull(connection);

			return connection;
		}
	}
}
