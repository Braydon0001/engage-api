// auto-generated
namespace Engage.Application.Services.BankNames.Queries;

public class BankNameOption : IMapFrom<BankName>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<BankName, BankNameOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
    }
}