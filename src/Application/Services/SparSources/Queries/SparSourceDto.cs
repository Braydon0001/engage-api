namespace Engage.Application.Services.SparSources.Queries;

public class SparSourceDto : IMapFrom<SparSource>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSource, SparSourceDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SparSourceId));
    }
}
