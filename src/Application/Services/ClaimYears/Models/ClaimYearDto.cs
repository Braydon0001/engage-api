namespace Engage.Application.Services.ClaimYears.Models;

public class ClaimYearDto : IMapFrom<ClaimYear>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimYear, ClaimYearDto>()
            .ForMember(e => e.Id, opt => opt.MapFrom(d => d.ClaimYearId));
    }
}
