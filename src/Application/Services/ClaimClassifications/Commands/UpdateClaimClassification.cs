using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.ClaimClassifications.Commands;

public class UpdateClaimClassificationCommand : ClaimClassificationCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateAssetCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateClaimClassificationCommand, OperationStatus>
{
    //public UpdateAssetCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public UpdateAssetCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) :
       base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(UpdateClaimClassificationCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.ClaimClassifications.SingleAsync(x => x.ClaimClassificationId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        if (command.ClaimTypeIds != null)
        {
            await _mediator.Send(new BatchAssignCommand(AssignDesc.CLAIM_CLASSIFICATION_TYPE, command.Id, command.ClaimTypeIds), cancellationToken);
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.ClaimClassificationId;
        return opStatus;
    }
}
