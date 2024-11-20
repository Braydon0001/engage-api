namespace Engage.Application.Services.CreditorStatuses.Queries;

public class CreditorStatusOption : IMapFrom<CreditorStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorStatus, CreditorStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorStatusId));
    }
}