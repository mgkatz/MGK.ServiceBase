using AutoMapper;
using AutoMapper.QueryableExtensions;
using MGK.ServiceBase.DAL.Infrastructure.Enums;

namespace MGK.ServiceBase.DAL.Infrastructure.Queries;

public abstract class QueryConstructorBase<TContext, TEntity, TQueryConstructor> : IQueryConstructor<TEntity, TQueryConstructor>
    where TContext : class
    where TEntity : class, IDataUnit
    where TQueryConstructor : class, IQueryConstructor
{
    private readonly IMapper _mapper;

    protected QueryConstructorBase(TContext context, IMapper mapper)
    {
        Ensure.Parameter.IsNotNull(context, nameof(context));
        Ensure.Parameter.IsNotNull(mapper, nameof(mapper));

        Context = context;
        _mapper = mapper;
    }

    #region Properties
    protected TContext Context { get; }

    protected IQueryable<TEntity> QueryEntity { get; private set; }
    #endregion

    #region Interface methods
    public async Task<TEntity> GetFirstRecordAsync(bool withTracking = false)
        => await Query(withTracking).FirstOrDefaultAsync();

    public async Task<TOutput> GetFirstRecordAsync<TOutput>(bool withTracking = false)
        => await Query<TOutput>().FirstOrDefaultAsync();

    public async Task<TEntity> GetRecordAsync(bool withTracking = false)
        => await Query(withTracking).SingleOrDefaultAsync();

    public async Task<TOutput> GetRecordAsync<TOutput>(bool withTracking = false)
        => await Query<TOutput>().SingleOrDefaultAsync();

    public abstract IQueryable<TEntity> Query(bool withTracking = false);

    public IQueryable<TOutput> Query<TOutput>(bool withTracking = false)
        => Query(withTracking).ProjectTo<TOutput>(_mapper.ConfigurationProvider);

    public async Task<TEntity[]> QueryAsArrayAsync(bool withTracking = false)
        => await Query(withTracking).ToArrayAsync();

    public async Task<TOutput[]> QueryAsArrayAsync<TOutput>(bool withTracking = false)
        => await Query<TOutput>().ToArrayAsync();

    public async Task<IEnumerable<TEntity>> QueryAsEnumerableAsync(bool withTracking = false)
        => await QueryAsArrayAsync(withTracking);

    public async Task<IEnumerable<TOutput>> QueryAsEnumerableAsync<TOutput>(bool withTracking = false)
        => await QueryAsArrayAsync<TOutput>(withTracking);

    public async Task<List<TEntity>> QueryAsListAsync(bool withTracking = false)
        => await Query(withTracking).ToListAsync();

    public async Task<List<TOutput>> QueryAsListAsync<TOutput>(bool withTracking = false)
        => await Query<TOutput>().ToListAsync();

    public abstract TQueryConstructor Start();
    #endregion

    #region Own methods
    protected TQueryConstructor FilterBy(Expression<Func<TEntity, bool>> filter)
    {
        SetQueryEntity(QueryEntity.Where(filter));
        return this as TQueryConstructor;
    }

    protected TQueryConstructor Include(Expression<Func<TEntity, object>> property)
    {
        SetQueryEntity(QueryEntity.Include(property));
        return this as TQueryConstructor;
    }

    protected TQueryConstructor IncludeWith(Expression<Func<TEntity, object>> property, Expression<Func<object, object>> withProperty)
        => IncludeWith(property, new Expression<Func<object, object>>[] { withProperty });

    protected abstract TQueryConstructor IncludeWith(
        Expression<Func<TEntity, object>> property,
        IEnumerable<Expression<Func<object, object>>> withProperties,
        bool isMultiLevel = false);

    protected TQueryConstructor OrderBy<TKey>(OrderSelector<TEntity, TKey> keySelector)
    {
        SetQueryEntity(keySelector.SortOrder == SortOrder.Ascending
            ? QueryEntity.OrderBy(keySelector.Key)
            : QueryEntity.OrderByDescending(keySelector.Key));
        return this as TQueryConstructor;
    }

    protected TQueryConstructor OrderByMany<TKey>(IList<OrderSelector<TEntity, TKey>> keySelectors)
    {
        IOrderedQueryable<TEntity> orderedQuery = null;

        for (int i = 0; i < keySelectors.Count; i++)
        {
            if (i == 0)
            {
                orderedQuery = keySelectors[i].SortOrder == SortOrder.Ascending
                    ? QueryEntity.OrderBy(keySelectors[i].Key)
                    : QueryEntity.OrderByDescending(keySelectors[i].Key);
            }
            else
            {
                orderedQuery = keySelectors[i].SortOrder == SortOrder.Ascending
                    ? orderedQuery.ThenBy(keySelectors[i].Key)
                    : orderedQuery.ThenByDescending(keySelectors[i].Key);
            }
        }

        SetQueryEntity(orderedQuery);

        return this as TQueryConstructor;
    }

    protected void SetQueryEntity(IQueryable<TEntity> value) => QueryEntity = value;

    protected TQueryConstructor Take(int count)
    {
        SetQueryEntity(QueryEntity.Take(count));
        return this as TQueryConstructor;
    }
    #endregion
}
