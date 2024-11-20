using Engage.Application.Services.EmployeeStores.Queries;
using Engage.Application.Services.Stores.Models;

namespace Engage.Application.Services.Stores.Queries;

public class GetStoreCallCyclesViewModelQuery : GetByNullableIdQuery, IRequest<StoreCallCyclesVM>
{ }

public class GetStoreCallCyclesViewModelQueryHandler : BaseViewModelQueryHandler, IRequestHandler<GetStoreCallCyclesViewModelQuery, StoreCallCyclesVM>
{
    public GetStoreCallCyclesViewModelQueryHandler(IMediator mediator) : base(mediator) { }

    public async Task<StoreCallCyclesVM> Handle(GetStoreCallCyclesViewModelQuery request, CancellationToken cancellationToken)
    {
        var vm = new StoreCallCyclesVM();
        var id = 0;
        if (request.Id.HasValue)
        {
            id = request.Id.Value;
            vm.Store = await _mediator.Send(new GetStoreQuery { Id = id });

        };

        vm.EmployeeStores = await _mediator.Send(new EmployeeStoresQuery() { StoreId = id });

        return vm;
    }
}
