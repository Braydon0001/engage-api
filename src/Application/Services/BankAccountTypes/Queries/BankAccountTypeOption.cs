// auto-generated
namespace Engage.Application.Services.BankAccountTypes.Queries;

public class BankAccountTypeOption : IMapFrom<BankAccountType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<BankAccountType, BankAccountTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
    }
}