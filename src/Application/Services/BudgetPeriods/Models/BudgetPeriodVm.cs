using Engage.Application.Services.Budgets.Models;

namespace Engage.Application.Services.BudgetPeriods.Models;

public class BudgetPeriodVm : IMapFrom<BudgetPeriod>
{
    public int Id { get; set; }
    public OptionDto BudgetYearId { get; set; }
    public int No { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public IEnumerable<BudgetDto> Budgets { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BudgetPeriod, BudgetPeriodVm>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.BudgetPeriodId))
            .ForMember(e => e.BudgetYearId, opt => opt.MapFrom(d => new OptionDto(d.BudgetYearId, d.BudgetYear.Name)));
    }
}
