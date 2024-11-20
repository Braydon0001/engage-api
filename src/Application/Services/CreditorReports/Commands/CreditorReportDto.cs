namespace Engage.Application.Services.CreditorReports.Commands;

public class CreditorReportDto : IMapFrom<Creditor>
{
    public int CreditorId { get; set; }
    public string Name { get; set; }
    public string CreditorStatusName { get; set; }
    public string IsVatRegistered { get; set; }
    public string VatNumber { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedDate { get; set; }
    public string CheckedBy { get; init; }
    public string CheckedDate { get; init; }
    public string ApprovedBy { get; init; }
    public string ApprovedDate { get; init; }
    public string RejectedBy { get; init; }
    public string RejectedDate { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Creditor, CreditorReportDto>()
            .ForMember(d => d.CreditorStatusName, opt => opt.MapFrom(s => s.CreditorStatus.Name))
            .ForMember(d => d.IsVatRegistered, opt => opt.MapFrom(s => s.IsVatRegistered ? "YES" : "NO"))
            .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => s.Created.ToShortDateString()))
            .ForMember(d => d.CheckedBy, opt => opt.MapFrom(s => s.CreditorStatusHistories.Where(p => p.CreditorStatusId == (int)CreditorStatusId.Checked).FirstOrDefault().CreatedBy))
            .ForMember(d => d.CheckedDate, opt => opt.MapFrom(s => s.CreditorStatusHistories.Where(p => p.CreditorStatusId == (int)CreditorStatusId.Checked).FirstOrDefault().Created.ToShortDateString()))
            .ForMember(d => d.ApprovedBy, opt => opt.MapFrom(s => s.CreditorStatusHistories.Where(p => p.CreditorStatusId == (int)CreditorStatusId.Approved).FirstOrDefault().CreatedBy))
            .ForMember(d => d.ApprovedDate, opt => opt.MapFrom(s => s.CreditorStatusHistories.Where(p => p.CreditorStatusId == (int)CreditorStatusId.Approved).FirstOrDefault().Created.ToShortDateString()))
            .ForMember(d => d.RejectedBy, opt => opt.MapFrom(s => s.CreditorStatusHistories.Where(p => p.CreditorStatusId == (int)CreditorStatusId.Rejected).FirstOrDefault().CreatedBy))
            .ForMember(d => d.RejectedDate, opt => opt.MapFrom(s => s.CreditorStatusHistories.Where(p => p.CreditorStatusId == (int)CreditorStatusId.Rejected).FirstOrDefault().Created.ToShortDateString()));
    }
}