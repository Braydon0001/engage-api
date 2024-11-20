using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.EngageMasterProducts.Commands;

public class UpdateEngageMasterProductCommand : EngageMasterProductCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEngageMasterProductCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEngageMasterProductCommand, OperationStatus>
{
    public UpdateEngageMasterProductCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) :
        base(context, mapper, mediator)
    { }

    public async Task<OperationStatus> Handle(UpdateEngageMasterProductCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageMasterProducts.SingleAsync(x => x.EngageMasterProductId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        if (command.EngageTagIds != null)
        {
            await _mediator.Send(new BatchAssignCommand(AssignDesc.TAG_PRODUCT, command.Id, command.EngageTagIds), cancellationToken);
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EngageMasterProductId;
        return opStatus;
    }
}
