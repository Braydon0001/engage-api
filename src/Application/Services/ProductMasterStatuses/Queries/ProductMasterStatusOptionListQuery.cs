// auto-generated
namespace Engage.Application.Services.ProductMasterStatuses.Queries;

public class ProductMasterStatusOptionListQuery : IRequest<List<ProductMasterStatusOption>>
{ 

}

public class ProductMasterStatusOptionListHandler : ListQueryHandler, IRequestHandler<ProductMasterStatusOptionListQuery, List<ProductMasterStatusOption>>
{
    public ProductMasterStatusOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductMasterStatusOption>> Handle(ProductMasterStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductMasterStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductMasterStatusOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}