using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.DistributionCenters.Commands;

public class CreateDistributionCenterCommand : DistributionCenterCommand, IRequest<OperationStatus>
{
}

public class CreateDistributionCenterCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateDistributionCenterCommand, OperationStatus>
{
    public CreateDistributionCenterCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(CreateDistributionCenterCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateDistributionCenterCommand, DistributionCenter>(command);
        _context.DistributionCenters.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        if (opStatus.Status == true)
        {
            opStatus.OperationId = entity.DistributionCenterId;

            if (command.DepartmentIds != null)
            {
                await _mediator.Send(new BatchAssignCommand(AssignDesc.DEPT_DC, entity.DistributionCenterId, command.DepartmentIds), cancellationToken);
            }

            if (command.WarehouseIds != null)
            {
                await _mediator.Send(new BatchAssignCommand(AssignDesc.WAREHOUSE_DC, entity.DistributionCenterId, command.WarehouseIds), cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        return opStatus;
    }
}
