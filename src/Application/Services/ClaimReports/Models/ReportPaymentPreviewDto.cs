namespace Engage.Application.Services.ClaimReports.Models;

public class ReportPaymentPreviewDto : IMapFrom<Claim>
{
    public ReportPaymentPreviewDto()
    {
        ClaimSkus = new HashSet<ClaimSku>();
        BankDetails = new HashSet<StoreBankDetail>();
    }
    public int Id { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public string ClaimNumber { get; set; }
    public decimal TotalClaimNumber { get; set; }
    public decimal TotalStore { get; set; }
    public string StoreEmailAddress { get; set; }
    public string BranchNumber { get; set; }
    public string AccountNumber { get; set; }
    public string AccountType { get; set; }
    public DateTime? PaidDate { get; set; }
    public string PaidBy { get; set; }
    public decimal Amount { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime Created { get; set; }
    public ICollection<ClaimSku> ClaimSkus { get; set; }
    public ICollection<StoreBankDetail> BankDetails { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Claim, ReportPaymentPreviewDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimId))
            .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
            .ForMember(d => d.AccountNumber, opt => opt.MapFrom(s => s.Store.BankDetails.Where(s => s.Disabled == false && s.IsPrimary == true).FirstOrDefault().AccountNumber))
            .ForMember(d => d.BranchNumber, opt => opt.MapFrom(s => s.Store.BankDetails.Where(s => s.Disabled == false && s.IsPrimary == true).FirstOrDefault().BranchCode))
            .ForMember(d => d.StoreEmailAddress, opt => opt.MapFrom(s => s.Store.StoreContacts.SingleOrDefault(s => s.EntityContactTypeId == (int)EntityContactTypeId.ClaimPaymentNotifier).EmailAddress1))
            .ForMember(d => d.ClaimSkus, opt => opt.MapFrom(s => s.ClaimSkus.Where(c => c.Deleted == false)))
            .ForMember(d => d.BankDetails, opt => opt.MapFrom(s => s.Store.BankDetails.Where(s => s.Disabled == false)))
            .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.ClaimSkus.Where(c => c.Deleted == false).Select(t => t.Amount + t.VatAmount).DefaultIfEmpty().Sum()))
            .ForMember(d => d.TotalAmount, opt => opt.MapFrom(s => s.ClaimSkus.Where(c => c.Deleted == false).Select(t => t.TotalAmount).DefaultIfEmpty().Sum()));
    }
}
