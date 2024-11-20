using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.Stores.Commands;

public class UpdateStoreCommand : StoreCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateStoreCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateStoreCommand, OperationStatus>
{
    public UpdateStoreCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(UpdateStoreCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Stores.SingleAsync(x => x.StoreId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var engageRegion = await _context.EngageRegions.FindAsync(command.EngageRegionId);
        entity.StoreSparRegionId = engageRegion.StoreSparRegionId.HasValue ? engageRegion.StoreSparRegionId.Value : 1;

        if (command.StoreDepartmentIds != null)
        {
            await _mediator.Send(new BatchAssignCommand(AssignDesc.DEPT_STORE, command.Id, command.StoreDepartmentIds), cancellationToken);
        }

        if (command.StoreConceptIds != null)
        {
            await _mediator.Send(new BatchAssignCommand(AssignDesc.CONCEPT_LEVEL_STORE, command.Id, command.StoreConceptIds), cancellationToken);
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.StoreId;
        return opStatus;
    }
}
