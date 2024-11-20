using Engage.Application.Services.StoreAssetFileTypes.Queries;

namespace Engage.Application.Services.StoreAssetFiles.Queries;

public class StoreAssetFileVm : IMapFrom<StoreAssetFile>
{
    public int Id { get; init; }
    public OptionDto StoreAssetId { get; init; }
    public string ImageUrl { get; init; }
    public StoreAssetFileTypeOption StoreAssetFileTypeId { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetFile, StoreAssetFileVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetFileId))
               .ForMember(d => d.StoreAssetId, opt => opt.MapFrom(s => s.StoreAsset))
               .ForMember(d => d.StoreAssetFileTypeId, opt => opt.MapFrom(s => s.StoreAssetFileType));
    }
}
