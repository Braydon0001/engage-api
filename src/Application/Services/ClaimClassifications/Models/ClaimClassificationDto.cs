namespace Engage.Application.Services.ClaimClassifications.Models;

public class ClaimClassificationDto : IMapFrom<ClaimClassification>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsPayStore { get; set; }
    public bool EditIsPayStore { get; set; }
    public bool IsClaimFromSupplier { get; set; }
    public bool EditIsClaimFromSupplier { get; set; }
    public bool IsDairy { get; set; }
    public bool IsSupplierProcess { get; set; }
    public int? ClaimTypeId { get; set; }
    public string ClaimTypeName { get; set; }
    public int? SupplierId { get; set; }
    public string SupplierName { get; set; }
    public bool Disabled { get; set; }
    public bool IsAttachmentRequiredOnSubmit { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimClassification, ClaimClassificationDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimClassificationId))
            .ForMember(d => d.ClaimTypeName, opt => opt.MapFrom(s => s.ClaimTypeId.HasValue ? s.ClaimType.Name : string.Empty))
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.SupplierId.HasValue ? s.Supplier.Name : string.Empty));
    }
}
