namespace Engage.Application.Services.ProjectStoreAssets.Queries;

public class ProjectStoreAssetDto : IMapFrom<ProjectStoreAsset>
{
    public int Id { get; init; }
    public int ProjectId { get; init; }
    public int StoreAssetId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStoreAsset, ProjectStoreAssetDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectStoreAssetId));
    }
}
