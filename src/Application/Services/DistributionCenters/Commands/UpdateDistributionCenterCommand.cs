using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.DistributionCenters.Commands;

public class UpdateDistributionCenterCommand : DistributionCenterCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateDistributionCenterCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateDistributionCenterCommand, OperationStatus>
{
    public UpdateDistributionCenterCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(UpdateDistributionCenterCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.DistributionCenters.SingleAsync(x => x.DistributionCenterId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        if (command.DepartmentIds != null)
        {
            await _mediator.Send(new BatchAssignCommand(AssignDesc.DEPT_DC, command.Id, command.DepartmentIds), cancellationToken);
        }

        if (command.WarehouseIds != null)
        {
            await _mediator.Send(new BatchAssignCommand(AssignDesc.WAREHOUSE_DC, command.Id, command.WarehouseIds), cancellationToken);
        }
       
        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = command.Id;
        return opStatus;
    }
}
