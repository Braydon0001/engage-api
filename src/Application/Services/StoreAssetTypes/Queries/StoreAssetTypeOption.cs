namespace Engage.Application.Services.StoreAssetTypes.Queries;

public class StoreAssetTypeOption : IMapFrom<StoreAssetType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetType, StoreAssetTypeOption>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name));
    }
}
