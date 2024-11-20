// auto-generated
namespace Engage.Application.Services.ProductMasterStatuses.Queries;

public class ProductMasterStatusListQuery : IRequest<List<ProductMasterStatusDto>>
{

}

public class ProductMasterStatusListHandler : ListQueryHandler, IRequestHandler<ProductMasterStatusListQuery, List<ProductMasterStatusDto>>
{
    public ProductMasterStatusListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductMasterStatusDto>> Handle(ProductMasterStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductMasterStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductMasterStatusDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}