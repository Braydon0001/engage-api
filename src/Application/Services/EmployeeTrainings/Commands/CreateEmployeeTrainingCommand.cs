namespace Engage.Application.Services.EmployeeTrainings.Commands;

public class CreateEmployeeTrainingCommand : IRequest<OperationStatus>
{
    public int TrainingId { get; set; }
    public List<int> EmployeeIds { get; set; }
}

public class CreateEmployeeTrainingCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeTrainingCommand, OperationStatus>
{
    public CreateEmployeeTrainingCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateEmployeeTrainingCommand command, CancellationToken cancellationToken)
    {
        var training = await _context.Trainings
                                        .Include(x => x.EmployeeTrainings)
                                        .Where(x => x.TrainingId == command.TrainingId)
                                        .FirstOrDefaultAsync(cancellationToken);

        if (training.EmployeeTrainings.Count() > 0)
        {
            var existingEmployeeIds = training.EmployeeTrainings.Select(x => x.EmployeeId).ToList();
            var idsToSave = existingEmployeeIds.Union(command.EmployeeIds).ToList();

            foreach (var empTraining in training.EmployeeTrainings)
            {
                var employeeTrainingRecord = await _context.EmployeeTrainings
                                                        .SingleAsync(x => x.TrainingId == empTraining.TrainingId && x.EmployeeId == empTraining.EmployeeId, cancellationToken);

                _context.EmployeeTrainings.Remove(employeeTrainingRecord);
            }

            //public decimal OtherCost { get; set; }

            var count = idsToSave.Count;
            decimal directCost = training.DirectCost > 0 ? training.DirectCost / count : 0;
            decimal additionalCost = training.AdditionalCost > 0 ? training.AdditionalCost / count : 0;

            decimal accommodationCost = training.AccommodationCost > 0 ? training.AccommodationCost / count : 0;
            decimal carHireCost = training.CarHireCost > 0 ? training.CarHireCost / count : 0;
            decimal cateringCost = training.CateringCost > 0 ? training.CateringCost / count : 0;
            decimal flightsCost = training.FlightsCost > 0 ? training.FlightsCost / count : 0;
            decimal fuelCost = training.FuelCost > 0 ? training.FuelCost / count : 0;
            decimal stationeryCost = training.StationeryCost > 0 ? training.StationeryCost / count : 0;
            decimal venueCost = training.VenueCost > 0 ? training.VenueCost / count : 0;
            decimal otherCost = training.OtherCost > 0 ? training.OtherCost / count : 0;

            training.NoOfParticipants = count;
            foreach (var id in idsToSave)
            {
                var employeeTraining = new EmployeeTraining
                {
                    AdditionalCost = additionalCost,
                    DirectCost = directCost,
                    Training = training,

                    AccommodationCost = accommodationCost,
                    CarHireCost = carHireCost,
                    CateringCost = cateringCost,
                    FlightsCost = flightsCost,
                    FuelCost = fuelCost,
                    StationeryCost = stationeryCost,
                    VenueCost = venueCost,
                    OtherCost = otherCost,

                    EmployeeId = id,
                };

                _context.EmployeeTrainings.Add(employeeTraining);
            }
        }
        else
        {
            var count = command.EmployeeIds.Count;
            decimal directCost = training.DirectCost > 0 ? training.DirectCost / count : 0;
            decimal additionalCost = training.AdditionalCost > 0 ? training.AdditionalCost / count : 0;

            decimal accommodationCost = training.AccommodationCost > 0 ? training.AccommodationCost / count : 0;
            decimal carHireCost = training.CarHireCost > 0 ? training.CarHireCost / count : 0;
            decimal cateringCost = training.CateringCost > 0 ? training.CateringCost / count : 0;
            decimal flightsCost = training.FlightsCost > 0 ? training.FlightsCost / count : 0;
            decimal fuelCost = training.FuelCost > 0 ? training.FuelCost / count : 0;
            decimal stationeryCost = training.StationeryCost > 0 ? training.StationeryCost / count : 0;
            decimal venueCost = training.VenueCost > 0 ? training.VenueCost / count : 0;
            decimal otherCost = training.OtherCost > 0 ? training.OtherCost / count : 0;

            training.NoOfParticipants = count;
            foreach (var id in command.EmployeeIds)
            {
                var employeeTraining = new EmployeeTraining
                {
                    AdditionalCost = additionalCost,
                    DirectCost = directCost,
                    Training = training,

                    AccommodationCost = accommodationCost,
                    CarHireCost = carHireCost,
                    CateringCost = cateringCost,
                    FlightsCost = flightsCost,
                    FuelCost = fuelCost,
                    StationeryCost = stationeryCost,
                    VenueCost = venueCost,
                    OtherCost = otherCost,

                    EmployeeId = id,
                };

                _context.EmployeeTrainings.Add(employeeTraining);
            }
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = training.TrainingId;
        return opStatus;
    }
}
