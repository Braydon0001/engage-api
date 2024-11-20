// auto-generated
namespace Engage.Application.Services.ProductMasterSystemStatuses.Queries;

public class ProductMasterSystemStatusOptionListQuery : IRequest<List<ProductMasterSystemStatusOption>>
{ 

}

public class ProductMasterSystemStatusOptionListHandler : ListQueryHandler, IRequestHandler<ProductMasterSystemStatusOptionListQuery, List<ProductMasterSystemStatusOption>>
{
    public ProductMasterSystemStatusOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductMasterSystemStatusOption>> Handle(ProductMasterSystemStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductMasterSystemStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductMasterSystemStatusOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}