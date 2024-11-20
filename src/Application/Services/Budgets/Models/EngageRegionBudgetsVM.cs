namespace Engage.Application.Services.Budgets.Models;

public record BudgetIdValue(int Id, double Value);

public record EngageRegionBudget(string EngageRegionName, double EngageRegionSum, Dictionary<int, BudgetIdValue> Periods);

public record Period(int PeriodNo, string PeriodName);

public record EngageRegionBudgetsVm(List<EngageRegionBudget> Budgets, List<Period> Periods);
