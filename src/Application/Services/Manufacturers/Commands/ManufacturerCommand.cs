namespace Engage.Application.Services.Manufacturers.Commands;

public class ManufacturerCommand : IMapTo<Manufacturer>
{
    public int EngageRegionId { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ManufacturerCommand, Manufacturer>();
    }
}