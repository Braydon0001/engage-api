namespace Engage.Application.Services.Vat.Queries;

public class VatOption : IMapFrom<Engage.Domain.Entities.Vat>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Engage.Domain.Entities.Vat, VatOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VatId));
    }
}