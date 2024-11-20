// auto-generated
namespace Engage.Application.Services.ProductSubGroups.Queries;

public class ProductSubGroupListQuery : IRequest<List<ProductSubGroupDto>>
{

}

public class ProductSubGroupListHandler : ListQueryHandler, IRequestHandler<ProductSubGroupListQuery, List<ProductSubGroupDto>>
{
    public ProductSubGroupListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductSubGroupDto>> Handle(ProductSubGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSubGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.ProductGroupId)
                              .ThenBy(e => e.Name)
                              .ProjectTo<ProductSubGroupDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}