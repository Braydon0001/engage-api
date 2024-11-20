// auto-generated
namespace Engage.Application.Services.CreditorBankAccounts.Queries;

public class CreditorBankAccountDto : IMapFrom<CreditorBankAccount>
{
    public int Id { get; set; }
    public int BankNameId { get; set; }
    public string BankNameName { get; set; }
    public int BankAccountTypeId { get; set; }
    public string BankAccountTypeName { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public string BranchCode { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorBankAccount, CreditorBankAccountDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorBankAccountId));
    }
}
