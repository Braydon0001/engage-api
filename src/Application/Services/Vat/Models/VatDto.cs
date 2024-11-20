namespace Engage.Application.Services.Vat.Models;

public class VatDto : IMapFrom<Domain.Entities.Vat>
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.Vat, VatDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VatId));
    }
}
