using Engage.Application.Services.EmployeeStoreCheckIns.Models;

namespace Engage.Application.Services.EmployeeStoreCheckIns.Queries;

public class GetEmployeeStoreCheckInViewModelQuery : GetByNullableIdQuery, IRequest<EmployeeStoreCheckInVM>
{
}

public class GetEmployeeStoreCheckInViewModelQueryHandler : BaseViewModelQueryHandler, IRequestHandler<GetEmployeeStoreCheckInViewModelQuery, EmployeeStoreCheckInVM>
{
    public GetEmployeeStoreCheckInViewModelQueryHandler(IMediator mediator) : base(mediator) { }

    public async Task<EmployeeStoreCheckInVM> Handle(GetEmployeeStoreCheckInViewModelQuery request, CancellationToken cancellationToken)
    {
        var vm = new EmployeeStoreCheckInVM();

        if (request.Id.HasValue)
        {
            vm.EmployeeStoreCheckIn = await _mediator.Send(new GetEmployeeStoreCheckInQuery() { Id = request.Id.Value });
        }

        return vm;
    }
}
