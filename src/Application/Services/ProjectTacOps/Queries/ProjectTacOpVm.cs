namespace Engage.Application.Services.ProjectTacOps.Queries;

public class ProjectTacOpVm : IMapFrom<ProjectTacOp>
{
    public int Id { get; init; }
    public OptionDto UserId { get; init; }
    public string MobilePhone { get; init; }
    public ICollection<OptionDto> EngageRegionIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectTacOp, ProjectTacOpVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectTacOpId))
               .ForMember(d => d.UserId, opt => opt.MapFrom(s => new OptionDto(s.UserId, s.User.FirstName + " " + s.User.LastName + " - " + s.User.Email)))
               .ForMember(d => d.EngageRegionIds, opt => opt.MapFrom(s => s.ProjectTacOpRegions.Select(o => new OptionDto(o.EngageRegionId, o.EngageRegion.Name))));
    }
}
