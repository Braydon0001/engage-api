namespace Engage.Application.Services.ProjectEngageBrands.Queries;

public class ProjectEngageBrandDto : IMapFrom<ProjectEngageBrand>
{
    public int Id { get; init; }
    public int ProjectId { get; init; }
    public int EngageBrandId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectEngageBrand, ProjectEngageBrandDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectEngageBrandId));
    }
}
