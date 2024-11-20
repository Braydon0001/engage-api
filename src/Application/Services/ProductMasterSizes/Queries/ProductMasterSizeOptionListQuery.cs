// auto-generated
namespace Engage.Application.Services.ProductMasterSizes.Queries;

public class ProductMasterSizeOptionListQuery : IRequest<List<ProductMasterSizeOption>>
{
    public int? ProductMasterId { get; set; }
}

public class ProductMasterSizeOptionListHandler : ListQueryHandler, IRequestHandler<ProductMasterSizeOptionListQuery, List<ProductMasterSizeOption>>
{
    public ProductMasterSizeOptionListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductMasterSizeOption>> Handle(ProductMasterSizeOptionListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductMasterSizes.AsQueryable().AsNoTracking();

        if (query.ProductMasterId.HasValue)
        {
            queryable = queryable.Where(e => e.ProductMasterId == query.ProductMasterId);
        }

        return await queryable.OrderBy(e => e.ProductMasterSizeId)
                              .ProjectTo<ProductMasterSizeOption>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}