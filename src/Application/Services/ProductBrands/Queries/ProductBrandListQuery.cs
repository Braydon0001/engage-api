// auto-generated
namespace Engage.Application.Services.ProductBrands.Queries;

public class ProductBrandListQuery : IRequest<List<ProductBrandDto>>
{

}

public class ProductBrandListHandler : ListQueryHandler, IRequestHandler<ProductBrandListQuery, List<ProductBrandDto>>
{
    public ProductBrandListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductBrandDto>> Handle(ProductBrandListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductBrands.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ThenBy(e => e.SparBrand)
                              .ProjectTo<ProductBrandDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}