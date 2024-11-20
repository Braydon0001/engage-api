namespace Engage.Application.Services.BudgetPeriods.Queries;

public class BudgetPeriodOptionsQuery : GetQuery, IRequest<List<OptionDto>>
{
    public int? BudgetYearId { get; set; }
}

public class BudgetPeriodOptionsHandler : BaseQueryHandler, IRequestHandler<BudgetPeriodOptionsQuery, List<OptionDto>>
{
    public BudgetPeriodOptionsHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<List<OptionDto>> Handle(BudgetPeriodOptionsQuery request, CancellationToken cancellationToken)
    {
        var queryable = _context.BudgetPeriods.AsQueryable();

        if (request.BudgetYearId.HasValue)
        {
            queryable = queryable.Where(e => e.BudgetYearId == request.BudgetYearId);
        }

        var entities = await queryable.OrderBy(e => e.BudgetYear)
                                      .ThenBy(e => e.No)
                                      .Select(e => new OptionDto(e.BudgetPeriodId, e.Name))
                                      .ToListAsync(cancellationToken);

        return new List<OptionDto>(entities);
    }
}


