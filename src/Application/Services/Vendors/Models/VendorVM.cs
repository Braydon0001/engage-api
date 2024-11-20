namespace Engage.Application.Services.Vendors.Models;

public class VendorVm : IMapFrom<Vendor>
{
    public int Id { get; set; }
    public OptionDto DistributionCenterId { get; set; }
    public OptionDto SupplierId { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Vendor, VendorVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VendorId))
            .ForMember(d => d.DistributionCenterId, opt => opt.MapFrom(s => new OptionDto(s.DistributionCenterId, s.DistributionCenter.Name)))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => new OptionDto(s.SupplierId, s.Supplier.Name)));
    }
}
