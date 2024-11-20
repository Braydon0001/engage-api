namespace Engage.Application.Services.StoreAssetStatuses.Queries;

public class StoreAssetStatusOption : IMapFrom<StoreAssetStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetStatus, StoreAssetStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetStatusId));
    }
}