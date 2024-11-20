// auto-generated
namespace Engage.Application.Services.ProductSubCategories.Queries;

public class ProductSubCategoryListQuery : IRequest<List<ProductSubCategoryDto>>
{

}

public class ProductSubCategoryListHandler : ListQueryHandler, IRequestHandler<ProductSubCategoryListQuery, List<ProductSubCategoryDto>>
{
    public ProductSubCategoryListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductSubCategoryDto>> Handle(ProductSubCategoryListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSubCategories.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductCategoryId)
                              .ThenBy(e => e.Name)
                              .ProjectTo<ProductSubCategoryDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}