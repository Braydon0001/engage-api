using Engage.Application.Services.Budgets.Models;

namespace Engage.Application.Services.Budgets.Queries;

public class EngageRegionBudgetsQuery : IRequest<EngageRegionBudgetsVm>
{
    public int BudgetTypeId { get; set; }
    public int BudgetVersionId { get; set; }
    public int BudgetYearId { get; set; }
    public int? BudgetYearFromId { get; set; }
    public int? BudgetYearToId { get; set; }
    public int? BudgetPeriodFromId { get; set; }
    public int? BudgetPeriodToId { get; set; }
}

public record EngageRegionBudgetsQueryHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<EngageRegionBudgetsQuery, EngageRegionBudgetsVm>
{
    public async Task<EngageRegionBudgetsVm> Handle(EngageRegionBudgetsQuery query, CancellationToken cancellationToken)
    {
        // TODO Get periods for YearId properties  

        // Budgets 
        var queryable = BudgetsQueryable(query);

        var budgets = await queryable.OrderBy(e => e.GLAccount.EngageRegion.Order)
                                     .ThenBy(e => e.BudgetPeriod.No)
                                     .ProjectTo<BudgetListDto>(Mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);

        var engageRegionNames = budgets.Select(e => e.EngageRegionName).Distinct();

        var engageRegionBudgets = new List<EngageRegionBudget>();
        foreach (var engageRegionName in engageRegionNames)
        {

            var regionBudgets = budgets.Where(e => e.EngageRegionName == engageRegionName)
                                       .Aggregate(new Dictionary<int, BudgetIdValue>(),
                                                  (budgetsDictionary, budget) =>
                                                  {
                                                      budgetsDictionary.Add(budget.BudgetPeriodNo, new BudgetIdValue(budget.Id, budget.Value));
                                                      return budgetsDictionary;
                                                  });

            var sum = budgets.Where(e => e.EngageRegionName == engageRegionName).Sum(e => e.Value);

            engageRegionBudgets.Add(new EngageRegionBudget(engageRegionName, sum, regionBudgets));
        };

        // Periods
        var periodsQueryable = PeriodsQueryable(query);

        var periods = await periodsQueryable.OrderBy(e => e.BudgetYear.Name)
                                            .ThenBy(e => e.No)
                                            .Select(e => new Period(e.No, e.Name))
                                            .ToListAsync(cancellationToken);

        return new EngageRegionBudgetsVm(engageRegionBudgets, periods);
    }


    private IQueryable<Budget> BudgetsQueryable(EngageRegionBudgetsQuery query)
    {
        var queryable = Context.Budgets.AsQueryable().AsNoTracking();

        if (query.BudgetTypeId > 0)
        {
            queryable = queryable.Where(e => e.BudgetTypeId == query.BudgetTypeId);
        }
        if (query.BudgetVersionId > 0)
        {
            queryable = queryable.Where(e => e.BudgetVersionId == query.BudgetVersionId);
        }
        if (query.BudgetYearId > 0)
        {
            queryable = queryable.Where(e => e.BudgetYearId == query.BudgetYearId);
        }
        else
        {
            if (query.BudgetYearFromId.HasValue)
            {
                queryable = queryable.Where(e => e.BudgetYearId >= query.BudgetYearFromId);
            }
            if (query.BudgetYearToId.HasValue)
            {
                queryable = queryable.Where(e => e.BudgetYearId <= query.BudgetYearToId);
            }
        }
        if (query.BudgetPeriodFromId.HasValue)
        {
            queryable = queryable.Where(e => e.BudgetPeriodId >= query.BudgetPeriodFromId);
        }
        if (query.BudgetPeriodToId.HasValue)
        {
            queryable = queryable.Where(e => e.BudgetPeriodId <= query.BudgetPeriodToId);
        }

        return queryable;
    }

    private IQueryable<BudgetPeriod> PeriodsQueryable(EngageRegionBudgetsQuery query)
    {
        var queryable = Context.BudgetPeriods.AsQueryable().AsNoTracking();

        if (query.BudgetYearId > 0)
        {
            queryable = queryable.Where(e => e.BudgetYearId == query.BudgetYearId);
        }
        else
        {
            if (query.BudgetYearFromId.HasValue)
            {
                queryable = queryable.Where(e => e.BudgetYearId >= query.BudgetYearFromId);
            }
            if (query.BudgetYearToId.HasValue)
            {
                queryable = queryable.Where(e => e.BudgetYearId <= query.BudgetYearToId);
            }
        }
        if (query.BudgetPeriodFromId.HasValue)
        {
            queryable = queryable.Where(e => e.BudgetPeriodId >= query.BudgetPeriodFromId);
        }
        if (query.BudgetPeriodToId.HasValue)
        {
            queryable = queryable.Where(e => e.BudgetPeriodId <= query.BudgetPeriodToId);
        }

        return queryable;
    }
}


