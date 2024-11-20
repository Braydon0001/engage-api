namespace Engage.Application.Services.ProductTransactions.Queries;

public class ProductTransactionByProductWarehouseQuery : IRequest<List<ProductTransactionByProductWarehouseDto>>
{
    public int WarehouseId { get; set; }
    public int ProductId { get; set; }
}
public class ProductTransactionByProductWarehouseHandler : BaseQueryHandler, IRequestHandler<ProductTransactionByProductWarehouseQuery, List<ProductTransactionByProductWarehouseDto>>
{
    public ProductTransactionByProductWarehouseHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductTransactionByProductWarehouseDto>> Handle(ProductTransactionByProductWarehouseQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductTransactions.AsQueryable().AsNoTracking();

        return await queryable.Where(e => e.ProductId == query.ProductId && e.ProductWarehouseId == query.WarehouseId)
                                .OrderBy(e => e.ProductTransactionId)
                                .ProjectTo<ProductTransactionByProductWarehouseDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();
    }
}