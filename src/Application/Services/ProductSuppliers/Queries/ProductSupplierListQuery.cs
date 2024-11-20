// auto-generated
namespace Engage.Application.Services.ProductSuppliers.Queries;

public class ProductSupplierListQuery : IRequest<List<ProductSupplierDto>>
{

}

public class ProductSupplierListHandler : ListQueryHandler, IRequestHandler<ProductSupplierListQuery, List<ProductSupplierDto>>
{
    public ProductSupplierListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductSupplierDto>> Handle(ProductSupplierListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSuppliers.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ThenBy(e => e.Code)
                              .ProjectTo<ProductSupplierDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}