namespace Engage.Application.Services.ProjectCategorySuppliers.Queries;

public class ProjectCategorySupplierDto : IMapFrom<ProjectCategorySupplier>
{
    public int Id { get; init; }
    public int ProjectCategoryId { get; init; }
    public int SupplierId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCategorySupplier, ProjectCategorySupplierDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectCategorySupplierId));
    }
}
