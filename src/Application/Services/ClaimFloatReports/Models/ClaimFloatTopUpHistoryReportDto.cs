namespace Engage.Application.Services.ClaimFloatReports.Models;

public class ClaimFloatTopUpHistoryReportDto : IMapFrom<ClaimFloatTopUpHistory>
{
    public int Id { get; set; }
    public string RegionName { get; set; }
    public string SupplierName { get; set; }
    public string FloatName { get; set; }
    public decimal TopUpAmount { get; set; }
    public string TopUpDate { get; set; }
    public string ToppedUpBy { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimFloatTopUpHistory, ClaimFloatTopUpHistoryReportDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimFloatTopUpHistoryId))
            .ForMember(d => d.FloatName, opt => opt.MapFrom(s => s.ClaimFloat.Title))
            .ForMember(d => d.RegionName, opt => opt.MapFrom(s => s.ClaimFloat.EngageRegion.Name))
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.ClaimFloat.Supplier.Name))
            .ForMember(d => d.TopUpDate, opt => opt.MapFrom(s => s.Created.ToShortDateString()))
            .ForMember(d => d.ToppedUpBy, opt => opt.MapFrom(s => s.CreatedBy));
    }
}
