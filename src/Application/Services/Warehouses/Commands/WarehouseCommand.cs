namespace Engage.Application.Services.Warehouses.Commands
{
    public class WarehouseCommand : IMapTo<Warehouse>
    {
        public string Name { get; set; }
        public int DCId { get; set; } 

        public void Mapping(Profile profile)
        {
            profile.CreateMap<WarehouseCommand, Warehouse>();
        }
    }
}
