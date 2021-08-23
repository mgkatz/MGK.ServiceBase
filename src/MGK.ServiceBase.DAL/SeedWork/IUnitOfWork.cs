using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MGK.ServiceBase.DAL.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
		T Add<T>(T entity) where T : class, IDataUnit;

		void AddRange<T>(IEnumerable<T> entities) where T : class, IDataUnit;

		Task<int> CommitChangesAsync(CancellationToken cancellationToken = default);

		void Remove<T>(T entity) where T : class, IDataUnit;

		void RemoveByIds<T>(params object[] entityIds) where T : class, IDataUnit;

		void RemoveRange<T>(IEnumerable<T> entities) where T : class, IDataUnit;

		void Update<T>(T entity) where T : class, IDataUnit;
	}
}
