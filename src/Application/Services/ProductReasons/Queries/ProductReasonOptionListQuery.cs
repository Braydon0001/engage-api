// auto-generated
namespace Engage.Application.Services.ProductReasons.Queries;

public class ProductReasonOptionListQuery : IRequest<List<ProductReasonOption>>
{ 

}

public class ProductReasonOptionListHandler : ListQueryHandler, IRequestHandler<ProductReasonOptionListQuery, List<ProductReasonOption>>
{
    public ProductReasonOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductReasonOption>> Handle(ProductReasonOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductReasons.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductReasonOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}