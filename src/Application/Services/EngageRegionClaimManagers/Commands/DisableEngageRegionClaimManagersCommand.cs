namespace Engage.Application.Services.EngageRegionClaimManagers.Commands;

public class DisableEngageRegionClaimManagersCommand : IRequest<OperationStatus>
{
    public int EngageRegionId { get; set; }

    public int UserId { get; set; }
}

public class DisableEngageRegionClaimManagersCommandHandler : BaseCreateCommandHandler, IRequestHandler<DisableEngageRegionClaimManagersCommand, OperationStatus>
{
    public DisableEngageRegionClaimManagersCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(DisableEngageRegionClaimManagersCommand command, CancellationToken cancellationToken)
    {
        var record = await _context.EngageRegionClaimManagers.IgnoreQueryFilters()
                                                    .Where(e => e.EngageRegionId == command.EngageRegionId
                                                                && e.UserId == command.UserId)
                                                    .FirstOrDefaultAsync(cancellationToken);

        if (record != null)
        {
            record.Disabled = !record.Disabled;

            return await _context.SaveChangesAsync(cancellationToken);
        }

        throw new ClaimException("Disable Action Failed. \n\n Could not find record to disable.");
    }
}

