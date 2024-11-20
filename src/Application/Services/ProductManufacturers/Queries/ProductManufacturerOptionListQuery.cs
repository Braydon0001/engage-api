// auto-generated
namespace Engage.Application.Services.ProductManufacturers.Queries;

public class ProductManufacturerOptionListQuery : IRequest<List<ProductManufacturerOption>>
{ 

}

public class ProductManufacturerOptionListHandler : ListQueryHandler, IRequestHandler<ProductManufacturerOptionListQuery, List<ProductManufacturerOption>>
{
    public ProductManufacturerOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductManufacturerOption>> Handle(ProductManufacturerOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductManufacturers.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductManufacturerId)
                              .ProjectTo<ProductManufacturerOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}