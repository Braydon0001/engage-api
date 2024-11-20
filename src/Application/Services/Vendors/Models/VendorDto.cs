namespace Engage.Application.Services.Vendors.Models;

public class VendorDto : IMapFrom<Vendor>
{
    public int Id { get; set; }    
    public int DistributionCenterId { get; set; }
    public string DistributionCenterName { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Vendor, VendorDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(d => d.VendorId))
            .ForMember(e => e.SupplierName, opt => opt.MapFrom(d => d.Supplier.Name))
            .ForMember(e => e.DistributionCenterName, opt => opt.MapFrom(d => d.DistributionCenter.Name));
    }
}
