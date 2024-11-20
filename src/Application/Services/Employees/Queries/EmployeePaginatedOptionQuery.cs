using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.Employees.Queries;

public class EmployeePaginatedOptionQuery : PaginatedQuery, IRequest<List<EmployeeOption>>
{
}

public class EmployeePaginatedOptionHandler : ListQueryHandler, IRequestHandler<EmployeePaginatedOptionQuery, List<EmployeeOption>>
{
    private readonly IMediator _mediator;
    public EmployeePaginatedOptionHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<List<EmployeeOption>> Handle(EmployeePaginatedOptionQuery query, CancellationToken cancellationToken)
    {
        var props = EmployeePaginationProps.Create();

        var queryable = _context.Employees.Where(e => e.EmployeeTypeId == (int)EmployeeTypeId.Employee)
                                          .AsQueryable()
                                          .AsNoTracking();

        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

        if (!engageRegionIds.Contains(7))
        {
            queryable = queryable
                                .Join(_context.EmployeeRegions.Where(c => engageRegionIds.Contains(c.EngageRegionId)),
                                      employee => employee.EmployeeId,
                                      region => region.EmployeeId,
                                      (employee, region) => employee).Distinct();
        }

        if (query.SortModel.IsNullOrEmpty())
        {
            queryable = queryable.OrderBy(e => e.FirstName)
                                 .ThenBy(e => e.LastName);
        }

        var entities = await queryable.Filter(query, props)
                                      .Sort(query, props)
                                      .SkipQuery(query)
                                      .TakeQuery(query)
                                      .ProjectTo<EmployeeOption>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return entities;
    }
}