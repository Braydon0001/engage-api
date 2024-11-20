// auto-generated
namespace Engage.Application.Services.ProductModuleStatuses.Queries;

public class ProductModuleStatusOptionListQuery : IRequest<List<ProductModuleStatusOption>>
{ 

}

public class ProductModuleStatusOptionListHandler : ListQueryHandler, IRequestHandler<ProductModuleStatusOptionListQuery, List<ProductModuleStatusOption>>
{
    public ProductModuleStatusOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductModuleStatusOption>> Handle(ProductModuleStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductModuleStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductModuleStatusOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}