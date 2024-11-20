using Engage.Application.Services.EmployeeRegions.Queries;
using Engage.Application.Services.Employees.Models;

namespace Engage.Application.Services.Employees.Queries;

public class EmployeeHistoryListQuery : IRequest<ListResult<EmployeeHistoryDto>>
{
    public int EmployeeId { get; set; }
}
public class EmployeeHistoryListQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeHistoryListQuery, ListResult<EmployeeHistoryDto>>
{
    private readonly IMediator _mediator;
    public EmployeeHistoryListQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<ListResult<EmployeeHistoryDto>> Handle(EmployeeHistoryListQuery request, CancellationToken cancellationToken)
    {
        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);
        var entity = await _context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == request.EmployeeId, cancellationToken);

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

        var terminationList = await _context.EmployeeTerminationHistories.Where(e => e.EmployeeId == request.EmployeeId)
                                                           .OrderByDescending(e => e.EmployeeTerminationHistoryId)
                                                           .ProjectTo<EmployeeHistoryDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

        var reinstatementList = await _context.EmployeeReinstatementHistories.Where(e => e.EmployeeId == request.EmployeeId)
                                                           .OrderByDescending(e => e.EmployeeReinstatementHistoryId)
                                                           .ProjectTo<EmployeeHistoryDto>(_mapper.ConfigurationProvider)
                                                           .ToListAsync(cancellationToken);

        List<EmployeeHistoryDto> result = new List<EmployeeHistoryDto>();
        if (terminationList.Any())
        {
            result.AddRange(terminationList);
        }

        if (reinstatementList.Any())
        {
            result.AddRange(reinstatementList);
        }

        return new ListResult<EmployeeHistoryDto>(result);
    }
}
