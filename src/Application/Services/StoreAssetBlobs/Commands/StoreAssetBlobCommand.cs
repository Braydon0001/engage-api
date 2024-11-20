namespace Engage.Application.Services.AssetImages.Commands;

public class StoreAssetBlobCommand : IMapTo<StoreAssetBlob>
{
    public int StoreAssetId { get; set; }
    public string ImageUrl { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetBlobCommand, StoreAssetBlob>();
    }
}
