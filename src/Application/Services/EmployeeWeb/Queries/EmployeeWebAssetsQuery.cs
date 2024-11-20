using Engage.Application.Services.EmployeeAssets.Queries;
using Engage.Application.Services.EmployeeCoolerBoxes.Queries;
using Engage.Application.Services.EmployeeVehicles.Queries;
using Engage.Application.Services.EmployeeWeb.Models;

namespace Engage.Application.Services.EmployeeWeb.Queries;

public record EmployeeWebAssetsQuery(int EmployeeId) : IRequest<EmployeeWebAssetsVm>
{
}

public class EmployeeWebAssetsHandler : BaseQueryHandler, IRequestHandler<EmployeeWebAssetsQuery, EmployeeWebAssetsVm>
{
    private readonly IMediator _mediator;
    public EmployeeWebAssetsHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
    {
        _mediator = mediator;
    }

    public async Task<EmployeeWebAssetsVm> Handle(EmployeeWebAssetsQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.IgnoreQueryFilters()
                                             .SingleAsync(x => x.EmployeeId == request.EmployeeId, cancellationToken);

        var vm = new EmployeeWebAssetsVm();

        var assets = await _mediator.Send(new EmployeeAssetsQuery { EmployeeId = request.EmployeeId }, cancellationToken);
        var vehicles = await _mediator.Send(new EmployeeVehiclesQuery { EmployeeId = request.EmployeeId }, cancellationToken);
        var coolerBoxes = await _mediator.Send(new EmployeeCoolerBoxesQuery { EmployeeId = request.EmployeeId }, cancellationToken);

        if (assets.Data.Count > 0)
        {
            vm.EmployeeAssets = assets.Data;
        }

        if (vehicles.Data.Count > 0)
        {
            vm.EmployeeVehicles = vehicles.Data;
        }

        if (coolerBoxes.Data.Count > 0)
        {
            vm.EmployeeCoolerBoxes = coolerBoxes.Data;
        }

        return vm;
    }
}