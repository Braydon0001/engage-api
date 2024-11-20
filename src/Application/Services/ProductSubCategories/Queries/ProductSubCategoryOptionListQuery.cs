// auto-generated
namespace Engage.Application.Services.ProductSubCategories.Queries;

public class ProductSubCategoryOptionListQuery : IRequest<List<ProductSubCategoryOption>>
{ 

}

public class ProductSubCategoryOptionListHandler : ListQueryHandler, IRequestHandler<ProductSubCategoryOptionListQuery, List<ProductSubCategoryOption>>
{
    public ProductSubCategoryOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductSubCategoryOption>> Handle(ProductSubCategoryOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSubCategories.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductCategoryId)
                              .ThenBy(e => e.Name)
                              .ProjectTo<ProductSubCategoryOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}