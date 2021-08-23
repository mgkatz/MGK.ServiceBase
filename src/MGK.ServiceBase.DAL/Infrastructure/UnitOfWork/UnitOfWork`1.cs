using MGK.Acceptance;
using MGK.ServiceBase.DAL.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MGK.ServiceBase.DAL.Infrastructure.UnitOfWork
{
	public abstract class UnitOfWork<TContext> : IUnitOfWork
		where TContext : DbContext
	{
		protected UnitOfWork(
			TContext context,
			ILogger<TContext> logger)
		{
			Ensure.Parameter.IsNotNull(context, nameof(context));
			Ensure.Parameter.IsNotNull(logger, nameof(logger));

			Context = context;
			Logger = logger;
		}

		protected TContext Context { get; }

		protected ILogger<TContext> Logger { get; }

		public virtual T Add<T>(T entity) where T : class, IDataUnit
		{
			var addOperation = Context.Set<T>().Add(entity);
			return addOperation.Entity;
		}

		public virtual void AddRange<T>(IEnumerable<T> entities) where T : class, IDataUnit
			=> Context.Set<T>().AddRange(entities);

		public virtual async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				Logger.LogWarning(DALResources.MessagesResources.WarningCommitCancelled);
				return -1;
			}

			return await Context.SaveChangesAsync(cancellationToken);
		}

		public virtual void Remove<T>(T entity) where T : class, IDataUnit
			=> Context.Set<T>().Remove(entity);

		public virtual void RemoveByIds<T>(params object[] entityIds) where T : class, IDataUnit
		{
			var entity = Context.Set<T>().Find(entityIds);
			Context.Set<T>().Remove(entity);
		}

		public virtual void RemoveRange<T>(IEnumerable<T> entities) where T : class, IDataUnit
			=> Context.Set<T>().RemoveRange(entities);

		public virtual void Update<T>(T entity) where T : class, IDataUnit
			=> Context.Set<T>().Update(entity);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				// Free managed resources
				Context?.Dispose();
			}

			// Free native resources if there are any.
		}
	}
}
