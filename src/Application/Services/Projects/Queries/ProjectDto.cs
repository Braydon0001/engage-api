namespace Engage.Application.Services.Projects.Queries;

public class ProjectDto : IMapFrom<Project>, IMapFrom<ProjectStore>
{
    public int Id { get; init; }
    public string Name { get; init; }
    //public string Note { get; init; }
    public int ProjectTypeId { get; init; }
    public string ProjectTypeName { get; init; }
    public int ProjectSubTypeId { get; init; }
    public string ProjectSubTypeName { get; init; }
    public int ProjectStatusId { get; init; }
    public string ProjectStatusName { get; init; }
    public int ProjectPriorityId { get; set; }
    public string ProjectPriorityName { get; init; }
    public int ProjectCategoryId { get; init; }
    public string ProjectCategoryName { get; init; }
    public int ProjectSubCategoryId { get; init; }
    public string ProjectSubCategoryName { get; init; }
    public int? EngageRegionId { get; init; }
    public string EngageRegionName { get; init; }
    public string ProjectAssignedTo { get; set; }
    public string EngageBrandNames { get; set; }
    public string SupplierNames { get; set; }
    public List<string> Comments { get; set; }
    public int OwnerId { get; init; }
    public string OwnerName { get; init; }
    public int StoreId { get; init; }
    public string StoreName { get; init; }
    public DateTime Created { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string Description { get; init; }
    public List<JsonFile> Files { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Project, ProjectDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectId))
               .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Name))
               .ForMember(d => d.EngageBrandNames, opt => opt.MapFrom(s => string.Join(", ", s.EngageBrands.Select(f => f.EngageBrand.Name))));
        //.ForMember(d => d.ProjectTags, opt => opt.MapFrom(s => string.Join(", ", s.ProjectTags.Select(s => s.ProjectTag.Name))))
        //.ForMember(d => d.ProjectUsers, opt => opt.MapFrom(s => string.Join(", ", s.ProjectUsers.Select(s => s.User.Email))));

        profile.CreateMap<ProjectStore, ProjectDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectId))
               .ForMember(d => d.Description, opt => opt.MapFrom(s => s.Name))
               .ForMember(d => d.OwnerName, opt => opt.MapFrom(s => $"{s.Owner.FirstName} {s.Owner.LastName}"))
               .ForMember(d => d.EngageBrandNames, opt => opt.MapFrom(s => string.Join(", ", s.EngageBrands.Select(f => f.EngageBrand.Name))))
               .ForMember(d => d.Comments, opt => opt.MapFrom(s =>
                                                                    s.ProjectComments.OrderByDescending(x => x.ProjectCommentId).Take(5).Select(x => x.Comment).ToList()
                                                             ))
               .ForMember(d => d.SupplierNames, opt => opt.MapFrom(s => string.Join(", ", s.ProjectSuppliers.Select(f => f.Supplier.Name))));
        //.ForMember(d => d.ProjectAssignedTo, opt => opt.MapFrom(s => string.Concat(s.ProjectAssignees.Select(e => e.ProjectStakeholder.))));
        //.ForMember(d => d.ProjectTags, opt => opt.MapFrom(s => string.Join(", ", s.ProjectTags.Select(s => s.ProjectTag.Name))))
        //.ForMember(d => d.ProjectUsers, opt => opt.MapFrom(s => string.Join(", ", s.ProjectUsers.Select(s => s.User.Email))));
    }
}
