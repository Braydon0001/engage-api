// auto-generated
namespace Engage.Application.Services.StoreAssetSubTypes.Queries;

public class StoreAssetSubTypeDto : IMapFrom<StoreAssetSubType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int StoreAssetTypeId { get; set; }
    public string StoreAssetTypeNames { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetSubType, StoreAssetSubTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetSubTypeId))
               .ForMember(d => d.StoreAssetTypeNames, opt => opt.MapFrom(s => string.Join(", ", s.StoreAssetTypes.Select(x => x.StoreAssetType.Name))));
    }
}
