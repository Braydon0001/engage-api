namespace Engage.Application.Services.Projects.Queries;

public class ProjectStoreDashboardDto : IMapFrom<ProjectStore>
{
    public int Id { get; init; }
    public string Priority { get; init; }
    public string Status { get; init; }
    public string Category { get; init; }
    public string Type { get; init; }
    public string Store { get; init; }
    public string Region { get; init; }
    public DateTime Date { get; init; }
    public List<string> Suppliers { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectStore, ProjectStoreDashboardDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectId))
               .ForMember(d => d.Priority, opt => opt.MapFrom(s => s.ProjectPriority.Name))
               //.ForMember(d => d.OwnerName, opt => opt.MapFrom(s => $"{s.Owner.FirstName} {s.Owner.LastName}"))
               .ForMember(d => d.Status, opt => opt.MapFrom(s => s.ProjectStatus.Name))
               .ForMember(d => d.Category, opt => opt.MapFrom(s => s.ProjectCategory.Name))
               .ForMember(d => d.Type, opt => opt.MapFrom(s => s.ProjectType.Name))
               .ForMember(d => d.Store, opt => opt.MapFrom(s => s.Store.Name))
               .ForMember(d => d.Region, opt => opt.MapFrom(s => s.Store.EngageRegion.Name))
               .ForMember(d => d.Date, opt => opt.MapFrom(s => s.StartDate))
               .ForMember(d => d.Suppliers, opt => opt.MapFrom(s => s.ProjectSuppliers.Select(f => f.Supplier.Name).ToList()));
    }
}
