using Engage.Application.Services.IncidentSkuStatuses.Models;

namespace Engage.Application.Services.IncidentSkuStatuses.Queries;

public class IncidentSkuStatusesQuery : IRequest<ListResult<IncidentSkuStatusDto>>
{

}

public class IncidentSkuStatusesQueryHandler : BaseQueryHandler, IRequestHandler<IncidentSkuStatusesQuery, ListResult<IncidentSkuStatusDto>>
{
    public IncidentSkuStatusesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<IncidentSkuStatusDto>> Handle(IncidentSkuStatusesQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.IncidentSkuStatuses.OrderBy(e => e.Name)
                                                         .ProjectTo<IncidentSkuStatusDto>(_mapper.ConfigurationProvider)
                                                         .ToListAsync(cancellationToken);

        return new ListResult<IncidentSkuStatusDto>(entities);
    }
}
