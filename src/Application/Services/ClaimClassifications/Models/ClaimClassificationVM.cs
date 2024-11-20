using Engage.Application.Services.Claims.Models;
using Engage.Application.Services.ClaimTypes.Models;

namespace Engage.Application.Services.ClaimClassifications.Models;

public class ClaimClassificationVm : IMapFrom<ClaimClassification>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsPayStore { get; set; }
    public bool EditIsPayStore { get; set; }
    public bool IsClaimFromSupplier { get; set; }
    public bool EditIsClaimFromSupplier { get; set; }
    public bool IsDairy { get; set; }
    public bool IsSupplierProcess { get; set; }
    public ClaimTypeVm ClaimTypeId { get; set; }
    public bool IsVatInclusive { get; set; }
    public OptionDto VatId { get; set; }
    public ClaimSupplierOptionDto SupplierId { get; set; }
    public bool Disabled { get; set; }
    public bool IsAttachmentRequiredOnSubmit { get; set; }
    public ICollection<OptionDto> ClaimTypeIds { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimClassification, ClaimClassificationVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimClassificationId))
            .ForMember(d => d.ClaimTypeId, opt => opt.MapFrom(s => s.ClaimTypeId.HasValue ? new ClaimTypeVm
            {
                Id = s.ClaimTypeId.Value,
                Name = s.ClaimType.Name,
                IsDairy = s.ClaimType.IsDairy,
                IsVatInclusive = s.ClaimType.IsVatInclusive,
                VatId = new OptionDto(s.ClaimType.VatId, s.ClaimType.Vat.Name)
            } : null))
            .ForMember(d => d.IsVatInclusive, opt => opt.MapFrom(s => s.ClaimTypeId.HasValue && s.ClaimType.IsVatInclusive))
            .ForMember(d => d.VatId, opt => opt.MapFrom(s => s.ClaimTypeId.HasValue ? new OptionDto(s.ClaimType.VatId, s.ClaimType.Vat.Name) : null))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.SupplierId.HasValue ? new ClaimSupplierOptionDto
            {
                Id = s.SupplierId.Value,
                Name = s.Supplier.Name,
                IsClaimAccountManager = s.SupplierId.HasValue && s.Supplier.IsClaimAccountManager,
                IsClaimManager = s.SupplierId.HasValue && s.Supplier.IsClaimManager,
                IsClaimAccountManagerRequired = s.SupplierId.HasValue && s.Supplier.IsClaimAccountManagerRequired,
                IsClaimFloatRequired = s.SupplierId.HasValue && s.Supplier.IsClaimFloatRequired,
            } : null))
            .ForMember(d => d.ClaimTypeIds, opt => opt.MapFrom(s => s.ClaimClassificationTypes.Select(o => new OptionDto(o.ClaimTypeId, o.ClaimType.Name))));
    }
}
