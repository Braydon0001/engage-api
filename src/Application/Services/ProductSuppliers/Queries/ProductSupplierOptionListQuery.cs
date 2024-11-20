// auto-generated
namespace Engage.Application.Services.ProductSuppliers.Queries;

public class ProductSupplierOptionListQuery : IRequest<List<ProductSupplierOption>>
{ 

}

public class ProductSupplierOptionListHandler : ListQueryHandler, IRequestHandler<ProductSupplierOptionListQuery, List<ProductSupplierOption>>
{
    public ProductSupplierOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductSupplierOption>> Handle(ProductSupplierOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSuppliers.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ThenBy(e => e.Code)
                              .ProjectTo<ProductSupplierOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}