namespace Engage.Application.Services.ProjectTaskComments.Queries;

public class ProjectTaskCommentOption : IMapFrom<ProjectTaskComment>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTaskComment, ProjectTaskCommentOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTaskCommentId));
    }
}