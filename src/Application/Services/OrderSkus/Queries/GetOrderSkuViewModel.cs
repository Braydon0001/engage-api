using Engage.Application.Services.Options.Queries;
using Engage.Application.Services.OrderSkus.Models;

namespace Engage.Application.Services.OrderSkus.Queries
{
    public class GetOrderSkuViewModelQuery : GetByNullableIdQuery, IRequest<OrderSkuVM>
    {
    }

    public class GetOrderSkuViewModelQueryHandler : BaseViewModelQueryHandler, IRequestHandler<GetOrderSkuViewModelQuery, OrderSkuVM>
    {

        public GetOrderSkuViewModelQueryHandler(IMediator mediator) : base(mediator) { }

        public async Task<OrderSkuVM> Handle(GetOrderSkuViewModelQuery request, CancellationToken cancellationToken)
        {
            var vm = new OrderSkuVM();
            if (request.Id.HasValue)
            {
                vm.OrderSku = await _mediator.Send(new GetOrderSkuQuery() { Id = request.Id.Value });
            };

            vm.OrderSkuTypes = await _mediator.Send(new OptionsQuery(nameof(vm.OrderSkuTypes)));
            vm.OrderSkuStatuses = await _mediator.Send(new OptionsQuery(nameof(vm.OrderSkuStatuses)));

            return vm;
        }
    }
}
