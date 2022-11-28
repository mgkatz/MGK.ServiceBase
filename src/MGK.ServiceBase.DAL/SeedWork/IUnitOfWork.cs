using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;

namespace MGK.ServiceBase.DAL.SeedWork;

public interface IUnitOfWork : IDataAccessService, IDisposable
{
    void AcceptAllChanges();

    T Add<T>(T entity) where T : class, IDataUnit;

	void AddRange<T>(IEnumerable<T> entities) where T : class, IDataUnit;

    Task<int> CommitChangesAsync(bool acceptAllChangesOnSuccess = true, CancellationToken cancellationToken = default);

    IExecutionStrategy CreateExecutionStrategy();

    void Remove<T>(T entity) where T : class, IDataUnit;

	void RemoveByIds<T>(params object[] entityIds) where T : class, IDataUnit;

	void RemoveRange<T>(IEnumerable<T> entities) where T : class, IDataUnit;

	void Update<T>(T entity) where T : class, IDataUnit;
}
