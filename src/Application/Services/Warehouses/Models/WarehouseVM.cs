namespace Engage.Application.Services.Warehouses.Models
{
    public class WarehouseVm : IMapFrom<Warehouse>
    {
        public int Id { get; set; }
        public OptionDto DCId { get; set; }
        public string Name { get; set; }
        public bool Disabled { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Warehouse, WarehouseVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WarehouseId))
                .ForMember(d => d.DCId, opt => opt.MapFrom(s => new OptionDto(s.WarehouseId, s.DC.Name)));
        }
    }
}
