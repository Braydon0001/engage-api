namespace Engage.Application.Services.Warehouses.Models;

public class WarehouseDto : IMapFrom<Warehouse>
{
    public int Id { get; set; }
    public int DCId { get; set; }
    public string DCName { get; set; }
    public string Name { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Warehouse, WarehouseDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WarehouseId))
            .ForMember(d => d.DCName, opt => opt.MapFrom(s => s.DC.Name));
    }
}
