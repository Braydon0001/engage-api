namespace Engage.Application.Services.ClaimPeriods.Commands;

public class UpdateClaimPeriodCommand : ClaimPeriodCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateClaimPeriodCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateClaimPeriodCommand, OperationStatus>
{
    public UpdateClaimPeriodCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateClaimPeriodCommand command, CancellationToken cancellationToken)
    {
        if (command.EndDate < command.StartDate)
        {
            throw new Exception("Start date must be before end date");
        }

        var entity = await _context.ClaimPeriods.SingleAsync(x => x.ClaimPeriodId == command.Id, cancellationToken);

        var previousPeriods = await _context.ClaimPeriods
                            .Where(e =>
                            command.StartDate >= e.StartDate && command.StartDate <= e.EndDate
                                && e.ClaimPeriodId != entity.ClaimPeriodId
                            || command.EndDate >= e.StartDate && command.EndDate <= e.EndDate
                                && e.ClaimPeriodId != entity.ClaimPeriodId
                            )
                            .FirstOrDefaultAsync(cancellationToken);

        if (previousPeriods != null)
        {
            throw new Exception("Period cannot intercept a different period");
        }

        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.ClaimPeriodId;
        return opStatus;
    }
}
