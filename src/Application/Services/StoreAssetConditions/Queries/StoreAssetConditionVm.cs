// auto-generated
namespace Engage.Application.Services.StoreAssetConditions.Queries;

public class StoreAssetConditionVm : IMapFrom<StoreAssetCondition>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetCondition, StoreAssetConditionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetConditionId));
    }
}
