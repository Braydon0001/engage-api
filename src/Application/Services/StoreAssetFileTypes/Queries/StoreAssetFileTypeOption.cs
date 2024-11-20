namespace Engage.Application.Services.StoreAssetFileTypes.Queries;

public class StoreAssetFileTypeOption : IMapFrom<StoreAssetFileType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreAssetFileType, StoreAssetFileTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetFileTypeId));
    }
}