namespace Engage.Application.Services.Users.Models;

public class UserDto : IMapFrom<User>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string MobilePhone { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public List<JsonSetting> Settings { get; set; }
    public bool Disabled { get; set; }
    public bool IgnoreOrderProductFilters { get; set; }
    public int UserGroupId { get; set; }
    public string RoleName { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserDto>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserId));
    }
}
