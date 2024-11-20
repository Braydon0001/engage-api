namespace Engage.Application.Services.StoreConceptAttributes.Models;
using Engage.Application.Services.StoreConceptAttributeOptions.Models;
public class StoreConceptAttributeDto : IMapFrom<StoreConceptAttribute>
{
    public int Id { get; set; }
    public int StoreConceptId { get; set; }
    public string StoreConceptName { get; set; }
    public int StoreConceptAttributeTypeId { get; set; }
    public string StoreConceptAttributeTypeName { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool Disabled { get; set; }
    public List<StoreConceptAttributeOptionDto> StoreConceptAttributeOptions { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConceptAttribute, StoreConceptAttributeDto>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreConceptAttributeId))
           .ForMember(d => d.StoreConceptAttributeOptions, opt => opt.MapFrom(s => s.StoreConceptAttributeOptions.Select(o =>
                                                                            new StoreConceptAttributeOptionDto(o.StoreConceptAttributeOptionId, o.StoreConceptAttributeId, o.Option))));
    }
}
