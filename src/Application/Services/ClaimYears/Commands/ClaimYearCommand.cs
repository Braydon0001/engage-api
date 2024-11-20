namespace Engage.Application.Services.ClaimYears.Commands;

public class ClaimYearCommand : IMapTo<ClaimYear>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimYearCommand, ClaimYear>();
    }
}
