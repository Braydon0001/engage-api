namespace Engage.Application.Services.ClaimHistories.Commands;

public class CreateClaimHistoryCommand : IRequest<OperationStatus>
{
    public Claim Claim { get; set; }
    public ClaimHistoryHeader ClaimHistoryHeader { get; set; }
    public bool SaveChanges { get; set; } = true;

    public CreateClaimHistoryCommand(Claim claim, ClaimHistoryHeader claimHistoryHeader, bool saveChanges = true)
    {
        Claim = claim;
        ClaimHistoryHeader = claimHistoryHeader;
        SaveChanges = saveChanges;
    }
    
    public CreateClaimHistoryCommand(Claim claim, bool saveChanges = true)
    {
        Claim = claim;        
        SaveChanges = saveChanges;
    }
}

public class CreateClaimHistoryCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateClaimHistoryCommand, OperationStatus>
{
    public CreateClaimHistoryCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateClaimHistoryCommand command, CancellationToken cancellationToken)
    {
        var history = new ClaimHistory
        {
            ClaimId = command.Claim.ClaimId,
            ClaimStatusId = command.Claim.ClaimStatusId,
            ClaimSupplierStatusId = command.Claim.ClaimSupplierStatusId
        };

        
        if (!string.IsNullOrWhiteSpace(command.Claim.PendingReason))
        {
            history.PendingReason = command.Claim.PendingReason;
        }
        if (command.Claim.ClaimPendingReasonId.HasValue)
        {
            history.ClaimPendingReasonId = command.Claim.ClaimPendingReasonId.Value;
        }
        if (!string.IsNullOrWhiteSpace(command.Claim.RejectedReason))
        {
            history.RejectedReason = command.Claim.RejectedReason;
        }
        if (command.Claim.ClaimRejectedReasonId.HasValue)
        {
            history.ClaimRejectedReasonId = command.Claim.ClaimRejectedReasonId.Value;
        }

        _context.ClaimHistories.Add(history);

        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = history.ClaimHistoryId;
            return opStatus;
        }

        return new OperationStatus
        {
            Status = true
        };
    }
}
