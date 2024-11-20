namespace Engage.Application.Services.Trainings.Commands;

public class UpdateTrainingCommand : TrainingCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
    //public List<int> EmployeeIds { get; set; }
    public List<int> FacilitatorIds { get; set; }
}

public class UpdateTrainingCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateTrainingCommand, OperationStatus>
{
    private readonly IUserService _user;
    public UpdateTrainingCommandHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateTrainingCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Trainings.SingleAsync(x => x.TrainingId == command.Id, cancellationToken);

        var currentPeriod = await _context.TrainingPeriods.SingleOrDefaultAsync(e => DateTime.Now.Date >= e.StartDate.Date &&
                                                                                DateTime.Now.Date <= e.EndDate.Date, cancellationToken);
        if (currentPeriod == null)
        {
            throw new TrainingException("There is no Training Period for today's date");
        }
        else
        {
            if (!_user.HasTrainingAdminClaim)
            {
                if (currentPeriod.TrainingPeriodId != entity.TrainingPeriodId)
                {
                    throw new TrainingException("This record can not be updated. The Cut-off date has passed");
                }
            }
        }

        _mapper.Map(command, entity);

        var employeeTrainings = await _context.EmployeeTrainings
                                                    .Where(x => x.TrainingId == command.Id)
                                                    .ToListAsync(cancellationToken);

        if (employeeTrainings.Count > 0)
        {
            var count = employeeTrainings.Count;
            decimal directCost = command.DirectCost > 0 ? command.DirectCost / count : 0;
            decimal additionalCost = command.AdditionalCost > 0 ? command.AdditionalCost / count : 0;
            foreach (var empTraining in employeeTrainings)
            {
                empTraining.DirectCost = directCost;
                empTraining.AdditionalCost = additionalCost;
            }
        }


        //Facilitators
        var existingFacilitators = await _context.TrainingFacilitators
                                                    .Where(x => x.TrainingId == command.Id)
                                                    .ToListAsync(cancellationToken);

        var facilitatorIdsToDelete = existingFacilitators
                                        .Select(c => c.EmployeeId)
                                        .Except(command.FacilitatorIds)
                                        .ToList();

        if (facilitatorIdsToDelete.Any())
        {
            foreach (var employeeId in facilitatorIdsToDelete)
            {
                var trainingFacilitatorRecord = await _context.TrainingFacilitators
                                                        .SingleAsync(x => x.TrainingId == command.Id && x.EmployeeId == employeeId, cancellationToken);

                _context.TrainingFacilitators.Remove(trainingFacilitatorRecord);
            }
        }

        if (command.FacilitatorIds.Count > 0)
        {
            if (command.IsInternalTraining)
            {
                var count = command.FacilitatorIds.Count;
                decimal directCost = command.DirectCost > 0 ? command.DirectCost / count : 0;
                decimal additionalCost = command.AdditionalCost > 0 ? command.AdditionalCost / count : 0;

                foreach (var id in command.FacilitatorIds)
                {
                    var trainingFacilitatorRecord = await _context.TrainingFacilitators
                                                                        .Where(x => x.TrainingId == command.Id && x.EmployeeId == id)
                                                                        .FirstOrDefaultAsync(cancellationToken);

                    if (trainingFacilitatorRecord != null)
                    {
                        trainingFacilitatorRecord.AdditionalCost = additionalCost;
                        trainingFacilitatorRecord.DirectCost = directCost;
                        trainingFacilitatorRecord.Note = command.Note;
                    }
                    else
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
