namespace Engage.Application.Services.StoreConceptAttributes.Commands;

public class StoreConceptAttributeCommand : IMapTo<StoreConceptAttribute>
{
    public int Id { get; set; }
    public int StoreConceptId { get; set; }
    public int StoreConceptAttributeTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConceptAttributeCommand, StoreConceptAttribute>();
    }
}

