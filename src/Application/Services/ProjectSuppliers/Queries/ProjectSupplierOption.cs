namespace Engage.Application.Services.ProjectSuppliers.Queries;

public class ProjectSupplierOption : IMapFrom<ProjectSupplier>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectSupplier, ProjectSupplierOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => $"{s.Supplier.Name}"));
    }
}