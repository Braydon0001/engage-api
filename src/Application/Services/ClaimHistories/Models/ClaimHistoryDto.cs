namespace Engage.Application.Services.ClaimHistories.Models;

public class ClaimHistoryDto : IMapFrom<ClaimHistory>
{
    public int Id { get; set; }
    public int ClaimId { get; set; }
    public int ClaimStatusId { get; set; }
    public string ClaimStatusName { get; set; }    
    public int ClaimSupplierStatusId { get; set; }
    public string ClaimSupplierStatusName { get; set; }
    public int? ClaimRejectedReasonId { get; set; }
    public string ClaimRejectedReasonName { get; set; }
    public string RejectedReason { get; set; }
    public int? ClaimPendingReasonId { get; set; }
    public string ClaimPendingReasonName { get; set; }
    public string PendingReason { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimHistory, ClaimHistoryDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimHistoryId))
            .ForMember(d => d.ClaimStatusName, opt => opt.MapFrom(s => s.ClaimStatus.Name))
            .ForMember(d => d.ClaimSupplierStatusName, opt => opt.MapFrom(s => s.ClaimSupplierStatus.Name))
            .ForMember(d => d.ClaimRejectedReasonName, opt => opt.MapFrom(s => s.ClaimRejectedReasonId.HasValue ? s.ClaimRejectedReason.Name : string.Empty))
            .ForMember(d => d.ClaimPendingReasonName, opt => opt.MapFrom(s => s.ClaimPendingReasonId.HasValue ? s.ClaimPendingReason.Name : string.Empty));
    }
}
