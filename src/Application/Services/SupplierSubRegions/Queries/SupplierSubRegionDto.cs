namespace Engage.Application.Services.SupplierSubRegions.Queries;

public class SupplierSubRegionDto : IMapFrom<SupplierSubRegion>
{
    public int Id { get; set; }
    public int SupplierRegionId { get; set; }
    public string SupplierRegionName { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubRegion, SupplierSubRegionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierSubRegionId));
    }
}
