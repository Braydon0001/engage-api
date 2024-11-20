using Engage.Application.Services.Incidents.Models;

namespace Engage.Application.Services.Incidents.Queries;

public class IncidentsQuery : IRequest<ListResult<IncidentDto>>
{

}

public class IncidentsQueryHandler : BaseQueryHandler, IRequestHandler<IncidentsQuery, ListResult<IncidentDto>>
{
    public IncidentsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<IncidentDto>> Handle(IncidentsQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.Incidents.OrderByDescending(e => e.IncidentId)
                                               .ProjectTo<IncidentDto>(_mapper.ConfigurationProvider)
                                               .ToListAsync(cancellationToken);

        return new ListResult<IncidentDto>(entities);
    }
}
