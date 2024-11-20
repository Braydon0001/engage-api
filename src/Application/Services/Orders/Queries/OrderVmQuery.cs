using Engage.Application.Services.DistributionCenters.Queries;
using Engage.Application.Services.OrderEngageDepartments;
using Engage.Application.Services.Orders.Models;
using Engage.Application.Services.OrderSkus.Queries;
using Engage.Application.Services.Stores.Queries;
using Engage.Application.Services.Suppliers.Queries;

namespace Engage.Application.Services.Orders.Queries;

public class OrderVmQuery : GetByNullableIdQuery, IRequest<OrderVM>
{
}

public class OrderVmQueryHandler : BaseViewModelQueryHandler, IRequestHandler<OrderVmQuery, OrderVM>
{
    private readonly IUserService _user;
    private readonly OrderDefaultsOptions _options;

    public OrderVmQueryHandler(IMediator mediator, IUserService user, IOptions<OrderDefaultsOptions> options) : base(mediator)
    {
        _user = user;
        _options = options.Value;
    }

    public async Task<OrderVM> Handle(OrderVmQuery request, CancellationToken cancellationToken)
    {
        var vm = new OrderVM();
        var id = 0;

        if (request.Id.HasValue)
        {
            id = request.Id.Value;
            vm.Order = await _mediator.Send(new OrderQuery() { Id = id }, cancellationToken);
            if (vm.Order != null)
            {
                vm.OrderStatusIdOption = await _mediator.Send(new OptionQuery { Option = OptionDesc.ORDERSTATUSES, Id = vm.Order.OrderStatusId }, cancellationToken);
                vm.DistributionCenterIdOption = await _mediator.Send(new DistributionCenterByStoreQuery { DistributionCenterId = vm.Order.DistributionCenterId, StoreId = vm.Order.StoreId }, cancellationToken);

                var store = await _mediator.Send(new GetStoreQuery { Id = vm.Order.StoreId }, cancellationToken);
                vm.StoreIdOption = new OptionDto { Id = store.Id, Name = store.Name };

                // SupplierId is nullable so a it is possible to have an order without a supplier.
                // In which case we infer it is the default Enage supplier.
                var supplierId = vm.Order.SupplierId > 0 ? vm.Order.SupplierId : _options.SupplierId;

                var supplier = await _mediator.Send(new SupplierQuery { Id = supplierId }, cancellationToken);
                vm.SupplierIdOption = new OptionDto { Id = supplier.Id, Name = supplier.Name };
            }
        };

        vm.OrderSkus = await _mediator.Send(new GetOrderSkusByQuantityTypeQuery { OrderId = id }, cancellationToken);

        var unassignedEngageDepartments = await _mediator.Send(new OrderEngageDepartmentsByUserNameQuery { OrderIdExclusion = id, UserName = _user.UserName }, cancellationToken);
        vm.UnassignedEngageDepartments = unassignedEngageDepartments.Data;

        return vm;
    }
}

