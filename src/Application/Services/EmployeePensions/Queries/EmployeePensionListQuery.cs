// auto-generated
using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.EmployeePensions.Queries;

public class EmployeePensionListQuery : IRequest<List<EmployeePensionDto>>
{

}

public class EmployeePensionListHandler : ListQueryHandler, IRequestHandler<EmployeePensionListQuery, List<EmployeePensionDto>>
{
    private readonly IMediator _mediator;
    public EmployeePensionListHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<List<EmployeePensionDto>> Handle(EmployeePensionListQuery query, CancellationToken cancellationToken)
    {
        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

        var queryable = _context.EmployeePensions.AsQueryable().AsNoTracking();

        if (!engageRegionIds.Contains(7))
        {
            queryable = queryable
                                .Join(_context.EmployeeRegions.Where(c => engageRegionIds.Contains(c.EngageRegionId)),
                                      employee => employee.EmployeeId,
                                      region => region.EmployeeId,
                                      (employee, region) => employee).Distinct();
        }

        return await queryable.OrderBy(e => e.EmployeePensionId)
                              .ProjectTo<EmployeePensionDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}