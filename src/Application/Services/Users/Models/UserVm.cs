using Engage.Application.Services.CommunicationTypes.Queries;
using Engage.Application.Services.EngageSubGroups.Models;

namespace Engage.Application.Services.Users.Models;

public class UserVm : IMapFrom<User>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string MobilePhone { get; set; }
    public List<JsonSetting> Settings { get; set; }
    public bool IgnoreOrderProductFilters { get; set; }
    public OptionDto SupplierId { get; set; }
    public List<OptionDto> Groups { get; set; }
    public List<OptionDto> EngageGroupIds { get; set; }
    public ICollection<EngageSubGroupOption> EngageSubGroupIds { get; set; }
    public OptionDto RoleId { get; set; }
    public ICollection<CommunicationTypeOption> CommunicationTypeIds { get; set; }
    public ICollection<OptionDto> EngageRegionIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserVm>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserId))
           .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => new OptionDto(s.SupplierId, s.Supplier.Name)))
           .ForMember(d => d.EngageSubGroupIds, opt => opt.MapFrom(s => s.UserEngageSubGroups.Select(o => new EngageSubGroupOption { Id = o.EngageSubGroupId, Name = o.EngageSubGroup.Name, ParentId = o.EngageSubGroup.EngageGroupId })))
           .ForMember(d => d.RoleId, opt => opt.MapFrom(s => s.RoleId.HasValue ? new OptionDto(s.RoleId.Value, s.Role.Name) : null))
           .ForMember(d => d.CommunicationTypeIds, opt => opt.MapFrom(s => s.UserCommunicationTypes.Select(o => new CommunicationTypeOption { Id = o.CommunicationTypeId, Name = o.CommunicationType.Name })))
           .ForMember(d => d.EngageRegionIds, opt => opt.Ignore());
    }
}
