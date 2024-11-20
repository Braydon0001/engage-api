using Engage.Application.Services.EngageVariantProducts.Models;

namespace Engage.Application.Services.EngageVariantProducts.Queries;

public class EngageVariantProductByMasterVariantQuery : IRequest<List<EngageVariantProductByMasterVariantDto>>
{
    public int Id { get; set; }
}
public record EngageVariantProductByMasterVariantHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageVariantProductByMasterVariantQuery, List<EngageVariantProductByMasterVariantDto>>
{
    public async Task<List<EngageVariantProductByMasterVariantDto>> Handle(EngageVariantProductByMasterVariantQuery query, CancellationToken cancellationToken)
    {
        var variants = await Context.EngageVariantProducts
                                            .AsNoTracking()
                                            .Where(e => e.EngageMasterProductId == query.Id)
                                            .ProjectTo<EngageVariantProductByMasterVariantDto>(Mapper.ConfigurationProvider)
                                            .ToListAsync(cancellationToken);
        return variants;
    }
}
