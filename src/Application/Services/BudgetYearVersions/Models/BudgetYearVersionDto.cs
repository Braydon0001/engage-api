namespace Engage.Application.Services.BudgetYearVersions.Models
{
    public class BudgetYearVersionDto : IMapFrom<BudgetYearVersion>
    {
        public int BudgetYearId { get; set; }
        public string BudgetYearName { get; set; }
        public int BudgetVersionId { get; set; }
        public string BudgetVersionName { get; set; }
        public bool Disabled { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<BudgetYearVersion, BudgetYearVersionDto>()
                .ForMember(e => e.BudgetYearName, opt => opt.MapFrom(d => d.BudgetYear.Name))
                .ForMember(e => e.BudgetVersionName, opt => opt.MapFrom(d => d.BudgetVersion.Name));
        }
    }
}
