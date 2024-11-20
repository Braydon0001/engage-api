// auto-generated
using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.EmployeeTransactions.Queries;

public class EmployeeTransactionPaginatedListQuery : PaginatedQuery, IRequest<List<EmployeeTransactionDto>>
{

}

public class EmployeeTransactionPaginatedListHandler : ListQueryHandler, IRequestHandler<EmployeeTransactionPaginatedListQuery, List<EmployeeTransactionDto>>
{
    private readonly IMediator _mediator;
    public EmployeeTransactionPaginatedListHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<List<EmployeeTransactionDto>> Handle(EmployeeTransactionPaginatedListQuery query, CancellationToken cancellationToken)
    {
        var paginationModels = CreatePaginationModels();

        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

        var queryable = _context.EmployeeTransactions.AsQueryable().AsNoTracking();

        if (!engageRegionIds.Contains(7))
        {
            queryable = queryable
                                .Join(_context.EmployeeRegions.Where(c => engageRegionIds.Contains(c.EngageRegionId)),
                                      employee => employee.EmployeeId,
                                      region => region.EmployeeId,
                                      (employee, region) => employee).Distinct();
        }

        var entities = await queryable.Filter(query, paginationModels)
                                      .Sort(query, paginationModels)
                                      .Skip(query.StartRow)
                                      .Take(query.PageSize)
                                      .ProjectTo<EmployeeTransactionDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return entities;
    }

    private static Dictionary<string, PaginationProperty> CreatePaginationModels()
    {
        return new Dictionary<string, PaginationProperty> {

            { "id", new PaginationProperty("EmployeeTransactionId") },
            { "employeeName", new PaginationProperty("Employee.FirstName") },
            { "employeeTransactionTypeName", new PaginationProperty("EmployeeTransactionType.Name") },
            { "employeeTransactionStatusName", new PaginationProperty("EmployeeTransactionStatus.Name") },
            { "payrollPeriodName", new PaginationProperty("PayrollPeriod.Name") }

        };
    }
}


