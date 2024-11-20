// auto-generated
namespace Engage.Application.Services.ProductReasons.Queries;

public class ProductReasonListQuery : IRequest<List<ProductReasonDto>>
{

}

public class ProductReasonListHandler : ListQueryHandler, IRequestHandler<ProductReasonListQuery, List<ProductReasonDto>>
{
    public ProductReasonListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductReasonDto>> Handle(ProductReasonListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductReasons.AsQueryable().AsNoTracking();
        
        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductReasonDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}