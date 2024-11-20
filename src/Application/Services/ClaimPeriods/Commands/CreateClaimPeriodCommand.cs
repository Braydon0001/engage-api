namespace Engage.Application.Services.ClaimPeriods.Commands;

public class CreateClaimPeriodCommand : ClaimPeriodCommand, IRequest<OperationStatus>
{

}

public class CreateClaimPeriodCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateClaimPeriodCommand, OperationStatus>
{
    public CreateClaimPeriodCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateClaimPeriodCommand command, CancellationToken cancellationToken)
    {
        if (command.EndDate < command.StartDate)
        {
            throw new Exception("Start date must be before end date");
        }

        var previousPeriods = await _context.ClaimPeriods
                            .Where(e =>
                            command.StartDate >= e.StartDate && command.StartDate <= e.EndDate
                            || command.EndDate >= e.StartDate && command.EndDate <= e.EndDate
                            )
                            .FirstOrDefaultAsync(cancellationToken);
        if (previousPeriods != null)
        {
            throw new Exception("Period cannot intercept a different period");
        }

        var entity = _mapper.Map<CreateClaimPeriodCommand, ClaimPeriod>(command);
        _context.ClaimPeriods.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.ClaimPeriodId;
        return opStatus;
    }
}
