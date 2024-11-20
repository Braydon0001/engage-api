// auto-generated
namespace Engage.Application.Services.CreditorBankAccounts.Queries;

public class CreditorBankAccountOption : IMapFrom<CreditorBankAccount>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorBankAccount, CreditorBankAccountOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorBankAccountId));
    }
}