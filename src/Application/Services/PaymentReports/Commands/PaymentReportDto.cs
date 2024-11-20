namespace Engage.Application.Services.PaymentReports.Commands;

public class PaymentReportDto : IMapFrom<PaymentLine>
{
    public int PaymentId { get; set; }
    public int PaymentBatchId { get; init; }
    public string BatchDate { get; init; }
    public string EngageRegions { get; init; }
    public string PaymentStatusName { get; set; }
    public string CreditorName { get; set; }
    public string InvoiceNumber { get; set; }
    public string InvoiceDate { get; set; }
    public string ExpenseTypeName { get; set; }
    public string Employees { get; init; }
    public string CostCenters { get; init; }
    public string Divisions { get; init; }
    public string SubDepartments { get; init; }
    public float Amount { get; set; }
    public float VatAmount { get; set; }
    public float TotalAmount { get; set; }
    public string IsClaimFromSupplier { get; init; }
    public string IsVat { get; set; }
    public string IsSplitAmount { get; set; }
    public string HasQuote { get; set; }
    public string HasInvoice { get; set; }
    public string PaymentPeriodName { get; init; }

    public string CreatedBy { get; set; }
    public string CreatedDate { get; set; }
    public string RegionalApprovedDate { get; init; }
    public string RegionalApprovedBy { get; init; }
    public string CheckedDate { get; init; }
    public string CheckedBy { get; init; }
    public string ApprovedDate { get; init; }
    public string ApprovedBy { get; init; }
    public string AuthorisedDate { get; init; }
    public string AuthorisedBy { get; init; }
    public string RejectedDate { get; init; }
    public string RejectedBy { get; init; }
    public string ArchivedDate { get; init; }
    public string ArchivedBy { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentLine, PaymentReportDto>()
            .ForMember(d => d.PaymentId, opt => opt.MapFrom(s => s.PaymentId))
            .ForMember(d => d.PaymentBatchId, opt => opt.MapFrom(s => s.Payment.PaymentBatchId))
            .ForMember(d => d.BatchDate, opt => opt.MapFrom(s => s.Payment.PaymentBatch.BatchDate.ToShortDateString()))
            .ForMember(d => d.EngageRegions, opt => opt.MapFrom(s => string.Join(", ", s.Payment.PaymentBatch.BatchRegions.Select(s => s.EngageRegion.Name))))
            .ForMember(d => d.PaymentStatusName, opt => opt.MapFrom(s => s.Payment.PaymentStatus.Name))
            .ForMember(d => d.CreditorName, opt => opt.MapFrom(s => s.Payment.Creditor.Name))
            .ForMember(d => d.InvoiceNumber, opt => opt.MapFrom(s => s.Payment.InvoiceNumber))
            .ForMember(d => d.InvoiceDate, opt => opt.MapFrom(s => s.Payment.InvoiceDate.ToShortDateString()))
            .ForMember(d => d.ExpenseTypeName, opt => opt.MapFrom(s => s.ExpenseType.Name))
            .ForMember(d => d.Employees, opt => opt.MapFrom(s => string.Join(", ", s.Employees.Select(s => s.Employee.FirstName + " " + s.Employee.LastName + " (" + s.Employee.Code + ")"))))
            .ForMember(d => d.CostCenters, opt => opt.MapFrom(s => string.Join(", ", s.CostCenters.Select(s => s.CostCenter.Name))))
            .ForMember(d => d.Divisions, opt => opt.MapFrom(s => string.Join(", ", s.EmployeeDivisions.Select(s => s.EmployeeDivision.Name))))
            .ForMember(d => d.SubDepartments, opt => opt.MapFrom(s => string.Join(", ", s.SubDepartments.Select(s => s.CostSubDepartment.Name))))
            .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.Amount))
            .ForMember(d => d.VatAmount, opt => opt.MapFrom(s => s.VatAmount))
            .ForMember(d => d.TotalAmount, opt => opt.MapFrom(s => s.Amount + s.VatAmount))
            .ForMember(d => d.IsClaimFromSupplier, opt => opt.MapFrom(s => s.Payment.IsClaimFromSupplier ? "YES" : "NO"))
            .ForMember(d => d.IsVat, opt => opt.MapFrom(s => s.IsVat ? "YES" : "NO"))
            .ForMember(d => d.IsSplitAmount, opt => opt.MapFrom(s => s.IsSplitAmount ? "YES" : "NO"))
            .ForMember(d => d.HasQuote, opt => opt.MapFrom(s => s.HasQuote ? "YES" : "NO"))
            .ForMember(d => d.HasInvoice, opt => opt.MapFrom(s => s.HasInvoice ? "YES" : "NO"))
            .ForMember(d => d.PaymentPeriodName, opt => opt.MapFrom(s => s.Payment.PaymentPeriod.PaymentYear.Name + " " + s.Payment.PaymentPeriod.Name))
            .ForMember(d => d.CreatedBy, opt => opt.MapFrom(s => s.Payment.CreatedBy))
            .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => s.Payment.Created.ToShortDateString()))
            .ForMember(d => d.RegionalApprovedBy, opt => opt.MapFrom(s => s.Payment.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)PaymentStatusId.RegionalApproved).FirstOrDefault().CreatedBy))
            .ForMember(d => d.RegionalApprovedDate, opt => opt.MapFrom(s => s.Payment.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)PaymentStatusId.RegionalApproved).FirstOrDefault().Created.ToShortDateString()))
            .ForMember(d => d.ApprovedBy, opt => opt.MapFrom(s => s.Payment.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)PaymentStatusId.Approved).FirstOrDefault().CreatedBy))
            .ForMember(d => d.ApprovedDate, opt => opt.MapFrom(s => s.Payment.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)PaymentStatusId.Approved).FirstOrDefault().Created.ToShortDateString()))
            .ForMember(d => d.CheckedBy, opt => opt.MapFrom(s => s.Payment.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)PaymentStatusId.Checked).FirstOrDefault().CreatedBy))
            .ForMember(d => d.CheckedDate, opt => opt.MapFrom(s => s.Payment.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)PaymentStatusId.Checked).FirstOrDefault().Created.ToShortDateString()))
            .ForMember(d => d.AuthorisedBy, opt => opt.MapFrom(s => s.Payment.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)PaymentStatusId.Authorised).FirstOrDefault().CreatedBy))
            .ForMember(d => d.AuthorisedDate, opt => opt.MapFrom(s => s.Payment.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)PaymentStatusId.Authorised).FirstOrDefault().Created.ToShortDateString()))
            .ForMember(d => d.RejectedBy, opt => opt.MapFrom(s => s.Payment.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)PaymentStatusId.Rejected).FirstOrDefault().CreatedBy))
            .ForMember(d => d.RejectedDate, opt => opt.MapFrom(s => s.Payment.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)PaymentStatusId.Rejected).FirstOrDefault().Created.ToShortDateString()))
            .ForMember(d => d.ArchivedBy, opt => opt.MapFrom(s => s.Payment.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)PaymentStatusId.Archived).FirstOrDefault().CreatedBy))
            .ForMember(d => d.ArchivedDate, opt => opt.MapFrom(s => s.Payment.PaymentStatusHistories.Where(p => p.PaymentStatusId == (int)PaymentStatusId.Archived).FirstOrDefault().Created.ToShortDateString()));
    }
}