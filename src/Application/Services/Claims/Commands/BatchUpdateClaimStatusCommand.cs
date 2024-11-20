namespace Engage.Application.Services.Claims.Commands;

public class BatchUpdateClaimStatusCommand : IRequest<OperationStatus>
{
    public List<int> ClaimIds { get; set; }
    public int ClaimStatusId { get; set; }
    public int ClaimClassificationId { get; set; }
    public int EngageRegionId { get; set; }
    public int? ClaimRejectedReasonId { get; set; }
    public string RejectedReason { get; set; }
}

public class BatchUpdateClaimStatusCommandHandler : IRequestHandler<BatchUpdateClaimStatusCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public BatchUpdateClaimStatusCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(BatchUpdateClaimStatusCommand command, CancellationToken cancellationToken)
    {        
        foreach (var id in command.ClaimIds)
        {
            var updateStatusCommand = new UpdateClaimStatusCommand
            {
                Id = id,
                ClaimStatusId = command.ClaimStatusId,
                ClaimRejectedReasonId = command.ClaimRejectedReasonId,
                RejectedReason = command.RejectedReason,
                SaveChanges = false,
            };

            await _mediator.Send(updateStatusCommand, cancellationToken);        
        }        

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
