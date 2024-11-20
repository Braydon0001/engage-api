// auto-generated
namespace Engage.Application.Services.ProductMasterSizes.Queries;

public class ProductMasterSizeListQuery : IRequest<List<ProductMasterSizeDto>>
{
    public int? ProductMasterId { get; set; }
}

public class ProductMasterSizeListHandler : ListQueryHandler, IRequestHandler<ProductMasterSizeListQuery, List<ProductMasterSizeDto>>
{
    public ProductMasterSizeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductMasterSizeDto>> Handle(ProductMasterSizeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductMasterSizes.AsQueryable().AsNoTracking();

        if (query.ProductMasterId.HasValue)
        {
            queryable = queryable.Where(e => e.ProductMasterId == query.ProductMasterId);
        }

        return await queryable.OrderBy(e => e.ProductMasterSizeId)
                              .ProjectTo<ProductMasterSizeDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}