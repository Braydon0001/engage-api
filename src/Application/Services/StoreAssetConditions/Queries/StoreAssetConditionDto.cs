// auto-generated
namespace Engage.Application.Services.StoreAssetConditions.Queries;

public class StoreAssetConditionDto : IMapFrom<StoreAssetCondition>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetCondition, StoreAssetConditionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetConditionId));
    }
}
