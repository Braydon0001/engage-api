// auto-generated
namespace Engage.Application.Services.ProductSubGroups.Queries;

public class ProductSubGroupOptionListQuery : IRequest<List<ProductSubGroupOption>>
{ 

}

public class ProductSubGroupOptionListHandler : ListQueryHandler, IRequestHandler<ProductSubGroupOptionListQuery, List<ProductSubGroupOption>>
{
    public ProductSubGroupOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductSubGroupOption>> Handle(ProductSubGroupOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSubGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductGroupId)
                              .ThenBy(e => e.Name)
                              .ProjectTo<ProductSubGroupOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}