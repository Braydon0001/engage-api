namespace Engage.Application.Services.ProjectExternalUsers.Queries;

public class ProjectExternalUserOption : IMapFrom<ProjectExternalUser>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectExternalUser, ProjectExternalUserOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectExternalUserId));
    }
}