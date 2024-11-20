namespace Engage.Application.Services.StoreAssetOwners.Queries;

public class StoreAssetOwnerOption : IMapFrom<StoreAssetOwner>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetOwner, StoreAssetOwnerOption>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id));
    }
}
