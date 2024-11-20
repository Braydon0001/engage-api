namespace Engage.Application.Services.Claims.Models;

public class ClaimDto : IMapFrom<Claim>
{
    public int Id { get; set; }
    public int ClientTypeId { get; set; }
    public string ClientTypeName { get; set; }
    public int ClaimTypeId { get; set; }
    public string ClaimTypeName { get; set; }
    public int ClaimClassificationId { get; set; }
    public string ClaimClassificationName { get; set; }
    public int VatId { get; set; }
    public string VatCodeName { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public string EngageRegionName { get; set; }
    public int? ClaimAccountManagerId { get; set; }
    public string ClaimAccountManagerName { get; set; }
    public int? ClaimManagerId { get; set; }
    public string ClaimManagerName { get; set; }
    //public int? ClaimFloatId { get; set; }
    //public string ClaimFloatTitle { get; set; }
    public int ClaimStatusId { get; set; }
    public string ClaimStatusName { get; set; }
    public int ClaimSupplierStatusId { get; set; }
    public string ClaimSupplierStatusName { get; set; }
    public string ClaimNumber { get; set; }
    public bool IsPayStore { get; set; }
    public bool IsClaimFromSupplier { get; set; }
    public bool IsVatInclusive { get; set; }
    public bool IsDairy { get; set; }
    public DateTime ClaimDate { get; set; }
    public string ClaimReference { get; set; }
    public string Comment { get; set; }
    public DateTime? ApprovedDate { get; set; }
    public string ApprovedBy { get; set; }
    public int? ClaimRejectedReasonId { get; set; }
    public DateTime? RejectedDate { get; set; }
    public string RejectedBy { get; set; }
    public string RejectedReason { get; set; }
    public DateTime? PendingDate { get; set; }
    public string PendingBy { get; set; }
    public int? ClaimPendingReasonId { get; set; }
    public string PendingReason { get; set; }
    public DateTime? PaidDate { get; set; }
    public string PaidBy { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public int? UserId { get; set; }
    public List<EntityBlobDto> Blobs { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Claim, ClaimDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimId))
            .ForMember(d => d.EngageRegionName, opt => opt.MapFrom(s => s.Store.EngageRegion.Name))
            .ForMember(d => d.ClaimAccountManagerName, opt => opt.MapFrom(s => s.ClaimAccountManagerId.HasValue ? s.ClaimAccountManager.FullName : ""))
            .ForMember(d => d.ClaimManagerName, opt => opt.MapFrom(s => s.ClaimManagerId.HasValue ? s.ClaimManager.FullName : ""))
            //.ForMember(d => d.ClaimFloatTitle, opt => opt.MapFrom(s => s.ClaimFloatId.HasValue ? s.ClaimFloat.Title : ""))
            .ForMember(d => d.ClaimSupplierStatusName, opt => opt.MapFrom(s => s.ClaimSupplierStatus.Name))
            .ForMember(d => d.VatCodeName, opt => opt.MapFrom(s => $"{s.Vat.Code} - {s.Vat.Name}"))
            .ForMember(d => d.Blobs, opt => opt.MapFrom(s => s.ClaimBlobs.OrderByDescending(e => e.EntityBlobId).Select(e => new EntityBlobDto(e))));
    }
}
public class ClaimSubTotalDto : ClaimDto, IMapFrom<Claim>
{
    public decimal AmountSubTotal { get; set; }
    public decimal VatAmountSubTotal { get; set; }
    public decimal TotalAmountSubTotal { get; set; }
    public new void Mapping(Profile profile)
    {
        profile.CreateMap<Claim, ClaimSubTotalDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimId))
            .ForMember(d => d.EngageRegionName, opt => opt.MapFrom(s => s.Store.EngageRegion.Name))
            .ForMember(d => d.ClaimAccountManagerName, opt => opt.MapFrom(s => s.ClaimAccountManagerId.HasValue ? s.ClaimAccountManager.FullName : ""))
            .ForMember(d => d.ClaimManagerName, opt => opt.MapFrom(s => s.ClaimManagerId.HasValue ? s.ClaimManager.FullName : ""))
            .ForMember(d => d.VatCodeName, opt => opt.MapFrom(s => $"{s.Vat.Code} - {s.Vat.Name}"))
            .ForMember(d => d.AmountSubTotal, opt => opt.MapFrom(s => s.ClaimSkus.Where(e => e.Deleted == false).Sum(e => e.Amount)))
            .ForMember(d => d.VatAmountSubTotal, opt => opt.MapFrom(s => s.ClaimSkus.Where(e => e.Deleted == false).Sum(e => e.VatAmount)))
            .ForMember(d => d.TotalAmountSubTotal, opt => opt.MapFrom(s => s.ClaimSkus.Where(e => e.Deleted == false).Sum(e => e.TotalAmount)))
            .ForMember(d => d.Blobs, opt => opt.MapFrom(s => s.ClaimBlobs.OrderByDescending(e => e.EntityBlobId).Select(e => new EntityBlobDto(e))));
    }
}
