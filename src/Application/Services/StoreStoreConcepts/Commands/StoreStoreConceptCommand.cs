namespace Engage.Application.Services.StoreStoreConcepts.Commands;

public class StoreStoreConceptCommand : IMapTo<StoreStoreConcept>
{
    public int StoreId { get; set; }
    public string StoreConceptName { get; set; }
    public int Level { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreStoreConceptCommand, StoreStoreConcept>();
    }
}

