namespace Engage.Application.Services.SupplierRegions.Models;

public class SupplierRegionVm : IMapFrom<SupplierRegion>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public OptionDto SupplierId { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierRegion, SupplierRegionVm>()
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => new OptionDto(s.SupplierId, s.Supplier.Name)));
    }
}
