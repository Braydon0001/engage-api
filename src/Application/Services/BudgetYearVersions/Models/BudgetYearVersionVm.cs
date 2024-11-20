namespace Engage.Application.Services.BudgetYearVersions.Models;

public class BudgetYearVersionVm : IMapFrom<BudgetYearVersion>
{
    public OptionDto BudgetYearId { get; set; }
    public OptionDto BudgetVersionId { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BudgetYearVersion, BudgetYearVersionVm>()
            .ForMember(e => e.BudgetYearId, opt => opt.MapFrom(d => new OptionDto(d.BudgetYearId, d.BudgetYear.Name)))
            .ForMember(e => e.BudgetVersionId, opt => opt.MapFrom(d => new OptionDto(d.BudgetVersionId, d.BudgetVersion.Name)));
    }
}
