namespace Engage.Application.Services.ClaimFloatReports.Models;

public class ClaimFloatReportDto : IMapFrom<Claim>
{
    public string FloatName { get; set; }
    public string ClaimNumber { get; set; }
    public string ClaimDate { get; set; }
    public string ApprovedDate { get; set; }
    public string ApprovedBy { get; set; }
    public string StoreName { get; set; }
    public string ClaimClassificationName { get; set; }
    public string ClaimTypeName { get; set; }
    public decimal TotalAmount { get; set; }
    public string IsPayStore { get; set; }
    public string IsClaimFromSupplier { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Claim, ClaimFloatReportDto>()
            .ForMember(d => d.TotalAmount, opt => opt.MapFrom(s => s.ClaimSkus.Where(s => s.Deleted == false).Select(t => t.TotalAmount).DefaultIfEmpty().Sum()))
            .ForMember(d => d.FloatName, opt => opt.MapFrom(s => s.ClaimFloatClaims.Select(c => c.ClaimFloat).FirstOrDefault().Title))
            .ForMember(d => d.IsClaimFromSupplier, opt => opt.MapFrom(s => s.IsClaimFromSupplier ? "YES" : "NO"))
            .ForMember(d => d.IsPayStore, opt => opt.MapFrom(s => s.IsPayStore ? "YES" : "NO"))
            .ForMember(d => d.ClaimDate, opt => opt.MapFrom(s => s.ClaimDate.ToShortDateString()))
            .ForMember(d => d.ApprovedDate, opt => opt.MapFrom(s => s.ClaimDate.ToShortDateString()));
    }
}
