namespace Engage.Application.Services.StoreConcepts.Models;

public class StoreConceptDto : IMapFrom<StoreConcept>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }
    public int EngageDepartmentId { get; set; }
    public string EngageDepartmentName { get; set; }
    public List<JsonFile> Files { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConcept, StoreConceptDto>();
    }
}
