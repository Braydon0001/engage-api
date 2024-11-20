namespace Engage.Application.Services.TrainingPeriods.Commands;

public class UpdateTrainingPeriodCommand : TrainingPeriodCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateTrainingPeriodCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateTrainingPeriodCommand, OperationStatus>
{
    public UpdateTrainingPeriodCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateTrainingPeriodCommand command, CancellationToken cancellationToken)
    {
        if (command.EndDate < command.StartDate)
        {
            throw new Exception("Start date must be before end date");
        }

        var entity = await _context.TrainingPeriods.SingleAsync(x => x.TrainingPeriodId == command.Id, cancellationToken);

        var previousPeriods = await _context.TrainingPeriods
                            .Where(e =>
                            command.StartDate >= e.StartDate && command.StartDate <= e.EndDate
                                && e.TrainingPeriodId != entity.TrainingPeriodId
                            || command.EndDate >= e.StartDate && command.EndDate <= e.EndDate
                                && e.TrainingPeriodId != entity.TrainingPeriodId
                            )
                            .FirstOrDefaultAsync(cancellationToken);

        if (previousPeriods != null)
        {
            throw new Exception("Period cannot intercept a different period");
        }

        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.TrainingPeriodId;
        return opStatus;
    }
}
