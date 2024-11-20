namespace Engage.Application.Services.Users.Models;

public class UserClerkVm : IMapFrom<User>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string MobilePhone { get; set; }
    public List<JsonSetting> Settings { get; set; }
    public bool IgnoreOrderProductFilterers { get; set; }
    public OptionDto SupplierId { get; set; }
    public string ExternalId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserClerkVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserId))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => new OptionDto(s.SupplierId, s.Supplier.Name)));
    }
}
