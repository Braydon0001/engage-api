namespace Engage.Application.Services.SupplierSubRegions.Queries;

public class SupplierSubRegionVm : IMapFrom<SupplierSubRegion>
{
    public int Id { get; set; }
    public OptionDto SupplierRegionId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierSubRegion, SupplierSubRegionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierSubRegionId))
               .ForMember(d => d.SupplierRegionId, opt => opt.MapFrom(s => new OptionDto(s.SupplierRegionId, s.SupplierRegion.Name)));
    }
}
