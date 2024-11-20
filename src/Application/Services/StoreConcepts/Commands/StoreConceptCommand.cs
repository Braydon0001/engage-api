namespace Engage.Application.Services.StoreConcepts.Commands;

public class StoreConceptCommand : IMapTo<StoreConcept>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int EngageDepartmentId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConceptCommand, StoreConcept>();
    }
}
