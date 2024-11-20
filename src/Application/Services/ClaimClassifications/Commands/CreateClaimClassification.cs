using Engage.Application.Services.Shared.AssignCommands;
namespace Engage.Application.Services.ClaimClassifications.Commands;

public class CreateClaimClassificationCommand : ClaimClassificationCommand, IRequest<OperationStatus>
{

}

public class CreateAssetCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateClaimClassificationCommand, OperationStatus>
{
    public CreateAssetCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateClaimClassificationCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateClaimClassificationCommand, ClaimClassification>(command);
        _context.ClaimClassifications.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status == true)
        {
            opStatus.OperationId = entity.ClaimClassificationId;

            if (command.ClaimTypeIds != null)
            {
                await _mediator.Send(new BatchAssignCommand(AssignDesc.CLAIM_CLASSIFICATION_TYPE, entity.ClaimClassificationId, command.ClaimTypeIds), cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        return opStatus;
    }
}
