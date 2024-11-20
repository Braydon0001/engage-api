// auto-generated
using Engage.Application.Services.BankNames.Queries;
using Engage.Application.Services.BankAccountTypes.Queries;

namespace Engage.Application.Services.CreditorBankAccounts.Queries;

public class CreditorBankAccountVm : IMapFrom<CreditorBankAccount>
{
    public int Id { get; set; }
    public BankNameOption BankNameId { get; set; }
    public BankAccountTypeOption BankAccountTypeId { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
    public string BranchCode { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorBankAccount, CreditorBankAccountVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorBankAccountId))
               .ForMember(d => d.BankNameId, opt => opt.MapFrom(s => s.BankName))
               .ForMember(d => d.BankAccountTypeId, opt => opt.MapFrom(s => s.BankAccountType));
    }
}
