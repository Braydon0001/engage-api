namespace Engage.Application.Services.EmployeeTrainings.Commands;

public class DeleteEmployeeTrainingCommand : IRequest<OperationStatus>
{
    public int TrainingId { get; set; }
    public int EmployeeId { get; set; }
}

public class DeleteEmployeeTrainingCommandHandler : IRequestHandler<DeleteEmployeeTrainingCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMediator _mediator;


    public DeleteEmployeeTrainingCommandHandler(IAppDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<OperationStatus> Handle(DeleteEmployeeTrainingCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeTrainings
                                        .Include(x => x.Training)
                                        .Where(x => x.EmployeeId == request.EmployeeId
                                            && x.TrainingId == request.TrainingId)
                                        .SingleAsync(cancellationToken);

        if (entity != null)
        {
            var allEmployeeTrainings = await _context.EmployeeTrainings.Where(x => x.TrainingId == request.TrainingId).ToListAsync(cancellationToken);

            if (allEmployeeTrainings.Count - 1 > 0)
            {
                var count = allEmployeeTrainings.Count - 1;
                decimal directCost = entity.Training.DirectCost > 0 ? entity.Training.DirectCost / count : 0;
                decimal additionalCost = entity.Training.AdditionalCost > 0 ? entity.Training.AdditionalCost / count : 0;

                decimal accommodationCost = entity.AccommodationCost > 0 ? entity.AccommodationCost / count : 0;
                decimal carHireCost = entity.CarHireCost > 0 ? entity.CarHireCost / count : 0;
                decimal cateringCost = entity.CateringCost > 0 ? entity.CateringCost / count : 0;
                decimal flightsCost = entity.FlightsCost > 0 ? entity.FlightsCost / count : 0;
                decimal fuelCost = entity.FuelCost > 0 ? entity.FuelCost / count : 0;
                decimal stationeryCost = entity.StationeryCost > 0 ? entity.StationeryCost / count : 0;
                decimal venueCost = entity.VenueCost > 0 ? entity.VenueCost / count : 0;
                decimal otherCost = entity.OtherCost > 0 ? entity.OtherCost / count : 0;

                foreach (var empTraining in allEmployeeTrainings)
                {
                    empTraining.DirectCost = directCost;
                    empTraining.AdditionalCost = additionalCost;

                    empTraining.AccommodationCost = accommodationCost;
                    empTraining.CarHireCost = carHireCost;
                    empTraining.CateringCost = cateringCost;
                    empTraining.FlightsCost = flightsCost;
                    empTraining.FuelCost = fuelCost;
                    empTraining.StationeryCost = stationeryCost;
                    empTraining.VenueCost = venueCost;
                    empTraining.OtherCost = otherCost;
                }
            }
            _context.EmployeeTrainings.Remove(entity);
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            return opStatus;
        }

        throw new TrainingException("Cannot Delete While Value Is In Use");
    }
}
