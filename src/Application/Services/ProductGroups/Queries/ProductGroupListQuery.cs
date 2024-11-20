// auto-generated
namespace Engage.Application.Services.ProductGroups.Queries;

public class ProductGroupListQuery : IRequest<List<ProductGroupDto>>
{

}

public class ProductGroupListHandler : ListQueryHandler, IRequestHandler<ProductGroupListQuery, List<ProductGroupDto>>
{
    public ProductGroupListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductGroupDto>> Handle(ProductGroupListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductGroups.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductGroupDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}