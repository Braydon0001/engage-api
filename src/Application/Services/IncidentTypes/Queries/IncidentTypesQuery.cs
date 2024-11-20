using Engage.Application.Services.IncidentTypes.Models;

namespace Engage.Application.Services.IncidentTypes.Queries;

public class IncidentTypesQuery : IRequest<ListResult<IncidentTypeDto>>
{
}

public class IncidentTypesQueryHandler : BaseQueryHandler, IRequestHandler<IncidentTypesQuery, ListResult<IncidentTypeDto>>
{
    public IncidentTypesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<IncidentTypeDto>> Handle(IncidentTypesQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.IncidentTypes.OrderBy(e => e.Name)
                                                   .ProjectTo<IncidentTypeDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);

        return new ListResult<IncidentTypeDto>(entities);
    }
}
