using Engage.Application.Services.Orders.Models;
using Engage.Application.Services.OrderSkus.Queries;
using Engage.Application.Services.Suppliers.Queries;

namespace Engage.Application.Services.Orders;

// Queries
public class OrderQuery : GetByIdQuery, IRequest<OrderDto>
{
}

public class OrderVmQuery2 : GetByIdQuery, IRequest<OrderVm>
{
}

// Handlers
public class OrderQueryHandler : BaseQueryHandler, IRequestHandler<OrderQuery, OrderDto>
{
    public OrderQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OrderDto> Handle(OrderQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders
            .Include(x => x.OrderStatus)
            .Include(x => x.OrderEngageDepartments)
                .ThenInclude(x => x.EngageDepartment)
            .FirstOrDefaultAsync(x => x.OrderId == request.Id, cancellationToken);

        return _mapper.Map<Order, OrderDto>(entity);
    }
}

public class OrderVmQueryHandler2 : BaseQueryHandler, IRequestHandler<OrderVmQuery2, OrderVm>
{
    private readonly IMediator _mediator;
    private readonly OrderDefaultsOptions _options;

    public OrderVmQueryHandler2(IAppDbContext context, IMapper mapper, IMediator mediator, IOptions<OrderDefaultsOptions> options) : base(context, mapper)
    {
        _mediator = mediator;
        _options = options.Value;
    }

    public async Task<OrderVm> Handle(OrderVmQuery2 request, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders.IgnoreQueryFilters()
                                          .Include(x => x.OrderStatus)
                                          .Include(x => x.OrderType)
                                          .Include(x => x.Store)
                                          .Include(x => x.DistributionCenter)
                                          .Include(x => x.OrderEngageDepartments)
                                          .ThenInclude(x => x.EngageDepartment)
                                          .SingleAsync(x => x.OrderId == request.Id, cancellationToken);

        var vm = _mapper.Map<Order, OrderVm>(entity);

        var supplier = await _mediator.Send(new SupplierQuery
        {
            // If necessary, use the default host supplier.
            Id = entity.SupplierId ?? _options.SupplierId
        }, cancellationToken);
        vm.SupplierId = new OptionDto { Id = supplier.Id, Name = supplier.Name };

        vm.OrderSkus = await _mediator.Send(new OrderSkuListQuery
        {
            OrderId = request.Id
        }, cancellationToken);

        return vm;
    }
}
