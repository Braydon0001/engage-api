
namespace Engage.Application.Services.StoreAssetStatuses.Queries;

public class StoreAssetStatusVm : IMapFrom<StoreAssetStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetStatus, StoreAssetStatusVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetStatusId));
    }
}
