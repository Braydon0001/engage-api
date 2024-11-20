namespace Engage.Application.Services.Trainings.Commands;

public class CreateTrainingCommand : TrainingCommand, IRequest<OperationStatus>
{
    //public List<int> EmployeeIds { get; set; }
    public List<int> FacilitatorIds { get; set; }
}

public class CreateTrainingCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateTrainingCommand, OperationStatus>
{
    public CreateTrainingCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateTrainingCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateTrainingCommand, Training>(command);
        entity.NoOfParticipants = 0;

        var trainingPeriod = await _context.TrainingPeriods.SingleOrDefaultAsync(e => DateTime.Now.Date >= e.StartDate.Date &&
                                                                                DateTime.Now.Date <= e.EndDate.Date, cancellationToken);
        if (trainingPeriod == null)
        {
            throw new TrainingException("There is no Training Period for today's date");
        }

        entity.TrainingPeriodId = trainingPeriod.TrainingPeriodId;

        _context.Trainings.Add(entity);

        if (command.FacilitatorIds != null)
        {
            if (command.FacilitatorIds.Count > 0)
            {
                if (command.IsInternalTraining)
                {
                    var count = command.FacilitatorIds.Count;
                    decimal directCost = command.DirectCost > 0 ? command.DirectCost / count : 0;
                    decimal additionalCost = command.AdditionalCost > 0 ? command.AdditionalCost / count : 0;

                    foreach (var id in command.FacilitatorIds)
                    {
                        var trainingFacilitator = new TrainingFacilitator
                        {
                            AdditionalCost = additionalCost,
                            DirectCost = directCost,
                            Training = entity,
                            EmployeeId = id,
                            Note = command.Note,
                        };

                        _context.TrainingFacilitators.Add(trainingFacilitator);
                    }
                }
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.TrainingId;
        return opStatus;
    }
}
