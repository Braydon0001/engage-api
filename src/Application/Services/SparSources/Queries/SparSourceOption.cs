namespace Engage.Application.Services.SparSources.Queries;

public class SparSourceOption : IMapFrom<SparSource>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSource, SparSourceOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparSourceId));
    }
}