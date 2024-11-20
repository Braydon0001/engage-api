
using Engage.Application.Services.Projects.Queries;

namespace Engage.Application.Services.ProjectEngageBrands.Queries;

public class ProjectEngageBrandVm : IMapFrom<ProjectEngageBrand>
{
    public int Id { get; init; }
    public ProjectOption ProjectId { get; init; }
    //public EngageBrandOption EngageBrandId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectEngageBrand, ProjectEngageBrandVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectEngageBrandId))
               .ForMember(d => d.ProjectId, opt => opt.MapFrom(s => s.Project));
        //.ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.EngageBrand));
    }
}
