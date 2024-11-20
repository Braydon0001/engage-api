namespace Engage.Application.Services.ProjectStoreAssets.Queries;

public class ProjectStoreAssetOption : IMapFrom<ProjectStoreAsset>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStoreAsset, ProjectStoreAssetOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreAssetId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => $"{s.StoreAsset.StoreAssetType.Name} - {s.StoreAsset.StoreAssetSubType.Name}"));
    }
}