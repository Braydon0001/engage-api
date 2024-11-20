namespace Engage.Application.Services.StoreConceptAttributeStoreAssets.Models;

public class StoreConceptAttributeStoreAssetVm : IMapFrom<StoreConceptAttributeStoreAsset>
{

    public int StoreConceptAttributeId { get; set; }
    public int StoreAssetId { get; set; }
    public string StoreConceptAttributeName { get; set; }
    public string StoreConceptAttributeDescription { get; set; }
    public string StoreAssetName { get; set; }
    public string StoreAssetDescription { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConceptAttributeStoreAsset, StoreConceptAttributeStoreAssetVm>();
    }
}
