// auto-generated
namespace Engage.Application.Services.StoreAssetSubTypes.Queries;

public class StoreAssetSubTypeOption : IMapFrom<StoreAssetSubType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetSubType, StoreAssetSubTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetSubTypeId));
    }
}