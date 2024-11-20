// auto-generated
namespace Engage.Application.Services.ProductVendors.Queries;

public class ProductVendorOptionListQuery : IRequest<List<ProductVendorOption>>
{ 

}

public class ProductVendorOptionListHandler : ListQueryHandler, IRequestHandler<ProductVendorOptionListQuery, List<ProductVendorOption>>
{
    public ProductVendorOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductVendorOption>> Handle(ProductVendorOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductVendors.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ThenBy(e => e.Code)
                              .ProjectTo<ProductVendorOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}