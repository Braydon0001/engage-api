namespace Engage.Application.Services.Manufacturers.Queries;

public class ManufacurersByVariantProductOptionQuery : IRequest<List<OptionDto>>
{
    public int Id { get; set; }
}
public record ManufacurersByVariantProductOptionHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ManufacurersByVariantProductOptionQuery, List<OptionDto>>
{
    public async Task<List<OptionDto>> Handle(ManufacurersByVariantProductOptionQuery query, CancellationToken cancellationToken)
    {
        var test = await Context.EngageVariantProducts
                                           .AsNoTracking()
                                           .Where(e => e.EngageVariantProductId == query.Id)
                                           .Include(e => e.EngageMasterProduct)
                                           //.Select(e => e.EngageMasterProduct.SupplierId)
                                           .FirstOrDefaultAsync(cancellationToken);

        var supplierId = test.EngageMasterProduct.SupplierId;
        if (supplierId == 0)
        {
            throw new Exception("No Variant Product found");
        }

        var options = await Context.Manufacturers
                                   .AsNoTracking()
                                   .Where(e => e.SupplierId == supplierId)
                                   .Select(e => new OptionDto(e.ManufacturerId, e.Name))
                                   .ToListAsync(cancellationToken);

        return options;
    }
}