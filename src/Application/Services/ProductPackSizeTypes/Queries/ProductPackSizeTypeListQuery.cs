// auto-generated
namespace Engage.Application.Services.ProductPackSizeTypes.Queries;

public class ProductPackSizeTypeListQuery : IRequest<List<ProductPackSizeTypeDto>>
{

}

public class ProductPackSizeTypeListHandler : ListQueryHandler, IRequestHandler<ProductPackSizeTypeListQuery, List<ProductPackSizeTypeDto>>
{
    public ProductPackSizeTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductPackSizeTypeDto>> Handle(ProductPackSizeTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductPackSizeTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductPackSizeTypeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}