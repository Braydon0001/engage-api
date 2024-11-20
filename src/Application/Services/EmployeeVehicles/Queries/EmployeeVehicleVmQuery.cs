using Engage.Application.Services.EmployeeVehicles.Models;

namespace Engage.Application.Services.EmployeeVehicles.Queries;

public class EmployeeVehicleVmQuery : IRequest<EmployeeVehicleVm>
{
    public int Id { get; set; }
}

public class EmployeeVehicleVMQueryHandler : BaseQueryHandler, IRequestHandler<EmployeeVehicleVmQuery, EmployeeVehicleVm>
{
    public EmployeeVehicleVMQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeVehicleVm> Handle(EmployeeVehicleVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeVehicles.Include(e => e.Employee)
                                                    .Include(e => e.VehicleType)
                                                    .Include(e => e.VehicleBrand)
                                                    .Include(e => e.AssetStatus)
                                                    .Include(e => e.AssetOwner)
                                                    .SingleAsync(x => x.EmployeeVehicleId == request.Id, cancellationToken);

        return _mapper.Map<EmployeeVehicle, EmployeeVehicleVm>(entity);
    }
}
