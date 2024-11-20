using Engage.Application.Services.EmployeeCoolerBoxes.Models;
using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.EmployeeCoolerBoxes.Queries;

public class EmployeeCoolerBoxesQuery : GetQuery, IRequest<ListResult<EmployeeCoolerBoxDto>>
{
    public int? EmployeeId { get; set; }
}

public class EmployeeCoolerBoxesQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeCoolerBoxesQuery, ListResult<EmployeeCoolerBoxDto>>
{
    private readonly IMediator _mediator;
    public EmployeeCoolerBoxesQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<ListResult<EmployeeCoolerBoxDto>> Handle(EmployeeCoolerBoxesQuery request, CancellationToken cancellationToken)
    {
        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

        var queryable = _context.EmployeeCoolerBoxes.AsQueryable();

        if (!engageRegionIds.Contains(7))
        {
            queryable = queryable
                                .Join(_context.EmployeeRegions.Where(c => engageRegionIds.Contains(c.EngageRegionId)),
                                      employee => employee.EmployeeId,
                                      region => region.EmployeeId,
                                      (employee, region) => employee).Distinct();
        }

        if (request.EmployeeId.HasValue)
        {
            queryable = queryable.Where(e => e.EmployeeId == request.EmployeeId);
        }

        var entities = await queryable.OrderBy(e => e.Employee.Code)
                                      .ThenBy(e => e.Name)
                                      .ProjectTo<EmployeeCoolerBoxDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<EmployeeCoolerBoxDto>(entities);
    }
}
