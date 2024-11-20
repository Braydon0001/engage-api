using Engage.Application.Services.Claims.Models;

namespace Engage.Application.Services.ClaimTypes.Models;

public class ClaimTypeVm : IMapFrom<ClaimType>
{
    public int Id { get; set; }
    public OptionDto VatId { get; set; }
    public string Name { get; set; }
    public bool IsVatInclusive { get; set; }
    public bool IsDairy { get; set; }
    public bool Disabled { get; set; }
    public bool IsEmployeeClaim { get; set; }
    public ClaimSupplierOptionDto SupplierId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimType, ClaimTypeVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimTypeId))
            .ForMember(d => d.VatId, opt => opt.MapFrom(s => new OptionDto(s.VatId, s.Vat.Name)))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.SupplierId.HasValue ? new ClaimSupplierOptionDto
            {
                Id = s.SupplierId.Value,
                Name = s.Supplier.Name,
                IsClaimAccountManager = s.SupplierId.HasValue && s.Supplier.IsClaimAccountManager,
                IsClaimManager = s.SupplierId.HasValue && s.Supplier.IsClaimManager,
                IsClaimAccountManagerRequired = s.SupplierId.HasValue && s.Supplier.IsClaimAccountManagerRequired,
                IsClaimFloatRequired = s.SupplierId.HasValue && s.Supplier.IsClaimFloatRequired,
            } : null));
    }
}
