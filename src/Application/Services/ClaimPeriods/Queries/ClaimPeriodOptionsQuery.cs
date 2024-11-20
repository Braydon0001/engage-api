namespace Engage.Application.Services.ClaimPeriods.Queries;

public class ClaimPeriodOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int? ClaimYearId { get; set; }
}

public class ClaimPeriodOptionsQueryHandler : IRequestHandler<ClaimPeriodOptionsQuery, List<OptionDto>>
{
    private readonly IAppDbContext _context;

    public ClaimPeriodOptionsQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<List<OptionDto>> Handle(ClaimPeriodOptionsQuery query, CancellationToken cancellationToken)
    {
        var queryable = _context.ClaimPeriods.AsQueryable().AsNoTracking();

        if (query.ClaimYearId.HasValue)
        {
            queryable = queryable.Where(e => e.ClaimYearId == query.ClaimYearId);
        }

        var entities = await queryable.Where(e => e.ClaimYearId == query.ClaimYearId)
                                      .OrderBy(e => e.Number)
                                      .ToListAsync(cancellationToken);

        return entities.Select(e => new OptionDto(e.ClaimPeriodId, GetPeriodDisplay(e.Name, e.StartDate, e.EndDate)))
                       .ToList();
    }

    public string GetPeriodDisplay(string name, DateTime startDate, DateTime endDate)
    {
        return name + " (" + startDate.ToString("MMM") + " " + startDate.Day + " - " + endDate.ToString("MMM") + " " + endDate.Day + ")";
    }
}
