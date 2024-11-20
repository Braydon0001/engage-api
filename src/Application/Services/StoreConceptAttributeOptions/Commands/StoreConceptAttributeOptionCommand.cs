namespace Engage.Application.Services.StoreConceptAttributeOptions.Commands;

public class StoreConceptAttributeOptionCommand : IMapTo<StoreConceptAttributeOption>
{
    public int StoreConceptAttributeId { get; set; }
    public string Option { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConceptAttributeOptionCommand, StoreConceptAttributeOption>();
    }
}