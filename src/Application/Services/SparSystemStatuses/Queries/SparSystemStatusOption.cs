namespace Engage.Application.Services.SparSystemStatuses.Queries;

public class SparSystemStatusOption : IMapFrom<SparSystemStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSystemStatus, SparSystemStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparSystemStatusId));
    }
}