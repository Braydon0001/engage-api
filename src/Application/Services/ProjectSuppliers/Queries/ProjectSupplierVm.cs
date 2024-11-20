
using Engage.Application.Services.Projects.Queries;
using Engage.Application.Services.Suppliers.Queries;

namespace Engage.Application.Services.ProjectSuppliers.Queries;

public class ProjectSupplierVm : IMapFrom<ProjectSupplier>
{
    public int Id { get; init; }
    public ProjectOption ProjectId { get; init; }
    public SupplierOption SupplierId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSupplier, ProjectSupplierVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProjectSupplierId))
               .ForMember(d => d.ProjectId, opt => opt.MapFrom(s => s.Project))
               .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Supplier));
    }
}
