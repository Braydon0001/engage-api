using Engage.Application.Services.ClaimClassifications.Models;
using Engage.Application.Services.Claims.Rules.Models;
using Engage.Application.Services.ClaimTypes.Models;

namespace Engage.Application.Services.Claims.Models;

public class ClaimVm : IMapFrom<Claim>
{
    public int Id { get; set; }
    public ClaimClassificationVm ClaimClassificationId { get; set; }
    public ClaimTypeVm ClaimTypeId { get; set; }
    public ClaimSupplierOptionDto SupplierId { get; set; }
    public ClaimStoreOptionDto StoreId { get; set; }
    public OptionDto ClientTypeId { get; set; }
    public OptionDto DistributionCenterId { get; set; }
    public OptionDto ClaimAccountManagerId { get; set; }
    public OptionDto ClaimManagerId { get; set; }
    public OptionDto ClaimFloatId { get; set; }
    public OptionDto ClaimStatusId { get; set; }
    public OptionDto ClaimSupplierStatusId { get; set; }
    public OptionDto VatId { get; set; }
    public OptionDto EmployeeDivisionId { get; set; }
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
    public string PendingReason { get; set; }
    public int? ClaimPendingReasonId { get; set; }
    public DateTime? PaidDate { get; set; }
    public string PaidBy { get; set; }
    public string UpdateMessage { get; set; }
    public CanUpdateClaimRuleResult CanUpdate { get; set; }
    public bool CanUpdateStatus { get; set; }
    public string CannotUpdateStatusText { get; set; }
    public List<EntityBlobDto> Blobs { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Claim, ClaimVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimId))
            .ForMember(d => d.ClaimClassificationId, opt => opt.Ignore())
            .ForMember(d => d.ClaimTypeId, opt => opt.Ignore())
            .ForMember(d => d.SupplierId, opt => opt.Ignore())
            .ForMember(d => d.StoreId, opt => opt.Ignore())
            .ForMember(d => d.ClientTypeId, opt => opt.MapFrom(s => new OptionDto(s.ClientTypeId, s.ClientType.Name)))
            .ForMember(d => d.DistributionCenterId, opt => opt.MapFrom(s => new OptionDto(s.DistributionCenterId, s.DistributionCenter.Name)))
            .ForMember(d => d.ClaimStatusId, opt => opt.MapFrom(s => new OptionDto(s.ClaimStatus.Id, s.ClaimStatus.Name)))
            .ForMember(d => d.ClaimSupplierStatusId, opt => opt.MapFrom(s => new OptionDto(s.ClaimSupplierStatus.Id, s.ClaimSupplierStatus.Name)))
            .ForMember(d => d.ClaimAccountManagerId, opt => opt.MapFrom(s => s.ClaimAccountManagerId.HasValue ? new OptionDto(s.ClaimAccountManagerId.Value, s.ClaimAccountManager.FullName) : null))
            .ForMember(d => d.ClaimManagerId, opt => opt.MapFrom(s => s.ClaimManagerId.HasValue ? new OptionDto(s.ClaimManagerId.Value, s.ClaimManager.FullName) : null))
            .ForMember(d => d.ClaimFloatId, opt => opt.MapFrom(s => s.ClaimFloatId.HasValue ? new OptionDto(s.ClaimFloatId.Value, s.ClaimFloat.Title) : null))
            .ForMember(d => d.EmployeeDivisionId, opt => opt.MapFrom(s => s.EmployeeDivisionId.HasValue ? new OptionDto(s.EmployeeDivisionId.Value, s.EmployeeDivision.Name) : null))
            .ForMember(d => d.VatId, opt => opt.MapFrom(s => new OptionDto(s.VatId, $"{s.Vat.Code} - {s.Vat.Name}")))
            .ForMember(d => d.Blobs, opt => opt.MapFrom(s => s.ClaimBlobs.OrderByDescending(e => e.EntityBlobId).Select(e => new EntityBlobDto(e))));
    }
}
