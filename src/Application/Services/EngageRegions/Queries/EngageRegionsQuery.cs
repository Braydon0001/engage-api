using Engage.Application.Services.EngageRegions.Models;

namespace Engage.Application.Services.EngageRegions.Queries;

public class EngageRegionsQuery : GetQuery, IRequest<ListResult<EngageRegionDto>>
{
}

public class EngageRegionsQueryHandler : BaseQueryHandler, IRequestHandler<EngageRegionsQuery, ListResult<EngageRegionDto>>
{
    public EngageRegionsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<EngageRegionDto>> Handle(EngageRegionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EngageRegions.Where(e => e.Deleted == false)
                                                   .OrderBy(e => e.Id)
                                                   .ProjectTo<EngageRegionDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);

        return new ListResult<EngageRegionDto>(entities);
    }
} 
