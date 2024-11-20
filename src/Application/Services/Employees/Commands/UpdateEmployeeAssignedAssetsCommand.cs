using Engage.Application.Services.EmployeeAssets.Commands;
using Engage.Application.Services.EmployeeCoolerBoxes.Commands;
using Engage.Application.Services.EmployeeVehicles.Commands;

namespace Engage.Application.Services.Employees.Commands;

public class UpdateEmployeeAssignedAssetsCommand : IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
    public ReassignAsset[] ReassignAssets { get; set; }
}
public class UpdateEmployeeAssignedAssetsCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeAssignedAssetsCommand, OperationStatus>
{
    public UpdateEmployeeAssignedAssetsCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    { }

    public async Task<OperationStatus> Handle(UpdateEmployeeAssignedAssetsCommand request, CancellationToken cancellationToken)
    {
        if (request.ReassignAssets.Length == 0)
        {
            return null;
        }
        var employee = await _context.Employees.SingleOrDefaultAsync(e => e.EmployeeId == request.EmployeeId);
        if (employee == null)
        {
            return null;
        }
        foreach (ReassignAsset asset in request.ReassignAssets)
        {
            switch (asset.AssetType)
            {
                case "Electronics":
                    var result = await _mediator.Send(new EmployeeAssetReassignCommand
                    { EmployeeAssetId = (int)asset.Id, EmployeeId = (int)request.EmployeeId });
                    break;
                case "Vehicles":
                    await _mediator.Send(new EmployeeVehicleReassignCommand
                    { EmployeeVehicleId = (int)asset.Id, EmployeeId = (int)request.EmployeeId });
                    break;
                case "Cooler Boxes":
                    await _mediator.Send(new EmployeeCoolerBoxReassignCommand
                    { EmployeeCoolerBoxId = (int)asset.Id, EmployeeId = (int)request.EmployeeId });
                    break;
                default:
                    //throw error
                    break;
            }
        }
        return new OperationStatus(true);
    }
}
public class UpdateEmployeeAssignedAssetsValidator : AbstractValidator<UpdateEmployeeAssignedAssetsCommand>
{
    public UpdateEmployeeAssignedAssetsValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.ReassignAssets).NotEmpty();
    }
}

public class ReassignAsset
{
    public int Id { get; set; }
    public string AssetType { get; set; }
}
