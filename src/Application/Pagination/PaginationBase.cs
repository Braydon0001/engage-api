using Finbuckle.MultiTenant.Abstractions;

namespace Engage.Application.Pagination;

public abstract class PaginationBase<TEntity, TQuery> where TEntity : class
                                                      where TQuery : GetQuery
{
    protected readonly TQuery _query;
    private readonly bool _useFilterPredicates2;
    protected IQueryable<TEntity> _queryable;
    protected IQueryable<TEntity> _countQueryable;
    protected List<Expression<Func<TEntity, bool>>> _predicates;
    protected Dictionary<string, FilterModel> _filters;

    protected PaginationBase(TQuery query, bool useFilterPredicates2 = false)
    {
        _query = query;
        _useFilterPredicates2 = useFilterPredicates2;
        _predicates = new List<Expression<Func<TEntity, bool>>>();
        _filters = new Dictionary<string, FilterModel>();
    }

    public (IQueryable<TEntity> queryable, PaginationResult result) Paginate(IQueryable<TEntity> queryable, IMultiTenantContextAccessor multiTenantContextAccessor)
    {
        // Setup
        var (search, filters, orderBy, orderDirection, pageNo, pageSize, skip) = _query;
        _queryable = queryable.AsNoTracking().IgnoreQueryFilters().Where(e => EF.Property<string>(e, "TenantId") == multiTenantContextAccessor.MultiTenantContext.TenantInfo.Id);
        _countQueryable = queryable.AsNoTracking().IgnoreQueryFilters().Where(e => EF.Property<string>(e, "TenantId") == multiTenantContextAccessor.MultiTenantContext.TenantInfo.Id);

        // Adhoc Filter
        Filter();

        // Filter
        if (!string.IsNullOrWhiteSpace(filters))
        {
            if (_useFilterPredicates2)
            {
                var filtersDictionary = TransformPipeDelimetedFilters(filters);
                if (filtersDictionary.Count > 0)
                {
                    AddFilterPredicates2(filtersDictionary);
                }
            }
            else
            {
                _filters = JsonConvert.DeserializeObject<Dictionary<string, FilterModel>>(filters);
                if (_filters.Count > 0)
                {
                    AddFilterPredicates();
                }
            }
        }
        if (_predicates.Count > 0)
        {
            _queryable = _predicates.Aggregate(_queryable, (current, predicate) => current.Where(predicate));
            _countQueryable = _predicates.Aggregate(_countQueryable, (current, predicate) => current.Where(predicate));
        }

        // Order
        Order(orderBy, orderDirection);

        // Page
        _queryable = _queryable.Skip(skip).Take(pageSize);
        var rowCount = GetRowCount();

        return (_queryable, PaginationResult.Create(pageNo, pageSize, rowCount));
    }

    protected abstract void Filter();
    protected abstract void AddFilterPredicates2(Dictionary<string, string> filters);
    protected abstract void AddFilterPredicates();
    protected abstract void Order(string orderBy, string orderDirection);
    protected abstract int GetRowCount();
}
