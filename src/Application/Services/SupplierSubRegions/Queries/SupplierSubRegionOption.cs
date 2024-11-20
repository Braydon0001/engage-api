// auto-generated
namespace Engage.Application.Services.SupplierSubRegions.Queries;

public class SupplierSubRegionOption : IMapFrom<SupplierSubRegion>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubRegion, SupplierSubRegionOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierSubRegionId));
    }
}