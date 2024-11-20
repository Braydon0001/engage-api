namespace Engage.Application.Services.ProjectCategories.Queries;

public class ProjectCategoryDto : IMapFrom<ProjectCategory>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string SupplierNames { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCategory, ProjectCategoryDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectCategoryId))
               .ForMember(d => d.SupplierNames, opt => opt.MapFrom(s => string.Join(", ", s.ProjectCategorySuppliers.Select(e => e.Supplier.Name))));
    }
}
