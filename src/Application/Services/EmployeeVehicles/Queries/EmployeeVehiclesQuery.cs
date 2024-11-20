using Engage.Application.Services.EmployeeRegions.Queries;
using Engage.Application.Services.EmployeeVehicles.Models;

namespace Engage.Application.Services.EmployeeVehicles.Queries;

public class EmployeeVehiclesQuery : GetQuery, IRequest<ListResult<EmployeeVehicleDto>>
{
    public int? EmployeeId { get; set; }
}

public class EmployeeVehiclesQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeVehiclesQuery, ListResult<EmployeeVehicleDto>>
{
    private readonly IMediator _mediator;
    public EmployeeVehiclesQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<ListResult<EmployeeVehicleDto>> Handle(EmployeeVehiclesQuery request, CancellationToken cancellationToken)
    {
        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

        var queryable = _context.EmployeeVehicles.AsQueryable();

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
                                      .ThenBy(e => e.RegistrationNumber)
                                      .ProjectTo<EmployeeVehicleDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<EmployeeVehicleDto>(entities);
    }
}
