
namespace Engage.Application.Services.StoreAssetFileTypes.Queries;

public class StoreAssetFileTypeVm : IMapFrom<StoreAssetFileType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetFileType, StoreAssetFileTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetFileTypeId));
    }
}
