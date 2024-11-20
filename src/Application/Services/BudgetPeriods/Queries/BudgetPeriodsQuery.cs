using Engage.Application.Services.BudgetPeriods.Models;

namespace Engage.Application.Services.BudgetPeriods.Queries;

public class BudgetPeriodsQuery : GetQuery, IRequest<ListResult<BudgetPeriodDto>>
{
    public int? BudgetYearId { get; set; }
}

public class BudgetPeriodsHandler : BaseQueryHandler, IRequestHandler<BudgetPeriodsQuery, ListResult<BudgetPeriodDto>>
{
    public BudgetPeriodsHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<ListResult<BudgetPeriodDto>> Handle(BudgetPeriodsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.BudgetPeriods.AsQueryable();

        if (request.BudgetYearId.HasValue)
        {
            query = query.Where(e => e.BudgetYearId == request.BudgetYearId);
        }

        var entities = await _context.BudgetPeriods.OrderBy(e => e.BudgetYear)
                                                   .ThenBy(e => e.No)
                                                   .ProjectTo<BudgetPeriodDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);

        return new ListResult<BudgetPeriodDto>(entities);
    }
}
