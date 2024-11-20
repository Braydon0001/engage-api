namespace Engage.Application.Services.StoreConceptLevels.Models;

public class StoreConceptVm : IMapFrom<StoreConcept>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool HasAttributes { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConcept, StoreConceptVm>();
    }
}
