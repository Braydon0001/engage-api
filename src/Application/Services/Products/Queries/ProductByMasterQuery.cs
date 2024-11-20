namespace Engage.Application.Services.Products.Queries;

public class ProductByMasterQuery : IRequest<List<ProductDto>>
{
    public int ProductMasterId { get; set; }
}
public class ProductByMasterHandler : ListQueryHandler, IRequestHandler<ProductByMasterQuery, List<ProductDto>>
{
    public ProductByMasterHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductDto>> Handle(ProductByMasterQuery query, CancellationToken cancellationToken)
    {
        if (query.ProductMasterId <= 0)
            throw new Exception("Product Master not found");

        var queryable = _context.Products.AsQueryable().AsNoTracking();

        return await queryable.Where(e => e.ProductMasterId == query.ProductMasterId)
                              .OrderBy(e => e.ProductId)
                              .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}
