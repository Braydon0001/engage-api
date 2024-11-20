// auto-generated
namespace Engage.Application.Services.StoreAssetConditions.Queries;

public class StoreAssetConditionOption : IMapFrom<StoreAssetCondition>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetCondition, StoreAssetConditionOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetConditionId));
    }
}