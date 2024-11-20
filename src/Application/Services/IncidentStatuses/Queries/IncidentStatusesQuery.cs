using Engage.Application.Services.IncidentStatuses.Models;

namespace Engage.Application.Services.IncidentStatuses.Queries;

public class IncidentStatusesQuery : IRequest<ListResult<IncidentStatusDto>>
{

}

public class IncidentStatusesQueryHandler : BaseQueryHandler, IRequestHandler<IncidentStatusesQuery, ListResult<IncidentStatusDto>>
{
    public IncidentStatusesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<IncidentStatusDto>> Handle(IncidentStatusesQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.IncidentStatuses.OrderBy(e => e.Name)
                                                      .ProjectTo<IncidentStatusDto>(_mapper.ConfigurationProvider)
                                                      .ToListAsync(cancellationToken);

        return new ListResult<IncidentStatusDto>(entities);
    }
}
