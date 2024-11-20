namespace Engage.Application.Services.Orders.Queries;

public class OrderSuppliersQuery : GetQuery, IRequest<List<OptionDto>>
{
}

public class GetOrderSuppliersQueryHandler : IRequestHandler<OrderSuppliersQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public GetOrderSuppliersQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(OrderSuppliersQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Suppliers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(o => EF.Functions.Like(o.Name, $"%{request.Search}%"));
        }

        return await query.Where(o => o.OrderModuleEnabled && o.Disabled == false)
                          .Select(o => new OptionDto { Id = o.SupplierId, Name = o.Name })
                          .Take(100)
                          .OrderBy(o => o.Name)
                          .ToListAsync(cancellationToken);
    }
}
