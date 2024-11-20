using Engage.Application.Services.EngageMasterProducts.Models;

namespace Engage.Application.Services.EngageMasterProducts.Queries;

public class EngageMasterVariantVmQuery : IRequest<EngageMasterVariantVm>
{
    public int Id { get; set; }
}

public record EngageMasterVariantVmHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageMasterVariantVmQuery, EngageMasterVariantVm>
{
    public async Task<EngageMasterVariantVm> Handle(EngageMasterVariantVmQuery query, CancellationToken cancellationToken)
    {
        var entity = await Context.EngageMasterProducts
                                                    .AsNoTracking()
                                                    .Where(e => e.EngageMasterProductId == query.Id)
                                                    .ProjectTo<EngageMasterVariantVm>(Mapper.ConfigurationProvider)
                                                    .FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("No master product found");

        var masterVariant = await Context.EngageVariantProducts.AsNoTracking()
                                                               .Where(e => e.EngageMasterProductId == query.Id
                                                                    && e.IsMaster == true)
                                                               .Include(e => e.UnitType)
                                                               .FirstOrDefaultAsync(cancellationToken)
                                                               ?? throw new Exception("No master variant found");

        entity.EngageMasterVariantId = masterVariant.EngageVariantProductId;
        entity.UnitTypeId = new OptionDto(masterVariant.UnitTypeId, masterVariant.UnitType.Name);
        entity.EANNumber = masterVariant.EANNumber;
        entity.Size = masterVariant.Size;
        entity.PackSize = masterVariant.PackSize;
        entity.Files = masterVariant.Files;

        return entity;
    }
}