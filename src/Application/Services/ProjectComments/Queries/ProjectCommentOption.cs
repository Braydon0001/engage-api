namespace Engage.Application.Services.ProjectComments.Queries;

public class ProjectCommentOption : IMapFrom<ProjectComment>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectComment, ProjectCommentOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectCommentId));
    }
}