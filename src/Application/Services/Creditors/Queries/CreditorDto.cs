namespace Engage.Application.Services.Creditors.Queries;

public class CreditorDto : IMapFrom<Creditor>
{
    public int Id { get; init; }
    public int CreditorStatusId { get; init; }
    public string CreditorStatusName { get; init; }
    public string Name { get; init; }
    public string TradingName { get; set; }
    public bool IsVatRegistered { get; init; }
    public string VatNumber { get; set; }
    public DateTime Created { get; init; }
    public string CreatedBy { get; init; }
    public DateTime? ApprovedDate { get; init; }
    public string ApprovedBy { get; init; }
    public DateTime? RejectedDate { get; init; }
    public string RejectedBy { get; init; }
    public string Reason { get; init; }
    public DateTime BankConfirmationDate { get; set; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Creditor, CreditorDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorId))
               .ForMember(d => d.ApprovedBy, opt => opt.MapFrom(s => s.CreditorStatusHistories.Where(p => p.CreditorStatusId == (int)Engage.Domain.Enums.CreditorStatusId.Approved).FirstOrDefault().CreatedBy))
               .ForMember(d => d.ApprovedDate, opt => opt.MapFrom(s => s.CreditorStatusHistories.Where(p => p.CreditorStatusId == (int)Engage.Domain.Enums.CreditorStatusId.Approved).FirstOrDefault().Created))
               .ForMember(d => d.RejectedBy, opt => opt.MapFrom(s => s.CreditorStatusHistories.Where(p => p.CreditorStatusId == (int)Engage.Domain.Enums.CreditorStatusId.Rejected).FirstOrDefault().CreatedBy))
               .ForMember(d => d.RejectedDate, opt => opt.MapFrom(s => s.CreditorStatusHistories.Where(p => p.CreditorStatusId == (int)Engage.Domain.Enums.CreditorStatusId.Rejected).FirstOrDefault().Created))
               .ForMember(d => d.Reason, opt => opt.MapFrom(s => s.CreditorStatusHistories.Where(p => p.CreditorStatusId == (int)Engage.Domain.Enums.CreditorStatusId.Rejected).FirstOrDefault().Reason));
    }
}
