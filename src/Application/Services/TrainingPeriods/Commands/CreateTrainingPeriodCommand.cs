namespace Engage.Application.Services.TrainingPeriods.Commands;

public class CreateTrainingPeriodCommand : TrainingPeriodCommand, IRequest<OperationStatus>
{

}

public class CreateTrainingPeriodCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateTrainingPeriodCommand, OperationStatus>
{
    public CreateTrainingPeriodCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateTrainingPeriodCommand command, CancellationToken cancellationToken)
    {
        if (command.EndDate < command.StartDate)
        {
            throw new Exception("Start date must be before end date");
        }

        var previousPeriods = await _context.TrainingPeriods
                            .Where(e =>
                            command.StartDate >= e.StartDate && command.StartDate <= e.EndDate
                            || command.EndDate >= e.StartDate && command.EndDate <= e.EndDate
                            )
                            .FirstOrDefaultAsync(cancellationToken);
        if (previousPeriods != null)
        {
            throw new Exception("Period cannot intercept a different period");
        }

        var entity = _mapper.Map<CreateTrainingPeriodCommand, TrainingPeriod>(command);
        _context.TrainingPeriods.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.TrainingPeriodId;
        return opStatus;
    }
}
