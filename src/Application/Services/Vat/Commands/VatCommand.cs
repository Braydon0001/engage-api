namespace Engage.Application.Services.Vat.Commands;

public class VatCommand : IMapTo<Domain.Entities.Vat>
{
    public string Code { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VatCommand, Domain.Entities.Vat>();
    }
}
