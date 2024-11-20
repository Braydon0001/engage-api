
using Engage.Application.Services.Suppliers.Queries;

namespace Engage.Application.Services.ProjectCategories.Queries;

public class ProjectCategoryVm : IMapFrom<ProjectCategory>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public List<SupplierOption> SupplierIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCategory, ProjectCategoryVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectCategoryId))
               .ForMember(d => d.SupplierIds, opt => opt.MapFrom(s => s.ProjectCategorySuppliers.Select(e => e.Supplier)));
    }
}
