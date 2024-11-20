namespace Engage.Application.Services.Vendors.Commands;

public class VendorCommand : IMapTo<Vendor>
{
    public int DistributionCenterId { get; set; }
    public int SupplierId { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VendorCommand, Vendor>();
    }
}
