// auto-generated
namespace Engage.Application.Services.ProductGroups.Queries;

public class ProductGroupOptionListQuery : IRequest<List<ProductGroupOption>>
{ 

}

public class ProductGroupOptionListHandler : ListQueryHandler, IRequestHandler<ProductGroupOptionListQuery, List<ProductGroupOption>>
{
    public ProductGroupOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductGroupOption>> Handle(ProductGroupOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductGroupOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}