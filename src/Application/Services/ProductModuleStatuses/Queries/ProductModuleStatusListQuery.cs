// auto-generated
namespace Engage.Application.Services.ProductModuleStatuses.Queries;

public class ProductModuleStatusListQuery : IRequest<List<ProductModuleStatusDto>>
{

}

public class ProductModuleStatusListHandler : ListQueryHandler, IRequestHandler<ProductModuleStatusListQuery, List<ProductModuleStatusDto>>
{
    public ProductModuleStatusListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductModuleStatusDto>> Handle(ProductModuleStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductModuleStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductModuleStatusDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}