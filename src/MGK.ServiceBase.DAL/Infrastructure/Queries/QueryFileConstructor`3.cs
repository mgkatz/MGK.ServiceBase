// TODO: this is WIP. It is not ready yet.
using AutoMapper;

namespace MGK.ServiceBase.DAL.Infrastructure.Queries;

public abstract class QueryFileConstructor<TContext, TEntity, TQueryConstructor> : QueryConstructorBase<TContext, TEntity, TQueryConstructor>
    where TContext : FileContext
    where TEntity : class, IDataUnit
    where TQueryConstructor : class, IQueryConstructor
{
    protected QueryFileConstructor(TContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    public override IQueryable<TEntity> Query(bool withTracking = false) => QueryEntity;

    public override TQueryConstructor Start()
        => this as TQueryConstructor;

    public TQueryConstructor Start(string propertyName)
    {
        //Context.GetType().GetProperty(propertyName).GetValue()
        return Start();
    }

    protected override TQueryConstructor IncludeWith(
        Expression<Func<TEntity, object>> property,
        IEnumerable<Expression<Func<object, object>>> withProperties,
        bool isMultiLevel = false)
    {
        throw new NotImplementedException();
    }
}
