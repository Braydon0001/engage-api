using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.EngageSubGroups.Commands;

public class CreateEngageSubGroupCommand : EngageSubGroupCommand, IRequest<OperationStatus>
{
}

public class CreateAssetCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEngageSubGroupCommand, OperationStatus>
{
    public CreateAssetCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(CreateEngageSubGroupCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateEngageSubGroupCommand, EngageSubGroup>(command);
        _context.EngageSubGroups.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status == true)
        {
            opStatus.OperationId = entity.Id;

            if (command.SupplierIds.NotNullOrEmpty())
            {
                await _mediator.Send(new BatchAssignCommand(
                    AssignDesc.SUPPLIER_ENGAGE_SUB_GROUP, entity.Id, command.SupplierIds));
            }


        }

        return opStatus;
    }
}
