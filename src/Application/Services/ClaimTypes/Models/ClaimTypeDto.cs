namespace Engage.Application.Services.ClaimTypes.Models;

public class ClaimTypeDto : IMapFrom<ClaimType>
{
    public int Id { get; set; }
    public int VatId { get; set; }
    public string VatCode { get; set; }
    public string VatName { get; set; }
    public string Name { get; set; }
    public bool IsVatInclusive { get; set; }
    public bool IsDairy { get; set; }
    public bool Disabled { get; set; }
    public bool IsEmployeeClaim { get; set; }
    public int? SupplierId { get; set; }
    public string SupplierName { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimType, ClaimTypeDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimTypeId))
            .ForMember(d => d.VatCode, opt => opt.MapFrom(s => s.Vat.Code))
            .ForMember(d => d.VatName, opt => opt.MapFrom(s => s.Vat.Name))
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.SupplierId.HasValue ? s.Supplier.Name : string.Empty));
    }
}
