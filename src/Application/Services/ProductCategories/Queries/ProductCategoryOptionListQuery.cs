// auto-generated
namespace Engage.Application.Services.ProductCategories.Queries;

public class ProductCategoryOptionListQuery : IRequest<List<ProductCategoryOption>>
{ 

}

public class ProductCategoryOptionListHandler : ListQueryHandler, IRequestHandler<ProductCategoryOptionListQuery, List<ProductCategoryOption>>
{
    public ProductCategoryOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductCategoryOption>> Handle(ProductCategoryOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductCategories.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductCategoryId)
                              .ProjectTo<ProductCategoryOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}