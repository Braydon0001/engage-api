// auto-generated
namespace Engage.Application.Services.ProductCategories.Queries;

public class ProductCategoryListQuery : IRequest<List<ProductCategoryDto>>
{

}

public class ProductCategoryListHandler : ListQueryHandler, IRequestHandler<ProductCategoryListQuery, List<ProductCategoryDto>>
{
    public ProductCategoryListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductCategoryDto>> Handle(ProductCategoryListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductCategories.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductCategoryId)
                              .ProjectTo<ProductCategoryDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}