using Engage.Application.Services.EmployeeAssets.Models;
using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.EmployeeAssets.Queries;

public class EmployeeAssetsQuery : GetQuery, IRequest<ListResult<EmployeeAssetDto>>
{
    public int? EmployeeId { get; set; }
}

public class EmployeeAssetsQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeAssetsQuery, ListResult<EmployeeAssetDto>>
{
    private readonly IMediator _mediator;
    public EmployeeAssetsQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<ListResult<EmployeeAssetDto>> Handle(EmployeeAssetsQuery request, CancellationToken cancellationToken)
    {
        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

        var queryable = _context.EmployeeAssets.AsQueryable();

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
                                      .ThenBy(e => e.MobileNumber)
                                      .ProjectTo<EmployeeAssetDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

        return new ListResult<EmployeeAssetDto>(entities);
    }
}
