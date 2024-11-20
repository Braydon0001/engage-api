namespace Engage.Application.Services.TrainingPeriods.Queries;

public class TrainingPeriodOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int TrainingYearId { get; set; }
}

public class TrainingPeriodOptionsQueryHandler : IRequestHandler<TrainingPeriodOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public TrainingPeriodOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(TrainingPeriodOptionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.TrainingPeriods.Where(e => e.TrainingYearId == request.TrainingYearId &&
                                                                  e.Disabled == false)
                                                   .OrderBy(e => e.Number)
                                                   .ToListAsync(cancellationToken);

        return entities.Select(e => new OptionDto(e.TrainingPeriodId, GetPeriodDisplay(e.Name, e.StartDate, e.EndDate)))
                       .ToList();
    }

    public string GetPeriodDisplay(string name, DateTime startDate, DateTime endDate)
    {
        return name + " (" + startDate.ToString("MMM") + " " + startDate.Day + " - " + endDate.ToString("MMM") + " " + endDate.Day + ")";
    }
}
