using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.Stores.Commands;

public class CreateStoreCommand : StoreCommand, IRequest<OperationStatus>
{
}

public class CreateStoreCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateStoreCommand, OperationStatus>
{
    public CreateStoreCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator)
        : base(context, mapper, mediator) { }

    public async Task<OperationStatus> Handle(CreateStoreCommand command, CancellationToken cancellationToken)
    {
        var engageRegion = await _context.EngageRegions.FindAsync(command.EngageRegionId);

        var existingStoreCode = await _context.Stores.Where(s => s.Code == command.Code).FirstOrDefaultAsync();
        if (existingStoreCode != null)
        {
            throw new Exception("A store already exists with the Code: " + existingStoreCode.Name + " - " + existingStoreCode.Code);
        }

        var entity = _mapper.Map<CreateStoreCommand, Store>(command);
        entity.StoreSparRegionId = engageRegion.StoreSparRegionId.HasValue ? engageRegion.StoreSparRegionId.Value : 1;
        var stakeholder = new Stakeholder
        {
            StakeholderType = StakeholderTypes.Store,
            Created = DateTime.Now
        };
        entity.Stakeholder = stakeholder;
        _context.Stores.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status == true)
        {
            opStatus.OperationId = entity.StoreId;

            if (command.StoreDepartmentIds != null)
            {
                await _mediator.Send(new BatchAssignCommand(
                   AssignDesc.DEPT_STORE, entity.StoreId, command.StoreDepartmentIds));
            }

            if (command.StoreConceptIds != null)
            {
                await _mediator.Send(new BatchAssignCommand(
                    AssignDesc.CONCEPT_LEVEL_STORE, entity.StoreId, command.StoreConceptIds));
            }

            stakeholder.StoreId = entity.StoreId;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return opStatus;
    }
}
