
using Engage.Application.Services.Projects.Queries;

namespace Engage.Application.Services.ProjectStoreAssets.Queries;

public class ProjectStoreAssetVm : IMapFrom<ProjectStoreAsset>
{
    public int Id { get; init; }
    public ProjectOption ProjectId { get; init; }
    public OptionDto StoreAssetId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStoreAsset, ProjectStoreAssetVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectStoreAssetId))
               .ForMember(d => d.ProjectId, opt => opt.MapFrom(s => s.Project))
               .ForMember(d => d.StoreAssetId, opt => opt.MapFrom(s => new OptionDto { Id = s.StoreAssetId, Name = s.StoreAsset.Name }));
    }
}
