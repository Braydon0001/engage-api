namespace Engage.Application.Services.BudgetPeriods.Models;

public class BudgetPeriodDto : IMapFrom<BudgetPeriod>
{
    public int Id { get; set; }
    public int BudgetYearId { get; set; }
    public string BudgetYearName { get; set; }
    public int No { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BudgetPeriod, BudgetPeriodDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.BudgetPeriodId))
            .ForMember(d => d.BudgetYearName, opt => opt.MapFrom(s => s.BudgetYear.Name));
    }
}
