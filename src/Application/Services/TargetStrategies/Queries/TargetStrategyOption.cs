namespace Engage.Application.Services.TargetStrategies.Queries;

public class TargetStrategyOption : BaseOption, IMapFrom<TargetStrategy>
{
    public void Mapping(Profile profile)
    {
        profile.CreateMap<TargetStrategy, TargetStrategyOption>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TargetStrategyId));
    }
}
