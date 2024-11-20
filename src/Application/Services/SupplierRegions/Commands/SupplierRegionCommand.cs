namespace Engage.Application.Services.SupplierRegions.Commands;

public class SupplierRegionCommand : IMapTo<SupplierRegion>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int SupplierId { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierRegionCommand, SupplierRegion>();
    }
}
