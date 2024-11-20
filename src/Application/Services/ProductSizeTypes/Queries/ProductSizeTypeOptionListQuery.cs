// auto-generated
namespace Engage.Application.Services.ProductSizeTypes.Queries;

public class ProductSizeTypeOptionListQuery : IRequest<List<ProductSizeTypeOption>>
{ 

}

public class ProductSizeTypeOptionListHandler : ListQueryHandler, IRequestHandler<ProductSizeTypeOptionListQuery, List<ProductSizeTypeOption>>
{
    public ProductSizeTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductSizeTypeOption>> Handle(ProductSizeTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSizeTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductSizeTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}