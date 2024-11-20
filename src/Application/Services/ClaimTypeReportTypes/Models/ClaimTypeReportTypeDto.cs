namespace Engage.Application.Services.ClaimTypeReportTypes.Models;

public class ClaimTypeReportTypeDto : IMapFrom<ClaimTypeReportType>
{
    public int ClaimTypeId { get; set; }
    public int ClaimReportTypeId { get; set; }
    public string ClaimTypeName { get; set; }
    public string ClaimReportTypeName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimTypeReportType, ClaimTypeReportTypeDto>()
            .ForMember(d => d.ClaimTypeId, opt => opt.MapFrom(s => s.ClaimType.ClaimTypeId))
            .ForMember(d => d.ClaimReportTypeId, opt => opt.MapFrom(s => s.ClaimReportType.Id))
            .ForMember(d => d.ClaimTypeName, opt => opt.MapFrom(s => s.ClaimType.Name))
            .ForMember(d => d.ClaimReportTypeName, opt => opt.MapFrom(s => s.ClaimReportType.Name));
    }
}
