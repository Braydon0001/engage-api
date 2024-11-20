namespace Engage.Application.Services.ProjectTacOps.Queries;

public class ProjectTacOpDto : IMapFrom<ProjectTacOpDto>
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public string UserName { get; init; }
    public string MobilePhone { get; init; }
    public string EngageRegions { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTacOp, ProjectTacOpDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTacOpId))
               .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.User.FirstName + " " + s.User.LastName + " - " + s.User.Email))
               .ForMember(d => d.EngageRegions, opt => opt.MapFrom(s => string.Join(", ", s.ProjectTacOpRegions.Select(s => s.EngageRegion.Name))));
    }
}
