
using Engage.Application.Services.StoreAssetTypes.Queries;

namespace Engage.Application.Services.StoreAssetTypeContacts.Queries;

public class StoreAssetTypeContactVm : IMapFrom<StoreAssetTypeContact>
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string EmailAddress { get; init; }
    public string MobilePhone { get; init; }
    public List<StoreAssetTypeOption> StoreAssetTypeIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetTypeContact, StoreAssetTypeContactVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetTypeContactId))
               .ForMember(d => d.StoreAssetTypeIds, opt => opt.MapFrom(s => s.StoreAssetTypes.Select(x
                            => x.StoreAssetType).ToList()));
    }
}
