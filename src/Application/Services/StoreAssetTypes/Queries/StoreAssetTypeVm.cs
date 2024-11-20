using Engage.Application.Services.StoreAssetSubTypes.Queries;
using Engage.Application.Services.StoreAssetTypeContacts.Queries;

namespace Engage.Application.Services.StoreAssetTypes.Queries;

public class StoreAssetTypeVm : IMapFrom<StoreAssetType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<OptionDto> StoreAssetOwnerIds { get; set; }
    public List<StoreAssetTypeContactOption> StoreAssetTypeContactIds { get; set; }
    public List<StoreAssetSubTypeOption> StoreAssetSubTypeIds { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetType, StoreAssetTypeVm>()
            .ForMember(d => d.StoreAssetOwnerIds, opt => opt.MapFrom(s => s.AssetOwnerAssetTypes.Select(x => new OptionDto(x.StoreAssetOwnerId, x.StoreAssetOwner.Name))))
            .ForMember(d => d.StoreAssetTypeContactIds, opt => opt.MapFrom(s => s.AssetContacts.Select(x => x.StoreAssetTypeContact).ToList()))
            .ForMember(d => d.StoreAssetSubTypeIds, opt => opt.MapFrom(s => s.AssetSubTypes.Select(x => x.StoreAssetSubType).ToList()));
        //.ForMember(d => d.StoreAssetTypeContactIds, opt => opt.MapFrom(s => s.AssetContacts.Select(x
        //                    => new StoreAssetTypeContactOption 
        //                    { Id = x.StoreAssetTypeContactId, Name = $"{x.StoreAssetTypeContact.EmailAddress} {x.StoreAssetTypeContact.FirstName}" })));
    }
}
