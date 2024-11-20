using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.EngageSubGroups.Commands;

public class UpdateEngageSubGroupCommand : EngageSubGroupCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateUpdateEngageSubGroupCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEngageSubGroupCommand, OperationStatus>
{
    public UpdateUpdateEngageSubGroupCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEngageSubGroupCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageSubGroups.SingleAsync(x => x.Id == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        if (opStatus.Exception == false)
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
