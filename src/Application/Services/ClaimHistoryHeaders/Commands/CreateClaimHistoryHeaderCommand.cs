namespace Engage.Application.Services.ClaimBatches.Commands;

public class CreateClaimHistoryHeaderCommand : IRequest<OperationStatus>
{
    public List<int> ClaimIds { get; set; }
    public int? ClaimStatusId { get; set; }
    public int? ClaimSupplierStatusId { get; set; }
    public int ClaimClassificationId { get; set; }
    public int EngageRegionId { get; set; }
    public bool SaveChanges { get; set; } = true;
}

public class CreateClaimHistoryHeaderCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateClaimHistoryHeaderCommand, OperationStatus>
{
    public CreateClaimHistoryHeaderCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(CreateClaimHistoryHeaderCommand command, CancellationToken cancellationToken)
    {
        var entity = new ClaimHistoryHeader
        {
            ClaimClassificationId = command.ClaimClassificationId,
            EngageRegionId = command.EngageRegionId,
        };
        if (command.ClaimStatusId.HasValue)
        {
            entity.ClaimStatusId = command.ClaimStatusId.Value;
        }
        if (command.ClaimSupplierStatusId.HasValue)
        {
            entity.ClaimSupplierStatusId = command.ClaimSupplierStatusId.Value;
        }
                
        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.ClaimHistoryHeaderId;
            return opStatus;
        }

        return new OperationStatus(status: true);
    }
}
