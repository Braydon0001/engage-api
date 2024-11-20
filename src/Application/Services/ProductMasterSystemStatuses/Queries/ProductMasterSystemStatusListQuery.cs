// auto-generated
namespace Engage.Application.Services.ProductMasterSystemStatuses.Queries;

public class ProductMasterSystemStatusListQuery : IRequest<List<ProductMasterSystemStatusDto>>
{

}

public class ProductMasterSystemStatusListHandler : ListQueryHandler, IRequestHandler<ProductMasterSystemStatusListQuery, List<ProductMasterSystemStatusDto>>
{
    public ProductMasterSystemStatusListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductMasterSystemStatusDto>> Handle(ProductMasterSystemStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductMasterSystemStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductMasterSystemStatusDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}