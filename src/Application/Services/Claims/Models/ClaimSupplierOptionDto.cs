namespace Engage.Application.Services.Claims.Models;

public class ClaimSupplierOptionDto : IMapFrom<Supplier>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsClaimAccountManager { get; set; }
    public bool IsClaimManager { get; set; }
    public bool IsClaimAccountManagerRequired { get; set; }
    public bool IsClaimFloatRequired { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Supplier, ClaimSupplierOptionDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierId));
    }
}
