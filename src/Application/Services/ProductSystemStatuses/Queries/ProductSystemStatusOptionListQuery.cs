// auto-generated
namespace Engage.Application.Services.ProductSystemStatuses.Queries;

public class ProductSystemStatusOptionListQuery : IRequest<List<ProductSystemStatusOption>>
{ 

}

public class ProductSystemStatusOptionListHandler : ListQueryHandler, IRequestHandler<ProductSystemStatusOptionListQuery, List<ProductSystemStatusOption>>
{
    public ProductSystemStatusOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductSystemStatusOption>> Handle(ProductSystemStatusOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductSystemStatuses.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductSystemStatusOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}