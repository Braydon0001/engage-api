namespace Engage.Application.Services.BudgetYears.Models
{
    public class BudgetYearDto : IMapFrom<BudgetYear>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<BudgetYear, BudgetYearDto>()
                .ForMember(e => e.Id, opt => opt.MapFrom(d => d.BudgetYearId));
        }
    }
}
