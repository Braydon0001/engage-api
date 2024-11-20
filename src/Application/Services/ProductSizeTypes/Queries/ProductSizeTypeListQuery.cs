// auto-generated
namespace Engage.Application.Services.ProductSizeTypes.Queries;

public class ProductSizeTypeListQuery : IRequest<List<ProductSizeTypeDto>>
{

}

public class ProductSizeTypeListHandler : ListQueryHandler, IRequestHandler<ProductSizeTypeListQuery, List<ProductSizeTypeDto>>
{
    public ProductSizeTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductSizeTypeDto>> Handle(ProductSizeTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSizeTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductSizeTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}