namespace Engage.Application.Services.Manufacturers.Models;

public class ManufacturerVm : IMapFrom<Manufacturer>
{
    public int Id { get; set; }
    public OptionDto SupplierId { get; set; }
    public OptionDto EngageRegionId { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Manufacturer, ManufacturerVm>()
             .ForMember(d => d.Id, opts => opts.MapFrom(s => s.ManufacturerId))
             .ForMember(d => d.SupplierId, opts => opts.MapFrom(s => new OptionDto(s.SupplierId, s.Supplier.Name)))
             .ForMember(d => d.EngageRegionId, opts => opts.MapFrom(s => new OptionDto(s.EngageRegionId, s.EngageRegion.Name)));
    }
}
