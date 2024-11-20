namespace Engage.Application.Services.Claims.Commands;

public class BatchUpdateClaimSupplierStatusCommand : IRequest<OperationStatus>
{
    public List<int> ClaimIds { get; set; }
    public int ClaimSupplierStatusId { get; set; }
    public int ClaimClassificationId { get; set; }
    public int EngageRegionId { get; set; }
    public int? ClaimPendingReasonId { get; set; }
    public string PendingReason { get; set; }
}

public class BatchUpdateClaimSupplierStatusCommandHandler : IRequestHandler<BatchUpdateClaimSupplierStatusCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;

    public BatchUpdateClaimSupplierStatusCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(BatchUpdateClaimSupplierStatusCommand command, CancellationToken cancellationToken)
    {
        var claimHistoryHeader = new ClaimHistoryHeader
        {
            ClaimSupplierStatusId = command.ClaimSupplierStatusId,
            ClaimClassificationId = command.ClaimClassificationId,
            EngageRegionId = command.EngageRegionId,
        };
        
        foreach (var id in command.ClaimIds)
        {
            var updateStatusCommand = new UpdateClaimSupplierStatusCommand
            {
                Id = id,
                ClaimSupplierStatusId = command.ClaimSupplierStatusId,
                ClaimPendingReasonId = command.ClaimPendingReasonId,
                PendingReason = command.PendingReason,
                ClaimHistoryHeader = claimHistoryHeader,
                SaveChanges = false
            };        
            await _mediator.Send(updateStatusCommand, cancellationToken);            
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }
}
