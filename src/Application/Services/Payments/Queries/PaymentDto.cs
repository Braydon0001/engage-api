namespace Engage.Application.Services.Payments.Queries;

public class PaymentDto : IMapFrom<Payment>
{
    public int Id { get; init; }
    public int PaymentBatchId { get; init; }
    public DateTime BatchDate { get; init; }
    public string EngageRegions { get; init; }
    public int CreditorId { get; init; }
    public string CreditorName { get; init; }
    public bool IsClaimFromSupplier { get; init; }
    public int PaymentStatusId { get; init; }
    public string PaymentStatusName { get; init; }
    public int PaymentCostTypeId { get; init; }
    public string PaymentCostTypeName { get; init; }
    public int VatId { get; init; }
    public string VatName { get; init; }
    public int PaymentPeriodId { get; init; }
    public string PaymentPeriodName { get; init; }
    public string InvoiceNumber { get; init; }
    public DateTime InvoiceDate { get; init; }
    public DateTime Created { get; init; }
    public string CreatedBy { get; init; }
    public DateTime? RegionalApprovedDate { get; init; }
    public string RegionalApprovedBy { get; init; }
    public DateTime? CheckedDate { get; init; }
    public string CheckedBy { get; init; }
    public DateTime? ApprovedDate { get; init; }
    public string ApprovedBy { get; init; }
    public DateTime? AuthorisedDate { get; init; }
    public string AuthorisedBy { get; init; }
    public DateTime? RejectedDate { get; init; }
    public string RejectedBy { get; init; }
    public DateTime? ArchivedDate { get; init; }
    public string ArchivedBy { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Payment, PaymentDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentId))
               .ForMember(d => d.BatchDate, opt => opt.MapFrom(s => s.PaymentBatch.BatchDate))
               .ForMember(d => d.EngageRegions, opt => opt.MapFrom(s => string.Join(", ", s.PaymentBatch.BatchRegions.Select(s => s.EngageRegion.Name))))
               .ForMember(d => d.PaymentPeriodName, opt => opt.MapFrom(s => s.PaymentPeriod.Name + " - " + s.PaymentPeriod.PaymentYear.Name))
               .ForMember(d => d.RegionalApprovedBy, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.RegionalApproved).FirstOrDefault().CreatedBy))
               .ForMember(d => d.RegionalApprovedDate, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.RegionalApproved).FirstOrDefault().Created))
               .ForMember(d => d.CheckedBy, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Checked).FirstOrDefault().CreatedBy))
               .ForMember(d => d.CheckedDate, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Checked).FirstOrDefault().Created))
               .ForMember(d => d.ApprovedBy, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Approved).FirstOrDefault().CreatedBy))
               .ForMember(d => d.ApprovedDate, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Approved).FirstOrDefault().Created))
               .ForMember(d => d.AuthorisedBy, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Authorised).FirstOrDefault().CreatedBy))
               .ForMember(d => d.AuthorisedDate, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Authorised).FirstOrDefault().Created))
               .ForMember(d => d.RejectedBy, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Rejected).FirstOrDefault().CreatedBy))
               .ForMember(d => d.RejectedDate, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Rejected).FirstOrDefault().Created))
               .ForMember(d => d.ArchivedBy, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Archived).FirstOrDefault().CreatedBy))
               .ForMember(d => d.ArchivedDate, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Archived).FirstOrDefault().Created));
    }
}

public class PaymentSubTotalDto : PaymentDto, IMapFrom<Payment>
{
    public float AmountSubTotal { get; set; }
    public float VatAmountSubTotal { get; set; }
    public float TotalAmountSubTotal { get; set; }
    public new void Mapping(Profile profile)
    {
        profile.CreateMap<Payment, PaymentSubTotalDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.PaymentId))
            .ForMember(d => d.AmountSubTotal, opt => opt.MapFrom(s => s.PaymentLines.Where(e => e.Deleted == false).Sum(e => e.Amount)))
            .ForMember(d => d.VatAmountSubTotal, opt => opt.MapFrom(s => s.PaymentLines.Where(e => e.Deleted == false).Sum(e => e.VatAmount)))
            .ForMember(d => d.TotalAmountSubTotal, opt => opt.MapFrom(s => s.PaymentLines.Where(e => e.Deleted == false).Sum(e => e.Amount + e.VatAmount)))
            .ForMember(d => d.BatchDate, opt => opt.MapFrom(s => s.PaymentBatch.BatchDate))
            .ForMember(d => d.EngageRegions, opt => opt.MapFrom(s => string.Join(", ", s.PaymentBatch.BatchRegions.Select(s => s.EngageRegion.Name))))
            .ForMember(d => d.RegionalApprovedBy, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.RegionalApproved).FirstOrDefault().CreatedBy))
               .ForMember(d => d.RegionalApprovedDate, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.RegionalApproved).FirstOrDefault().Created))
               .ForMember(d => d.CheckedBy, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Checked).FirstOrDefault().CreatedBy))
               .ForMember(d => d.CheckedDate, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Checked).FirstOrDefault().Created))
               .ForMember(d => d.ApprovedBy, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Approved).FirstOrDefault().CreatedBy))
               .ForMember(d => d.ApprovedDate, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Approved).FirstOrDefault().Created))
               .ForMember(d => d.AuthorisedBy, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Authorised).FirstOrDefault().CreatedBy))
               .ForMember(d => d.AuthorisedDate, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Authorised).FirstOrDefault().Created))
               .ForMember(d => d.RejectedBy, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Rejected).FirstOrDefault().CreatedBy))
               .ForMember(d => d.RejectedDate, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Rejected).FirstOrDefault().Created))
               .ForMember(d => d.ArchivedBy, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Archived).FirstOrDefault().CreatedBy))
               .ForMember(d => d.ArchivedDate, opt => opt.MapFrom(s => s.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)Engage.Domain.Enums.PaymentStatusId.Archived).FirstOrDefault().Created));
    }
}
