namespace Engage.Application.Services.StoreConceptAttributeStoreAssets.Commands;

public class StoreConceptAttributeStoreAssetCommand : IMapTo<StoreConceptAttributeStoreAsset>
{
    public int StoreConceptAttributeId { get; set; }
    public int StoreAssetId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConceptAttributeStoreAssetCommand, StoreConceptAttributeStoreAsset>();
    }
}
