namespace Engage.Application.Services.StoreAssetTypeContacts.Queries;

public class StoreAssetTypeContactDto : IMapFrom<StoreAssetTypeContact>
{
    public int Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string EmailAddress { get; init; }
    public string MobilePhone { get; init; }
    public string StoreAssetTypes { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetTypeContact, StoreAssetTypeContactDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetTypeContactId))
               .ForMember(d => d.StoreAssetTypes, opt => opt.MapFrom(s => string.Join(", ", s.StoreAssetTypes.Select(o => o.StoreAssetType.Name))));
    }
}
