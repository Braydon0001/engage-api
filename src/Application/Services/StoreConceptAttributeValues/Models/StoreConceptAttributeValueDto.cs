namespace Engage.Application.Services.StoreConceptAttributeValues.Models;

public class StoreConceptAttributeValueDto : IMapFrom<StoreConceptAttributeValue>
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public int StoreConceptAttributeId { get; set; }
    public string StoreConceptAttributeName { get; set; }
    public int StoreConceptAttributeTypeId { get; set; }
    public string StoreConceptAttributeTypeName { get; set; }
    public string Value { get; set; }
    public int? StoreConceptAttributeOptionId { get; set; }
    public string StoreConceptAttributeOptionValue { get; set; }
    public ICollection<OptionDto> StoreConceptAttributeStoreAssets { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConceptAttributeValue, StoreConceptAttributeValueDto>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreConceptAttributeValueId))
           .ForMember(d => d.StoreConceptAttributeName, opt => opt.MapFrom(s => s.StoreConceptAttribute.Name))
           .ForMember(d => d.StoreConceptAttributeTypeId, opt => opt.MapFrom(s => s.StoreConceptAttribute.StoreConceptAttributeTypeId))
           .ForMember(d => d.StoreConceptAttributeTypeName, opt => opt.MapFrom(s => s.StoreConceptAttribute.StoreConceptAttributeType.Name))
           .ForMember(d => d.StoreConceptAttributeOptionValue, opt => opt.MapFrom(s => s.StoreConceptAttributeOption.Option))
           .ForMember(d => d.StoreConceptAttributeStoreAssets, opt => opt.MapFrom(s => s.StoreConceptAttribute.StoreConceptAttributeStoreAssets.Select(e => e.StoreAsset).Where(e => e.StoreId == s.StoreId).Select(e => new OptionDto(e.StoreAssetId, e.Name))));
    }
}
