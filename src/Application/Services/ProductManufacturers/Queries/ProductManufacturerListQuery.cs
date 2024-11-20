// auto-generated
namespace Engage.Application.Services.ProductManufacturers.Queries;

public class ProductManufacturerListQuery : IRequest<List<ProductManufacturerDto>>
{

}

public class ProductManufacturerListHandler : ListQueryHandler, IRequestHandler<ProductManufacturerListQuery, List<ProductManufacturerDto>>
{
    public ProductManufacturerListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductManufacturerDto>> Handle(ProductManufacturerListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductManufacturers.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductManufacturerId)
                              .ProjectTo<ProductManufacturerDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}