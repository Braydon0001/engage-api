namespace Engage.Application.Services.EmployeeTrainings.Commands;

public class CreateEmployeeTrainingsCommand : IRequest<OperationStatus>
{
    public int TrainingId { get; set; }
    public List<int> EmployeeIds { get; set; }
    public string Note { get; set; }
    public decimal DirectCost { get; set; }
    public decimal AdditionalCost { get; set; } = 0;

    public decimal AccommodationCost { get; set; } = 0;
    public decimal CarHireCost { get; set; } = 0;
    public decimal CateringCost { get; set; } = 0;
    public decimal FlightsCost { get; set; } = 0;
    public decimal FuelCost { get; set; } = 0;
    public decimal StationeryCost { get; set; } = 0;
    public decimal VenueCost { get; set; } = 0;
    public decimal OtherCost { get; set; } = 0;
}

public class CreateEmployeeTrainingsCommandHandler : IRequestHandler<CreateEmployeeTrainingsCommand, OperationStatus>
{
    private readonly IAppDbContext _context;


    public CreateEmployeeTrainingsCommandHandler(IAppDbContext context)
    {
        this._context = context;
    }

    public async Task<OperationStatus> Handle(CreateEmployeeTrainingsCommand request, CancellationToken cancellationToken)
    {
        var ids = await _context.EmployeeTrainings.IgnoreQueryFilters()
                                                             .Where(e => e.TrainingId == request.TrainingId &&
                                                                         request.EmployeeIds.Contains(e.EmployeeId))
                                                             .Select(e => e.EmployeeId)
                                                             .ToListAsync(cancellationToken);

        var newIds = request.EmployeeIds.Except(ids);

        foreach (var id in newIds)
        {
            _context.EmployeeTrainings.Add(new EmployeeTraining
            {
                TrainingId = request.TrainingId,
                EmployeeId = id,
                Note = request.Note,
                DirectCost = request.DirectCost,
                AdditionalCost = request.AdditionalCost,

                AccommodationCost = request.AccommodationCost,
                CarHireCost = request.CarHireCost,
                CateringCost = request.CateringCost,
                FlightsCost = request.FlightsCost,
                FuelCost = request.FuelCost,
                StationeryCost = request.StationeryCost,
                VenueCost = request.VenueCost,
                OtherCost = request.OtherCost,
            });
        }

        return await _context.SaveChangesAsync(cancellationToken);
    }
}

