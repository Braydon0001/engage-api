namespace Engage.Application.Services.BudgetYears.Models;

public class BudgetYearVm : IMapFrom<BudgetYear>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<BudgetYear, BudgetYearVm>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.BudgetYearId));
    }
}
