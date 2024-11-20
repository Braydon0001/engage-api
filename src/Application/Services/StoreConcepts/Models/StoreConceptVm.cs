namespace Engage.Application.Services.StoreConcepts.Models;

public class StoreConceptVm : IMapFrom<StoreConcept>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }
    public OptionDto EngageDepartmentId { get; set; }
    public List<JsonFile> Files { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConcept, StoreConceptVm>()
            .ForMember(d => d.EngageDepartmentId, opt => opt.MapFrom(s => new OptionDto(s.EngageDepartmentId, s.EngageDepartment.Name)));
    }
}
