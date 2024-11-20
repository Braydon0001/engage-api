namespace Engage.Application.Services.ClaimYears.Models;

public class ClaimYearVm : IMapFrom<ClaimYear>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimYear, ClaimYearVm>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.ClaimYearId));
    }
}
