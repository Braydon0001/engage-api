namespace Engage.Application.Services.StoreStoreConcepts.Models;

public class StoreStoreConceptDto : IMapFrom<StoreStoreConcept>
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public int StoreConceptId { get; set; }
    public string StoreConceptName { get; set; }
    public int Level { get; set; }
    public string ImageUrl { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreStoreConcept, StoreStoreConceptDto>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreStoreConceptId));
    }
}
