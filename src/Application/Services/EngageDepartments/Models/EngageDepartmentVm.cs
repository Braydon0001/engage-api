namespace Engage.Application.Services.EngageDepartments.Models;

public class EngageDepartmentVm : IMapFrom<EngageDepartment>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }
    public OptionDto EngageDepartmentGroupId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageDepartment, EngageDepartmentVm>()
            .ForMember(d => d.EngageDepartmentGroupId, opt => opt.MapFrom(s => new OptionDto(s.EngageDepartmentGroupId, s.EngageDepartmentGroup.Name)));
    }
}
