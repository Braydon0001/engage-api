using Engage.Application.Services.DCProducts.Queries;
using Engage.Application.Services.EngageBrands.Queries;
using Engage.Application.Services.ProjectCategories.Queries;
using Engage.Application.Services.ProjectStakeholders.Queries;
using Engage.Application.Services.ProjectStoreAssets.Queries;
using Engage.Application.Services.ProjectSubCategories.Queries;
using Engage.Application.Services.ProjectSubTypes.Queries;
using Engage.Application.Services.ProjectSuppliers.Queries;
using Engage.Application.Services.ProjectTypes.Queries;

namespace Engage.Application.Services.Projects.Queries;

public class ProjectStoreVm : IMapFrom<ProjectStore>
{
    public int Id { get; set; }
    public string Description { get; set; }
    public OptionDto ProjectPriorityId { get; set; }
    public ProjectTypeOption ProjectTypeId { get; set; }
    public ProjectSubTypeOption ProjectSubTypeId { get; set; }
    public OptionDto ProjectOwnerId { get; set; }
    public List<ProjectStakeholderSearchOption> ProjectAssignedTo { get; set; }
    public DateTime? EndDate { get; set; }
    public OptionDto ProjectStoreId { get; set; }
    public List<ProjectStoreAssetOption> StoreAssetIds { get; set; }
    public List<DCProductOption> DcProductIds { get; set; }
    public string ProjectStatus { get; set; }
    public string ProjectStatusId { get; set; }
    public ProjectCategoryOption ProjectCategoryId { get; set; }
    public ProjectSubCategoryOption ProjectSubCategoryId { get; set; }
    public List<ProjectSupplierOption> SupplierIds { get; set; }
    public List<EngageBrandOption> EngageBrandIds { get; set; }
    public List<ProjectStakeholderSearchOption> ProjectStakeholderIds { get; set; }
    public List<ProjectStakeholderSearchOption> StakeholderIds { get; set; }
    public DateTime? StartDate { get; set; }
    public bool IsOwner { get; set; }
    public bool IsAssigned { get; set; }
    public List<JsonFile> Files { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStore, ProjectStoreVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectId))
               .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Name))
               .ForMember(d => d.ProjectPriorityId, opt => opt.MapFrom(s => new OptionDto { Id = s.ProjectPriorityId, Name = s.ProjectPriority.Name }))
               .ForMember(d => d.ProjectTypeId, opt => opt.MapFrom(s => s.ProjectType))
               .ForMember(d => d.ProjectSubTypeId, opt => opt.MapFrom(s => s.ProjectSubType))
               .ForMember(d => d.ProjectCategoryId, opt => opt.MapFrom(s => s.ProjectCategory))
               .ForMember(d => d.ProjectSubCategoryId, opt => opt.MapFrom(s => s.ProjectSubCategory))
               .ForMember(d => d.ProjectOwnerId, opt => opt.MapFrom(s => s.OwnerId.HasValue ? new OptionDto { Id = s.OwnerId.Value, Name = $"{s.Owner.FirstName} {s.Owner.LastName}" } : null))
               .ForMember(d => d.ProjectAssignedTo, opt => opt.Ignore())
               .ForMember(d => d.ProjectStatus, opt => opt.MapFrom(s => s.ProjectStatusId.ToString()))
               .ForMember(d => d.ProjectStoreId, opt => opt.MapFrom(s => new OptionDto { Id = s.StoreId, Name = s.Store.Name }))
               .ForMember(d => d.DcProductIds, opt => opt.Ignore())
               .ForMember(d => d.SupplierIds, opt => opt.Ignore())
               .ForMember(d => d.StoreAssetIds, opt => opt.Ignore());
    }
}
