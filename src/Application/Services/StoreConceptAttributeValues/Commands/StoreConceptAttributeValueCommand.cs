namespace Engage.Application.Services.StoreConceptAttributeValues.Commands;

public class StoreConceptAttributeValueCommand : IMapTo<StoreConceptAttributeValue>
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public int StoreConceptAttributeId { get; set; }
    public string Value { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConceptAttributeValueCommand, StoreConceptAttributeValue>();
    }
}

