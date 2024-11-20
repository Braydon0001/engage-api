namespace Engage.Application.Services.Mobile.ProjectStores.Queries;

using Engage.Application.Services.Projects.Queries;
using Engage.Application.Services.ProjectTasks.Queries;
using Engage.Domain.Entities;
public class ProjectWithTasks : ProjectDto, IMapFrom<ProjectTask>
{
    public List<ProjectTaskDto> ProjectTasks { get; set; }
    public List<ProjectProjectTagMobileDto> StoreAssetTagIds { get; set; }
    public List<ProjectProjectTagMobileDto> DcProductTagIds { get; set; }
    public List<OptionDto> ProjectUsersTags { get; set; }

    public new void Mapping(Profile profile)
    {
        profile.CreateMap<Project, ProjectWithTasks>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectId))
               //.ForMember(d => d.ProjectUsers, opt => opt.MapFrom(s => string.Join(", ", s.ProjectUsers.Select(s => s.User.Email))))
               .ForMember(d => d.ProjectTasks, opt => opt.MapFrom(s => s.ProjectTasks));

        profile.CreateMap<ProjectStore, ProjectWithTasks>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectId))
               //.ForMember(d => d.ProjectUsers, opt => opt.MapFrom(s => string.Join(", ", s.ProjectUsers.Select(s => s.User.Email))))
               .ForMember(d => d.ProjectTasks, opt => opt.MapFrom(s => s.ProjectTasks));

    }
}