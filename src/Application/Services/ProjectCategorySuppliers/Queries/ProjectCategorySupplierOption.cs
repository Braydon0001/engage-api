namespace Engage.Application.Services.ProjectCategorySuppliers.Queries;

public class ProjectCategorySupplierOption : IMapFrom<ProjectCategorySupplier>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCategorySupplier, ProjectCategorySupplierOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectCategorySupplierId));
    }
}