// auto-generated

using Engage.Application.Services.StoreAssetTypes.Queries;

namespace Engage.Application.Services.StoreAssetSubTypes.Queries;

public class StoreAssetSubTypeVm : IMapFrom<StoreAssetSubType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<StoreAssetTypeOption> StoreAssetTypeIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetSubType, StoreAssetSubTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetSubTypeId))
               .ForMember(d => d.StoreAssetTypeIds, opt => opt.MapFrom(s => s.StoreAssetTypes.Select(x => new StoreAssetTypeOption { Id = x.StoreAssetTypeId, Name = x.StoreAssetType.Name })));
    }
}
