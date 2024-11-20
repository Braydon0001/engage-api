using Engage.Application.Services.EngageMasterProducts.Models;

namespace Engage.Application.Services.EngageMasterProducts.Queries;

public class EngageMasterProductOptionsQuery : GetQuery, IRequest<List<EngageMasterProductDto>>
{
}

public record EngageMasterProductOptionsQueryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageMasterProductOptionsQuery, List<EngageMasterProductDto>>
{

    public async Task<List<EngageMasterProductDto>> Handle(EngageMasterProductOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = Context.EngageMasterProducts.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            queryable = queryable.Where(o => EF.Functions.Like(o.Code, $"%{request.Search}%") ||
                                             EF.Functions.Like(o.Name, $"%{request.Search}%"));
        }

        return await queryable.OrderBy(e => e.Name)
                            .ProjectTo<EngageMasterProductDto>(Mapper.ConfigurationProvider)
                            .Take(100)
                            .ToListAsync(cancellationToken);

    }
}
