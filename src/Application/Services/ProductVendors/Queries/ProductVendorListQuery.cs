// auto-generated
namespace Engage.Application.Services.ProductVendors.Queries;

public class ProductVendorListQuery : IRequest<List<ProductVendorDto>>
{

}

public class ProductVendorListHandler : ListQueryHandler, IRequestHandler<ProductVendorListQuery, List<ProductVendorDto>>
{
    public ProductVendorListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductVendorDto>> Handle(ProductVendorListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductVendors.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ThenBy(e => e.Code)
                              .ProjectTo<ProductVendorDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}