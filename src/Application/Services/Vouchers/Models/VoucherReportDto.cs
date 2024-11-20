namespace Engage.Application.Services.Vouchers.Models;

public class VoucherReportDto : IMapFrom<VoucherDetail>
{
    public int VoucherId { get; set; }
    public string VoucherName { get; set; }
    public decimal Total { get; set; }
    public decimal TotalUsed { get; set; }
    public string VoucherNote { get; set; }
    public int VoucherDetailId { get; set; }
    public string VoucherDetailsNumber { get; set; }
    public string VoucherDetailNote { get; set; }
    public decimal VoucherDetailAmount { get; set; }
    public string RecievedDate { get; set; }
    public string EmployeeCode { get; set; }
    public string EmployeeName { get; set; }
    public string AssignedDate { get; set; }
    public string ClaimNumber { get; set; }
    public string StoreName { get; set; }
    public string StoreContact { get; set; }
    public string ClosedDate { get; set; }
    public string AttachmentUrl { get; set; }
    public List<JsonFile> Files { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VoucherDetail, VoucherReportDto>()
            .ForMember(d => d.VoucherId, opt => opt.MapFrom(s => s.Voucher.VoucherId))
            .ForMember(d => d.Total, opt => opt.MapFrom(s => s.Voucher.Total))
            .ForMember(d => d.VoucherNote, opt => opt.MapFrom(s => s.Voucher.Note))
            .ForMember(d => d.VoucherDetailId, opt => opt.MapFrom(s => s.VoucherDetailId))
            //.ForMember(d => d.RecievedDate, opt => opt.MapFrom(s => s.Created.ToShortDateString()))
            .ForMember(d => d.RecievedDate, opt => opt.MapFrom(s => s.Created.ToString("yyyy/MM/dd")))
            .ForMember(d => d.EmployeeCode, opt => opt.MapFrom(s => s.Employee.Code))
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => $"{s.Employee.FirstName} {s.Employee.LastName}"))
            .ForMember(d => d.VoucherDetailsNumber, opt => opt.MapFrom(s => s.VoucherNumber))
            .ForMember(d => d.VoucherDetailNote, opt => opt.MapFrom(s => s.Note))
            .ForMember(d => d.VoucherDetailAmount, opt => opt.MapFrom(s => s.Amount))
            .ForMember(d => d.ClaimNumber, opt => opt.MapFrom(s => s.Claim.ClaimNumber))
            .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
            .ForMember(d => d.TotalUsed, opt => opt.MapFrom(s => s.Voucher.VoucherDetails.Where(e => e.Deleted == false).Sum(e => e.Amount)))
            .ForMember(d => d.AssignedDate, opt => opt.MapFrom(s => s.AssignedDate.Value.ToString("yyyy/MM/dd")))
            .ForMember(d => d.StoreContact, opt => opt.MapFrom(s => s.StoreContactId.HasValue ? s.StoreContact.FullName : ""))
            .ForMember(d => d.ClosedDate, opt => opt.MapFrom(s => s.ClosedDate.Value.ToString("yyyy/MM/dd")))
            .ForMember(d => d.AttachmentUrl, opt => opt.Ignore())
            .ForMember(d => d.Files, opt => opt.MapFrom(s => s.Files));
        //.ForMember(d => d.AttachmentUrl, opt => opt.MapFrom(s => s.Files != null ? s.Files.First().Url : ""));
    }
}
