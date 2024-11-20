namespace Engage.Application.Services.StoreAssetFiles.Queries;

public class StoreAssetFileOption : IMapFrom<StoreAssetFile>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetFile, StoreAssetFileOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetFileId));
    }
}