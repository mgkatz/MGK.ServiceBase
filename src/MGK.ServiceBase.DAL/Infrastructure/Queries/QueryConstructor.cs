using AutoMapper;
using AutoMapper.QueryableExtensions;
using MGK.Acceptance;
using MGK.ServiceBase.DAL.Infrastructure.Enums;
using MGK.ServiceBase.DAL.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MGK.ServiceBase.DAL.Infrastructure.Queries
{
    public abstract class QueryConstructor<TContext, TEntity, TQueryConstructor> : IQueryConstructor<TEntity, TQueryConstructor>
        where TContext : DbContext
		where TEntity : class, IDataUnit
		where TQueryConstructor : class, IQueryConstructor
	{
        private readonly IMapper _mapper;

        protected QueryConstructor(TContext context, IMapper mapper)
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

        public IQueryable<TEntity> Query(bool withTracking = false)
            => withTracking ? QueryEntity.AsTracking() : QueryEntity.AsNoTracking();

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

        public TQueryConstructor Start()
        {
            QueryEntity = Context.Set<TEntity>().AsQueryable();
            return this as TQueryConstructor;
        }
        #endregion

        #region Own methods
        protected TQueryConstructor FilterBy(Expression<Func<TEntity, bool>> filter)
        {
            QueryEntity = QueryEntity.Where(filter);
            return this as TQueryConstructor;
        }

        protected TQueryConstructor Include(Expression<Func<TEntity, object>> property)
        {
            QueryEntity = QueryEntity.Include(property);
            return this as TQueryConstructor;
        }

        protected TQueryConstructor IncludeWith(Expression<Func<TEntity, object>> property, Expression<Func<object, object>> withProperty)
            => IncludeWith(property, new Expression<Func<object, object>>[] { withProperty });

        protected TQueryConstructor IncludeWith(
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

                QueryEntity = queryable;
            }
            else
            {
                // A non multi-level IncludeWith allows to perform an Include with one ThenInclude.
                foreach (Expression<Func<object, object>> withProperty in withProperties)
                    QueryEntity = QueryEntity.Include(property).ThenInclude(withProperty);
            }

            return this as TQueryConstructor;
        }

        protected TQueryConstructor OrderBy<TKey>(OrderSelector<TEntity, TKey> keySelector)
        {
            QueryEntity = keySelector.SortOrder == SortOrder.Ascending
                ? QueryEntity.OrderBy(keySelector.Key)
                : QueryEntity.OrderByDescending(keySelector.Key);
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

            QueryEntity = orderedQuery;

            return this as TQueryConstructor;
        }

        protected TQueryConstructor Take(int count)
        {
            QueryEntity = QueryEntity.Take(count);
            return this as TQueryConstructor;
        }
        #endregion
    }
}
