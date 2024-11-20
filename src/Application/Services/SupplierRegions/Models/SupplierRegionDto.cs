using Engage.Application.Services.SupplierSubRegions.Queries;

namespace Engage.Application.Services.SupplierRegions.Models;

public class SupplierRegionDto : IMapFrom<SupplierRegion>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public bool Disabled { get; set; }
    public List<SupplierSubRegionDto> SupplierSubRegions { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierRegion, SupplierRegionDto>()
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Supplier.Name));
    }
}
