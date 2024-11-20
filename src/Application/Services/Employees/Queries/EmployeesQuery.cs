using Engage.Application.Services.EmployeeRegions.Queries;
using Engage.Application.Services.Employees.Models;

namespace Engage.Application.Services.Employees.Queries;

public class EmployeesQuery : GetQuery, IRequest<ListResult<EmployeeListDto>>
{
    public string EmailAddresses { get; set; }
}

public class EmployeesQueryHandler : BaseQueryHandler, IRequestHandler<EmployeesQuery, ListResult<EmployeeListDto>>
{
    private readonly IMediator _mediator;
    public EmployeesQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<ListResult<EmployeeListDto>> Handle(EmployeesQuery request, CancellationToken cancellationToken)
    {
        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

        var queryable = _context.Employees.Where(e => string.IsNullOrWhiteSpace(request.EmailAddresses) || request.EmailAddresses.ToLower().Contains(e.EmailAddress1.ToLower()) &&
                                                           !string.IsNullOrWhiteSpace(e.EmailAddress1));

        if (!engageRegionIds.Contains(7))
        {
            queryable = queryable
                                .Join(_context.EmployeeRegions.Where(c => engageRegionIds.Contains(c.EngageRegionId)),
                                      employee => employee.EmployeeId,
                                      region => region.EmployeeId,
                                      (employee, region) => employee).Distinct();
        }

        var entities = await queryable.OrderBy(e => e.FirstName)
                                      .ThenBy(e => e.LastName)
                                      .ProjectTo<EmployeeListDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<EmployeeListDto>(entities);
    }
}
