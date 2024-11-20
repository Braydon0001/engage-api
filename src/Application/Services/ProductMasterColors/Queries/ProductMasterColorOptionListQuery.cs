// auto-generated
namespace Engage.Application.Services.ProductMasterColors.Queries;

public class ProductMasterColorOptionListQuery : IRequest<List<ProductMasterColorOption>>
{
    public int? ProductMasterId { get; set; }
}

public class ProductMasterColorOptionListHandler : ListQueryHandler, IRequestHandler<ProductMasterColorOptionListQuery, List<ProductMasterColorOption>>
{
    public ProductMasterColorOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductMasterColorOption>> Handle(ProductMasterColorOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductMasterColors.AsQueryable().AsNoTracking();

        if (query.ProductMasterId.HasValue)
        {
            queryable = queryable.Where(e => e.ProductMasterId == query.ProductMasterId);
        }

        return await queryable.OrderBy(e => e.ProductMasterColorId)
                              .ProjectTo<ProductMasterColorOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}