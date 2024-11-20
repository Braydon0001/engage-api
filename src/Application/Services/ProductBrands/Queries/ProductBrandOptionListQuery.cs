// auto-generated
namespace Engage.Application.Services.ProductBrands.Queries;

public class ProductBrandOptionListQuery : IRequest<List<ProductBrandOption>>
{ 

}

public class ProductBrandOptionListHandler : ListQueryHandler, IRequestHandler<ProductBrandOptionListQuery, List<ProductBrandOption>>
{
    public ProductBrandOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductBrandOption>> Handle(ProductBrandOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductBrands.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ThenBy(e => e.SparBrand)
                              .ProjectTo<ProductBrandOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}