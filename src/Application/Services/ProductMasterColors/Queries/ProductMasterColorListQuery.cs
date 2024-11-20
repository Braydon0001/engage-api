// auto-generated
namespace Engage.Application.Services.ProductMasterColors.Queries;

public class ProductMasterColorListQuery : IRequest<List<ProductMasterColorDto>>
{
    public int? ProductMasterId { get; set; }
}

public class ProductMasterColorListHandler : ListQueryHandler, IRequestHandler<ProductMasterColorListQuery, List<ProductMasterColorDto>>
{
    public ProductMasterColorListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductMasterColorDto>> Handle(ProductMasterColorListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductMasterColors.AsQueryable().AsNoTracking();

        if (query.ProductMasterId.HasValue)
        {
            queryable = queryable.Where(e => e.ProductMasterId == query.ProductMasterId);
        }

        return await queryable.OrderBy(e => e.ProductMasterColorId)
                              .ProjectTo<ProductMasterColorDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}