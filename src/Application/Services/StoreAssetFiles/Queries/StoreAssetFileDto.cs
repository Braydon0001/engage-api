namespace Engage.Application.Services.StoreAssetFiles.Queries;

public class StoreAssetFileDto : IMapFrom<StoreAssetFile>
{
    public int Id { get; init; }
    public int StoreAssetId { get; init; }
    public string ImageUrl { get; init; }
    public int StoreAssetFileTypeId { get; init; }
    public string FileTypeName { get; init; }
    public List<JsonFile> Files { get; init; }
    public string Created { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetFile, StoreAssetFileDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetFileId))
               .ForMember(d => d.FileTypeName, opt => opt.MapFrom(s => s.StoreAssetFileType.Name))
               .ForMember(d => d.Created, opt => opt.MapFrom(s => s.Created.ToShortDateString()));
    }
}
