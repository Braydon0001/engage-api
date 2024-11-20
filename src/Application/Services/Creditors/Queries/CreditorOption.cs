namespace Engage.Application.Services.Creditors.Queries;

public class CreditorOption : IMapFrom<Creditor>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Creditor, CreditorOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CreditorId));
    }
}