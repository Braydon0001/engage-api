namespace Engage.Application.Services.EmployeeVehicles.Commands;

public class EmployeeVehicleReassignCommand : IRequest<OperationStatus>
{
    public int EmployeeVehicleId { get; set; }
    public int EmployeeId { get; set; }
    public bool SaveChanges { get; set; } = true;
}

public class EmployeeVehicleReassignHandler : IRequestHandler<EmployeeVehicleReassignCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public EmployeeVehicleReassignHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(EmployeeVehicleReassignCommand request, CancellationToken cancellationToken)
    {
        var asset = await _context.EmployeeVehicles.SingleOrDefaultAsync(e => e.EmployeeVehicleId == request.EmployeeVehicleId, cancellationToken);
        if (asset == null)
        {
            throw new NotFoundException(nameof(EmployeeVehicle), request.EmployeeVehicleId);
        }

        if (asset.EmployeeId == request.EmployeeId)
        {
            return new OperationStatus(true);
        }

        var history = new EmployeeVehicleHistory
        {
            EmployeeVehicleId = asset.EmployeeVehicleId,
            OldEmployeeId = asset.EmployeeId,
            NewEmployeeId = request.EmployeeId,
        };

        _context.EmployeeVehicleHistories.Add(history);
        //_context.EmployeeVehicles.Remove(asset);
        asset.EmployeeId = request.EmployeeId;

        return await _context.SaveChangesAsync(cancellationToken);

        //return await _mediator.Send(new EmployeeVehicleCreateCommand
        //{
        //    EmployeeId = request.EmployeeId,
        //    VehicleTypeId = asset.VehicleTypeId,
        //    VehicleBrandId = asset.VehicleBrandId,
        //    AssetStatusId = asset.AssetStatusId,
        //    AssetOwnerId = asset.AssetOwnerId,
        //    Name = asset.Name,
        //    Description = asset.Description,
        //    Tracker = asset.Tracker,
        //    Year = asset.Year,
        //    RegistrationNumber = asset.RegistrationNumber,
        //    Vin = asset.Vin,
        //    Note = asset.Note,
        //    SaveChanges = request.SaveChanges
        //}, cancellationToken);
    }
}
