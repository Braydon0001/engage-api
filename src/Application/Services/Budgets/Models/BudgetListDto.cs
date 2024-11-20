namespace Engage.Application.Services.Budgets.Models;

public class BudgetListDto : IMapFrom<Budget>
{
    public int Id { get; set; }
    public int GLAccountId { get; set; }
    public int BudgetTypeId { get; set; }
    public int BudgetYearId { get; set; }
    public int BudgetVersionId { get; set; }
    public int BudgetPeriodId { get; set; }
    public double Value { get; set; }
    public string GLAccountCode { get; set; }
    public string GLAccountName { get; set; }
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public string BudgetTypeName { get; set; }
    public string BudgetYearName { get; set; }
    public string BudgetVersionName { get; set; }
    public int BudgetPeriodNo { get; set; }
    public string BudgetPeriodName { get; set; }
    public DateTime BudgetPeriodStartDate { get; set; }
    public DateTime BudgetPeriodEndDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Budget, BudgetListDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.BudgetId))
            .ForMember(e => e.GLAccountCode, opt => opt.MapFrom(d => d.GLAccount.Code))
            .ForMember(e => e.GLAccountName, opt => opt.MapFrom(d => d.GLAccount.Name))
            .ForMember(e => e.EngageRegionId, opt => opt.MapFrom(d => d.GLAccount.EngageRegionId))
            .ForMember(e => e.EngageRegionName, opt => opt.MapFrom(d => d.GLAccount.EngageRegion.Name))
            .ForMember(e => e.BudgetTypeName, opt => opt.MapFrom(d => d.BudgetType.Name))
            .ForMember(e => e.BudgetYearName, opt => opt.MapFrom(d => d.BudgetYear.Name))
            .ForMember(e => e.BudgetVersionName, opt => opt.MapFrom(d => d.BudgetVersion.Name))
            .ForMember(e => e.BudgetPeriodNo, opt => opt.MapFrom(d => d.BudgetPeriod.No))
            .ForMember(e => e.BudgetPeriodName, opt => opt.MapFrom(d => d.BudgetPeriod.Name))
            .ForMember(e => e.BudgetPeriodStartDate, opt => opt.MapFrom(d => d.BudgetPeriod.StartDate))
            .ForMember(e => e.BudgetPeriodEndDate, opt => opt.MapFrom(d => d.BudgetPeriod.EndDate));
    }
}
