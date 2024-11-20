namespace Engage.Application.Services.ClaimReports.Models;

public class ClaimFNBreportDto : IMapFrom<Claim>
{
    public ClaimFNBreportDto()
    {
        ClaimSkus = new HashSet<ClaimSku>();
        BankDetails = new HashSet<StoreBankDetail>();
    }
    public int Id { get; set; }
    public string RecipientName { get; set; }
    public string RecipientAccount { get; set; }
    public int RecipientAccountType { get; set; }
    public string BranchCode { get; set; }
    public decimal Amount { get; set; }
    public string OwnReference { get; set; }
    public string RecipientReference { get; set; }
    public ICollection<ClaimSku> ClaimSkus { get; set; }
    public ICollection<StoreBankDetail> BankDetails { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Claim, ClaimFNBreportDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimId))
            .ForMember(d => d.RecipientName, opt => opt.MapFrom(s => s.Store.Name))
            .ForMember(d => d.RecipientAccount, opt => opt.MapFrom(s => s.Store.BankDetails.Where(s => s.Disabled == false && s.IsPrimary == true).FirstOrDefault().AccountNumber))
            .ForMember(d => d.BranchCode, opt => opt.MapFrom(s => s.Store.BankDetails.Where(s => s.Disabled == false && s.IsPrimary == true).FirstOrDefault().BranchCode))
            .ForMember(d => d.OwnReference, opt => opt.MapFrom(s => s.Store.Name + " Claims"))
            .ForMember(d => d.RecipientReference, opt => opt.MapFrom(s => "Engage Claims"))
            .ForMember(d => d.RecipientAccountType, opt => opt.MapFrom(s => 1))
            .ForMember(d => d.Amount, opt => opt.MapFrom(s => s.ClaimSkus.Where(s => s.Deleted == false).Select(t => t.TotalAmount).DefaultIfEmpty().Sum()));
    }
}
