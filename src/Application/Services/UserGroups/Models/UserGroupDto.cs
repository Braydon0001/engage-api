namespace Engage.Application.Services.UserGroups.Models;
using Engage.Application.Services.Users.Models;

public class UserGroupDto : IMapFrom<UserGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string VendorId { get; set; }
    public List<UserDto> Users { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserGroup, UserGroupDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserGroupId))
            .ForMember(d => d.Users, opt => opt.MapFrom(s => s.Users
                .Select(e => e.User)
                .Select(e =>
                    new UserDto {
                        UserGroupId = s.UserGroupId,
                        Id = e.UserId,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        SupplierName = e.Supplier.Name,
                        Email = e.Email
                    }))) ;
    }
}
