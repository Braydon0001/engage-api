namespace Engage.Application.Services.ProjectUsers.Queries;

public class ProjectUserDto : IMapFrom<ProjectUser>
{
    public int ProjectId { get; init; }
    public string ProjectName { get; init; }
    public int UserId { get; init; }
    public string UserName { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectUser, ProjectUserDto>();
    }
}
