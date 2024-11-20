namespace Engage.Application.Services.Manufacturers.Models;

public class ManufacturerDto : IMapFrom<Manufacturer>
{
    public int Id { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile) {
        profile.CreateMap<Manufacturer, ManufacturerDto>()
             .ForMember(d => d.Id, opts => opts.MapFrom(s => s.ManufacturerId))
             .ForMember(d => d.SupplierName, opts => opts.MapFrom(s => s.Supplier.Name))   
             .ForMember(d => d.EngageRegionName, opts => opts.MapFrom(s => s.EngageRegion.Name));   
    }
}
