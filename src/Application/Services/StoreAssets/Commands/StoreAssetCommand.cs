namespace Engage.Application.Services.StoreAssets.Commands;

public class StoreAssetCommand : IMapTo<StoreAsset>
{
    public int StoreId { get; set; }
    public int? StoreAssetOwnerId { get; set; }
    public int StoreAssetTypeId { get; set; }
    public int? StoreAssetSubTypeId { get; set; }
    public int? StoreAssetConditionId { get; set; }
    public int? AssetStatusId { get; set; }
    public int? StoreAssetStatusId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Note { get; set; }
    public string SerialNumber { get; set; }
    public string EmailAddress { get; set; }
    public bool Disabled { get; set; }
    public DateTime? InstallDate { get; set; }
    public DateTime? UpliftDate { get; set; }
    public List<int> StoreConceptAttributeIds { get; set; }
    public List<int> StoreAssetTypeContactIds { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetCommand, StoreAsset>();
    }
}
