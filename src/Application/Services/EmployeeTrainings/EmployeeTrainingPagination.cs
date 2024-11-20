using Engage.Application.Services.EmployeeTrainings.Queries;
using Finbuckle.MultiTenant.Abstractions;

namespace Engage.Application.Services.EmployeeTrainings;

public class EmployeeTrainingPagination : PaginationBase<EmployeeTraining, EmployeeTrainingsQuery>
{
    private const string TRAINING_NAME = "TRAININGNAME";
    private const string EMPLOYEE_NAME = "EMPLOYEENAME";

    public EmployeeTrainingPagination(EmployeeTrainingsQuery query) : base(query)
    {
    }

    protected override void Filter()
    {
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
                case TRAINING_NAME: _predicates.Add(CreateFilter<EmployeeTraining>(filter, "Training.Name")); break;
                case EMPLOYEE_NAME: _predicates.Add(CreateFilter<EmployeeTraining>(filter, "Employee.FirstName")); break;
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

            if (colId == TRAINING_NAME) { _queryable = isAsc ? _queryable.OrderBy(e => e.Training.Name) : _queryable.OrderByDescending(e => e.Training.Name); }
            if (colId == EMPLOYEE_NAME) { _queryable = isAsc ? _queryable.OrderBy(e => e.Employee.FirstName) : _queryable.OrderByDescending(e => e.Employee.FirstName); }
        }
        else
        {
            _queryable = _queryable.OrderByDescending(e => e.TrainingId);
        }
    }

    protected override int GetRowCount()
    {
        return _countQueryable.Count();
    }
}

public static class EmployeeTrainingPaginationExtensions
{
    public static (IQueryable<EmployeeTraining> queryable, PaginationResult result) Paginate(this IQueryable<EmployeeTraining> queryable, EmployeeTrainingsQuery query, IMultiTenantContextAccessor multiTenantContextAccessor)
    {
        var pagination = new EmployeeTrainingPagination(query);
        return pagination.Paginate(queryable, multiTenantContextAccessor);
    }
}
