using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.Employees.Queries;

public class EmployeeQuery : IRequest<EmployeeDto>
{
    public int Id { get; set; }
}

public class EmployeeQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeQuery, EmployeeDto>
{
    private readonly IMediator _mediator;
    public EmployeeQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<EmployeeDto> Handle(EmployeeQuery request, CancellationToken cancellationToken)
    {
        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

        var entity = await _context.Employees.Include(x => x.EmployeeDepartments)
                                             .ThenInclude(x => x.EngageDepartment)
                                             .Include(x => x.EmployeeRegions)
                                             .ThenInclude(x => x.EngageRegion)
                                             .SingleAsync(x => x.EmployeeId == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new Exception("Employee not found");
        }

        if (!engageRegionIds.Contains(7))
        {
            var isInRegion = await _context.EmployeeRegions.Where(e => e.EmployeeId == entity.EmployeeId && engageRegionIds.Contains(e.EngageRegionId))
                                                           .AnyAsync(cancellationToken);

            if (!isInRegion)
            {
                throw new Exception("This Employee is not in your Region");
            }
        }

        return _mapper.Map<Employee, EmployeeDto>(entity);
    }
}
