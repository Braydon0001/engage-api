namespace Engage.Application.Services.StoreAssetTypeContacts.Queries;

public class StoreAssetTypeContactOption : IMapFrom<StoreAssetTypeContact>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetTypeContact, StoreAssetTypeContactOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetTypeContactId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => $"{s.EmailAddress} - {s.FirstName}"));
    }
}