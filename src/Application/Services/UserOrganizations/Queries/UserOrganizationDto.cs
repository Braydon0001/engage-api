namespace Engage.Application.Services.UserOrganizations.Queries;

public class UserOrganizationDto : IMapFrom<UserOrganization>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int SupplierId { get; init; }
    public string SupplierName { get; init; }
    public string ThemeName { get; init; }
    public string ThemeColor { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserOrganization, UserOrganizationDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserOrganizationId));
    }
}
