namespace Engage.Application.Services.Vendors.Models;

public class VendorOptionDto : IMapFrom<Vendor>
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Vendor, VendorOptionDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VendorId))
            .ForMember(d => d.ParentId, opt => opt.MapFrom(s => s.SupplierId));
    }
}
