// auto-generated
namespace Engage.Application.Services.ProductSystemStatuses.Queries;

public class ProductSystemStatusListQuery : IRequest<List<ProductSystemStatusDto>>
{

}

public class ProductSystemStatusListHandler : ListQueryHandler, IRequestHandler<ProductSystemStatusListQuery, List<ProductSystemStatusDto>>
{
    public ProductSystemStatusListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductSystemStatusDto>> Handle(ProductSystemStatusListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSystemStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductSystemStatusDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}