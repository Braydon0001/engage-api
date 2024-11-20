using Engage.Application.Services.Shared.AssignCommands;

namespace Engage.Application.Services.EngageMasterProducts.Commands;

public class CreateEngageMasterProductCommand : EngageMasterProductCommand, IRequest<OperationStatus>
{
}

public class CreateEngageMasterProductCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEngageMasterProductCommand, OperationStatus>
{
    public CreateEngageMasterProductCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator)
        : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(CreateEngageMasterProductCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateEngageMasterProductCommand, EngageMasterProduct>(command);
        _context.EngageMasterProducts.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status == true)
        {
            opStatus.OperationId = entity.EngageMasterProductId;

            if (command.EngageTagIds != null)
            {
                await _mediator.Send(new BatchAssignCommand(AssignDesc.TAG_PRODUCT, entity.EngageMasterProductId, command.EngageTagIds), cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        return opStatus;
    }
}
