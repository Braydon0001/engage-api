namespace Engage.Application.Services.StoreConceptAttributeValues.Models;

public class StoreConceptAttributeValueVm : IMapTo<StoreConceptAttributeValue>
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public int StoreConceptAttributeId { get; set; }
    public string StoreConceptAttributeName { get; set; }
    public string StoreConceptAttributeTypeName { get; set; }
    public string Value { get; set; }
    public ICollection<OptionDto> StoreAssetIds { get; set; }
    public ICollection<OptionDto> StoreAssetOptions { get; set; }
    public ICollection<OptionDto> StoreConceptAttributeOptions { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConceptAttributeValue, StoreConceptAttributeValueVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreConceptAttributeValueId))
            .ForMember(d => d.StoreConceptAttributeTypeName, opt => opt.MapFrom(s => s.StoreConceptAttribute.StoreConceptAttributeType.Name))
            .ForMember(d => d.StoreAssetOptions, opt => opt.MapFrom(s => s.Store.StoreAssets.Select(e => new OptionDto(e.StoreAssetId, e.Name))))
            .ForMember(d => d.StoreAssetIds, opt => opt.MapFrom(s => s.StoreConceptAttribute.StoreConceptAttributeStoreAssets.Select(e => new OptionDto(e.StoreAssetId, e.StoreAsset.Name))))
            .ForMember(d => d.StoreConceptAttributeOptions, opt => opt.MapFrom(s => s.StoreConceptAttribute.StoreConceptAttributeOptions.Select(e => new OptionDto(e.StoreConceptAttributeId, e.StoreConceptAttribute.Name))));
    }

}
