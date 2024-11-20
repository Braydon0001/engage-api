// auto-generated
namespace Engage.Application.Services.ProductPackSizeTypes.Queries;

public class ProductPackSizeTypeOptionListQuery : IRequest<List<ProductPackSizeTypeOption>>
{ 

}

public class ProductPackSizeTypeOptionListHandler : ListQueryHandler, IRequestHandler<ProductPackSizeTypeOptionListQuery, List<ProductPackSizeTypeOption>>
{
    public ProductPackSizeTypeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductPackSizeTypeOption>> Handle(ProductPackSizeTypeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductPackSizeTypes.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductPackSizeTypeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}