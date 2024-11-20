namespace Engage.Application.Services.ProductTransactions.Queries;

public class ProductTransactionByEmployeeQuery : IRequest<List<ProductTransactionDto>>
{
    public int EmployeeId { get; set; }
}
public class ProductTransactionByEmployeeHandler : BaseQueryHandler, IRequestHandler<ProductTransactionByEmployeeQuery, List<ProductTransactionDto>>
{
    public ProductTransactionByEmployeeHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<ProductTransactionDto>> Handle(ProductTransactionByEmployeeQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.ProductTransactions.AsQueryable().AsNoTracking();

        return await queryable.Where(e => e.EmployeeId == request.EmployeeId)
                              .OrderBy(e => e.ProductTransactionId)
                              .ProjectTo<ProductTransactionDto>(_mapper.ConfigurationProvider)
                              .ToListAsync(cancellationToken);
    }
}
