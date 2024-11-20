namespace Engage.Application.Services.EmployeeJobTitleUserGroups.Models;

public class EmployeeJobTitleUserGroupVm : IMapFrom<EmployeeJobTitleUserGroup>
{
    public int Id { get; set; }
    public OptionDto EmployeeJobTitleIdId { get; set; }
    public OptionDto UserGroupId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeJobTitleUserGroup, EmployeeJobTitleUserGroupVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeJobTitleUserGroupId))
            .ForMember(d => d.EmployeeJobTitleIdId, opt => opt.MapFrom(s => new OptionDto(s.EmployeeJobTitleId, s.EmployeeJobTitle.Name)))
            .ForMember(d => d.UserGroupId, opt => opt.MapFrom(s => new OptionDto(s.UserGroupId, s.UserGroup.Name)));
    }
}
