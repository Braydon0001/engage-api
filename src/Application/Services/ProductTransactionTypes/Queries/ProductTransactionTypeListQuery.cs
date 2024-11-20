// auto-generated
namespace Engage.Application.Services.ProductTransactionTypes.Queries;

public class ProductTransactionTypeListQuery : IRequest<List<ProductTransactionTypeDto>>
{

}

public class ProductTransactionTypeListHandler : ListQueryHandler, IRequestHandler<ProductTransactionTypeListQuery, List<ProductTransactionTypeDto>>
{
    public ProductTransactionTypeListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductTransactionTypeDto>> Handle(ProductTransactionTypeListQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductTransactionTypes.AsQueryable().AsNoTracking();

        return await queryable.OrderBy(e => e.Name)
                              .ProjectTo<ProductTransactionTypeDto>(_mapper.ConfigurationProvider)
                              .OrderBy(e => e.Id)
                              .ToListAsync(cancellationToken);
    }
}