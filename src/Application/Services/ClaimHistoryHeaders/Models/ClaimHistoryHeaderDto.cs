namespace Engage.Application.Services.ClaimBatches.Models;

public class ClaimHistoryHeaderDto : IMapFrom<ClaimHistoryHeader>
{
    public int Id { get; set; }
    public int? ClaimStatusId { get; set; }
    public string ClaimStatusName { get; set; }
    public int? ClaimSupplierStatusId { get; set; }
    public string ClaimSupplierStatusName { get; set; }
    public int ClaimClassificationId { get; set; }
    public string ClaimClassificationName { get; set; }
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimHistoryHeader, ClaimHistoryHeaderDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimHistoryHeaderId))
            .ForMember(d => d.ClaimStatusName, opt => opt.MapFrom(s => s.ClaimStatusId.HasValue ? s.ClaimStatus.Name : null))
            .ForMember(d => d.ClaimSupplierStatusName, opt => opt.MapFrom(s => s.ClaimSupplierStatusId.HasValue ? s.ClaimSupplierStatus.Name : null))
            .ForMember(d => d.ClaimClassificationName, opt => opt.MapFrom(s => s.ClaimClassification.Name))
            .ForMember(d => d.EngageRegionName, opt => opt.MapFrom(s => s.EngageRegion.Name));
    }
}
