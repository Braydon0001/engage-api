using Engage.Application.Services.IncidentSkus.Models;

namespace Engage.Application.Services.IncidentSkus.Queries;

public class IncidentSkusQuery : IRequest<ListResult<IncidentSkuDto>>
{
    public int IncidentId { get; set; }
}

public class IncidentSkusQueryHandler : BaseQueryHandler, IRequestHandler<IncidentSkusQuery, ListResult<IncidentSkuDto>>
{
    public IncidentSkusQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<IncidentSkuDto>> Handle(IncidentSkusQuery request, CancellationToken cancellationToken)
    {

        var entities = await _context.IncidentSkus.Where(e => e.IncidentId == request.IncidentId)
                                                  .OrderBy(e => e.IncidentSkuId)
                                                  .ProjectTo<IncidentSkuDto>(_mapper.ConfigurationProvider)
                                                  .ToListAsync(cancellationToken);

        return new ListResult<IncidentSkuDto>(entities);
    }
}
