using Engage.Application.Services.Claims.Models;
using Engage.Application.Services.EmployeeRegions.Queries;

namespace Engage.Application.Services.Claims.Queries;

public class ClaimStoreOptionsQuery : GetQuery, IRequest<List<ClaimStoreOptionDto>>
{
}

public class ClaimStoreOptionsQueryHandler : BaseQueryHandler, IRequestHandler<ClaimStoreOptionsQuery, List<ClaimStoreOptionDto>>
{
    private readonly IMediator _mediator;

    public ClaimStoreOptionsQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<List<ClaimStoreOptionDto>> Handle(ClaimStoreOptionsQuery request, CancellationToken cancellationToken)
    {
        var engageRegionIds = await _mediator.Send(new UserRegionsQuery(), cancellationToken);

        var queryable = _context.Stores.Where(e => engageRegionIds.Contains(e.EngageRegionId));

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(e => EF.Functions.Like(e.Name, $"%{request.Search}%"));
        }

        return await queryable.Include(e => e.EngageRegion)
                              .Where(e => e.Disabled == false)
                              .ProjectTo<ClaimStoreOptionDto>(_mapper.ConfigurationProvider)
                              .Take(100)
                              .OrderBy(e => e.Name)
                              .ToListAsync(cancellationToken);
    }
}
