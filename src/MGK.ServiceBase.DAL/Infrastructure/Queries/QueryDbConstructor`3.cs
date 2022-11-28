using AutoMapper;
using Microsoft.EntityFrameworkCore.Query;

namespace MGK.ServiceBase.DAL.Infrastructure.Queries;

public abstract class QueryDbConstructor<TContext, TEntity, TQueryConstructor> : QueryConstructorBase<TContext, TEntity, TQueryConstructor>
    where TContext : DbContext
    where TEntity : class, IDataUnit
    where TQueryConstructor : class, IQueryConstructor
{
    protected QueryDbConstructor(TContext context, IMapper mapper)
        : base(context, mapper)
    {
    }

    public override IQueryable<TEntity> Query(bool withTracking = false)
        => withTracking ? QueryEntity.AsTracking() : QueryEntity.AsNoTracking();

    public override TQueryConstructor Start()
    {
        SetQueryEntity(Context.Set<TEntity>().AsQueryable());
        return this as TQueryConstructor;
    }

    protected override TQueryConstructor IncludeWith(
        Expression<Func<TEntity, object>> property,
        IEnumerable<Expression<Func<object, object>>> withProperties,
        bool isMultiLevel = false)
    {
        if (isMultiLevel)
        {
            // A multi-level IncludeWith allows to perform nested ThenInclude queries.
            IIncludableQueryable<TEntity, object> queryable = QueryEntity.Include(property);

            foreach (Expression<Func<object, object>> withProperty in withProperties)
                queryable = queryable.ThenInclude(withProperty);

            SetQueryEntity(queryable);
        }
        else
        {
            // A non multi-level IncludeWith allows to perform an Include with one ThenInclude.
            foreach (Expression<Func<object, object>> withProperty in withProperties)
                SetQueryEntity(QueryEntity.Include(property).ThenInclude(withProperty));
        }

        return this as TQueryConstructor;
    }
}
