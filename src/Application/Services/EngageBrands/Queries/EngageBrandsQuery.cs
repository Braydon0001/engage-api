using Engage.Application.Services.EngageBrands.Queries;

namespace Engage.Application.Services.EngageCategories.Queries;

public class EngageBrandsQuery : GetQuery, IRequest<ListResult<EngageBrandDto>>
{
}

public class EngageBrandsQueryHandler : BaseQueryHandler, IRequestHandler<EngageBrandsQuery, ListResult<EngageBrandDto>>
{
    public EngageBrandsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<EngageBrandDto>> Handle(EngageBrandsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.EngageBrands.OrderBy(e => e.Name)
                                                      .ProjectTo<EngageBrandDto>(_mapper.ConfigurationProvider)
                                                      .ToListAsync(cancellationToken);

        return new ListResult<EngageBrandDto>(entities);
    }
}
