
using Engage.Application.Services.Suppliers.Queries;

namespace Engage.Application.Services.UserOrganizations.Queries;

public class UserOrganizationVm : IMapFrom<UserOrganization>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public SupplierOption SupplierId { get; init; }
    public string ThemeName { get; init; }
    public string ThemeColor { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserOrganization, UserOrganizationVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserOrganizationId))
               .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Supplier));
    }
}
