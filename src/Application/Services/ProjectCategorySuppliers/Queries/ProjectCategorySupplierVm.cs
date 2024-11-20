
using Engage.Application.Services.ProjectCategories.Queries;
using Engage.Application.Services.Suppliers.Queries;

namespace Engage.Application.Services.ProjectCategorySuppliers.Queries;

public class ProjectCategorySupplierVm : IMapFrom<ProjectCategorySupplier>
{
    public int Id { get; init; }
    public ProjectCategoryOption ProjectCategoryId { get; init; }
    public SupplierOption SupplierId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectCategorySupplier, ProjectCategorySupplierVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectCategorySupplierId))
               .ForMember(d => d.ProjectCategoryId, opt => opt.MapFrom(s => s.ProjectCategory))
               .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Supplier));
    }
}
