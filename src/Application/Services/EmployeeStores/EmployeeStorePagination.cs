using Engage.Application.Services.EmployeeStores.Queries;
using Finbuckle.MultiTenant.Abstractions;

namespace Engage.Application.Services.EmployeeStores;

public class EmployeeStorePagination : PaginationBase<EmployeeStore, PaginatedEmployeeStoresQuery>
{
    // Int
    private const string ID = "ID";
    private const string FREQUENCY = "FREQUENCY";
    // Int SetFilter 
    private const string FREQUENCY_TYPE_NAME = "FREQUENCYTYPENAME";
    // Text
    private const string EMPLOYEE_NAME = "EMPLOYEENAME";
    private const string STORE_NAME = "STORENAME";
    private const string ENGAGE_SUBGROUP_NAME = "ENGAGESUBGROUPNAME";


    public EmployeeStorePagination(PaginatedEmployeeStoresQuery query) : base(query)
    {
    }

    protected override void Filter()
    {
        if (_query.EmployeeId.HasValue)
        {
            _predicates.Add(e => e.EmployeeId == _query.EmployeeId.Value);
        }

        if (_query.StoreId.HasValue)
        {
            _predicates.Add(e => e.StoreId == _query.StoreId.Value);
        }
    }

    protected override void AddFilterPredicates2(Dictionary<string, string> filters)
    {
        throw new NotImplementedException();
    }

    protected override void AddFilterPredicates()
    {
        foreach (var filter in _filters)
        {
            switch (filter.Key)
            {
                case ID: _predicates.Add(CreateFilter<EmployeeStore>(filter, nameof(EmployeeStore.EmployeeStoreId))); break;
                case FREQUENCY: _predicates.Add(CreateFilter<EmployeeStore>(filter, nameof(EmployeeStore.Frequency))); break;
                case FREQUENCY_TYPE_NAME: _predicates.Add(CreateFilter<EmployeeStore>(filter, nameof(EmployeeStore.FrequencyTypeId))); break;
                case EMPLOYEE_NAME: _predicates.Add(CreateFilter<EmployeeStore>(filter, "Employee.Name")); break;
                case STORE_NAME: _predicates.Add(CreateFilter<EmployeeStore>(filter, "Store.Name")); break;
                case ENGAGE_SUBGROUP_NAME: _predicates.Add(CreateFilter<EmployeeStore>(filter, "EngageSubGroup.Name")); break;
            }
        }
    }

    protected override void Order(string orderBy, string orderDirection)
    {
        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            var sortModel = JsonConvert.DeserializeObject<List<SortModel>>(orderBy);

            var isAsc = sortModel[0].Sort.ToUpper().Equals("ASC");
            var colId = sortModel[0].ColId.ToUpper();

            switch (colId)
            {
                case ID: { _queryable = isAsc ? _queryable.OrderBy(e => e.EmployeeStoreId) : _queryable.OrderByDescending(e => e.EmployeeStoreId); break; }
                case FREQUENCY: { _queryable = isAsc ? _queryable.OrderBy(e => e.Frequency) : _queryable.OrderByDescending(e => e.Frequency); break; }
                case FREQUENCY_TYPE_NAME: { _queryable = isAsc ? _queryable.OrderBy(e => e.GetFrequencyType.Name) : _queryable.OrderByDescending(e => e.GetFrequencyType.Name); break; }
                case EMPLOYEE_NAME: { _queryable = isAsc ? _queryable.OrderBy(e => e.Employee.FirstName) : _queryable.OrderByDescending(e => e.Employee.FirstName); break; }
                case STORE_NAME: { _queryable = isAsc ? _queryable.OrderBy(e => e.Store.Name) : _queryable.OrderByDescending(e => e.Store.Name); break; }
                case ENGAGE_SUBGROUP_NAME: { _queryable = isAsc ? _queryable.OrderBy(e => e.EngageSubGroup.Name) : _queryable.OrderByDescending(e => e.EngageSubGroup.Name); break; }
            }
        }
        else
        {
            _queryable = _queryable.OrderBy(e => e.EngageSubGroup.Name);
        }
    }

    protected override int GetRowCount()
    {
        return _countQueryable.Count();
    }
}

public static class EmployeeStorePaginationExtensions
{
    public static (IQueryable<EmployeeStore> queryable, PaginationResult result) Paginate(this IQueryable<EmployeeStore> queryable, PaginatedEmployeeStoresQuery query, IMultiTenantContextAccessor multiTenantContextAccessor)
    {
        var pagination = new EmployeeStorePagination(query);
        return pagination.Paginate(queryable, multiTenantContextAccessor);
    }
}
