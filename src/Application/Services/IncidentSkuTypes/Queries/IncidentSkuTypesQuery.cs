using Engage.Application.Services.IncidentSkuTypes.Models;

namespace Engage.Application.Services.IncidentSkuTypes.Queries;

public class IncidentSkuTypesQuery : IRequest<ListResult<IncidentSkuTypeDto>>
{

}

public class IncidentSkuTypesQueryHandler : BaseQueryHandler, IRequestHandler<IncidentSkuTypesQuery, ListResult<IncidentSkuTypeDto>>
{
    public IncidentSkuTypesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<IncidentSkuTypeDto>> Handle(IncidentSkuTypesQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.IncidentSkuTypes.OrderBy(e => e.Name)
                                                      .ProjectTo<IncidentSkuTypeDto>(_mapper.ConfigurationProvider)
                                                      .ToListAsync(cancellationToken);

        return new ListResult<IncidentSkuTypeDto>(entities);
    }
}
