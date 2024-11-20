namespace Engage.Application.Services.StoreAssetTypes.Queries;

public class StoreAssetTypeDto : IMapFrom<StoreAssetType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string StoreAssetOwners { get; set; }
    public string StoreAssetSubTypes { get; set; }
    public string StoreAssetTypeContacts { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetType, StoreAssetTypeDto>()
            .ForMember(d => d.StoreAssetOwners, opt => opt.MapFrom(s => string.Join(", ", s.AssetOwnerAssetTypes.Select(x => x.StoreAssetOwner.Name))))
            .ForMember(d => d.StoreAssetSubTypes, opt => opt.MapFrom(s => string.Join(", ", s.AssetSubTypes.Select(x => x.StoreAssetSubType.Name))))
            .ForMember(d => d.StoreAssetTypeContacts, opt => opt.MapFrom(s => string.Join(", ", s.AssetContacts.Select(x => x.StoreAssetTypeContact.FirstName))));
    }
}
